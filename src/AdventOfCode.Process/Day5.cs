namespace AdventOfCode.Process;

public class Day5 : IDay
{
    public string PartA(string[] input)
    {
        List<long> seeds = GetNumbers(input[0]);
        List<Transformer> transformers = GetTransformers(input);

        long lowestValue = long.MaxValue;
        foreach (long seed in seeds)
        {
            long currentValue = seed;

            foreach (Transformer transformer in transformers)
            {
                currentValue = transformer.Transform(currentValue);
            }

            if (currentValue < lowestValue)
            {
                lowestValue = currentValue;
            }
        }

        return lowestValue.ToString();
    }

    public string PartB(string[] input)
    {
        List<long> seeds = GetNumbers(input[0]);
        List<Transformer> transformers = GetTransformers(input);

        long lowestValue = long.MaxValue;
        for (int i = 0; i < seeds.Count; i += 2)
        {
            long from = seeds[i];
            long to = seeds[i] + seeds[i + 1];

            for (long start = from; start < to; start++)
            {
                long currentValue = start;

                foreach (Transformer transformer in transformers)
                {
                    currentValue = transformer.Transform(currentValue);
                }

                if (currentValue < lowestValue)
                {
                    lowestValue = currentValue;
                }
            }

            Console.WriteLine($"{i / 2}. done!");
        }

        return lowestValue.ToString();
    }

    private class Transformer
    {
        private readonly List<Range> ranges;

        public Transformer()
        {
            ranges = new();
        }

        public void AddRange(Range range)
        {
            ranges.Add(range);
        }

        public long Transform(long number)
        {
            foreach (Range range in ranges)
            {
                if (range.InRange(number))
                {
                    return range.Transform(number);
                }
            }

            return number;
        }

        public List<Range> Transform(List<Range> ranges)
        {
            return new List<Range>();
        }
    }

    private class Range
    {
        private readonly long value;
        private readonly long start;
        private readonly long end;

        public Range(long value, long start, long range)
        {
            this.value = value;
            this.start = start;
            end = start + range - 1;
        }

        public bool InRange(long number)
        {
            return number >= start && number <= end;
        }

        public long Transform(long number)
        {
            return number - start + value;
        }
    }

    private static List<Transformer> GetTransformers(string[] input)
    {
        List<Transformer> transformers = new();
        Transformer currentTransformer = new();
        for (int i = 3; i < input.Length; i++)
        {
            if (input[i].Contains("map"))
            {
                transformers.Add(currentTransformer);
                currentTransformer = new Transformer();
            }
            else if (!input[i].Equals(""))
            {
                List<long> values = GetNumbers(input[i]);
                currentTransformer.AddRange(new Range(values[0], values[1], values[2]));
            }
        }

        transformers.Add(currentTransformer);

        return transformers;
    }

    private static List<long> GetNumbers(string line)
    {
        List<long> numbers = new();

        foreach (string value in line.Split(" "))
        {
            if (long.TryParse(value, out long number))
            {
                numbers.Add(number);
            }
        }

        return numbers;
    }
}