namespace AdventOfCode.Process;

public class Day7 : IDay
{
    public string PartA(string[] input)
    {
        List<Hand> hands = new();

        foreach (string line in input)
        {
            string[] lineDetails = line.Split(' ');
            hands.Add(new Hand(int.Parse(lineDetails[1]), lineDetails[0], false));
        }

        hands.Sort();

        int winnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            winnings += hands[i].Price * (i + 1);
        }

        return winnings.ToString();
    }

    public string PartB(string[] input)
    {
        List<Hand> hands = new();

        foreach (string line in input)
        {
            string[] lineDetails = line.Split(' ');
            hands.Add(new Hand(int.Parse(lineDetails[1]), lineDetails[0], true));
        }

        hands.Sort();

        int winnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            winnings += hands[i].Price * (i + 1);
        }

        return winnings.ToString();
    }

    public class Hand : IComparable<Hand>
    {
        public List<int> HandValues { get; private set; }
        public int HandScore { get; private set; }
        public int Price { get; private set; }

        public Hand(int price, string hand, bool withJoker)
        {
            Price = price;

            if (withJoker)
            {
                HandValues = GenerateJokerHandValues(hand);
                HandScore = GenerateJokerHandScore(hand);
            }
            else
            {
                HandValues = GenerateHandValues(hand);
                HandScore = GenerateHandScore(hand);
            }
        }

        public int CompareTo(Hand? other)
        {
            if (other == null)
            {
                return 0;
            }

            int comparison = HandScore.CompareTo(other.HandScore);

            if (comparison != 0)
            {
                return comparison;
            }

            for (int i = 0; i < 5; i++)
            {
                comparison = HandValues[i].CompareTo(other.HandValues[i]);

                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return comparison;
        }

        private static List<int> GenerateHandValues(string hand)
        {
            List<int> handValues = new();

            foreach (char card in hand.ToCharArray())
            {
                switch (card)
                {
                    case 'A':
                        handValues.Add(14);
                        break;
                    case 'K':
                        handValues.Add(13);
                        break;
                    case 'Q':
                        handValues.Add(12);
                        break;
                    case 'J':
                        handValues.Add(11);
                        break;
                    case 'T':
                        handValues.Add(10);
                        break;
                    default:
                        handValues.Add(int.Parse(card.ToString()));
                        break;
                }
            }

            return handValues;
        }

        private static List<int> GenerateJokerHandValues(string hand)
        {
            List<int> handValues = new();

            foreach (char card in hand.ToCharArray())
            {
                switch (card)
                {
                    case 'A':
                        handValues.Add(14);
                        break;
                    case 'K':
                        handValues.Add(13);
                        break;
                    case 'Q':
                        handValues.Add(12);
                        break;
                    case 'J':
                        handValues.Add(1);
                        break;
                    case 'T':
                        handValues.Add(10);
                        break;
                    default:
                        handValues.Add(int.Parse(card.ToString()));
                        break;
                }
            }

            return handValues;
        }

        private static int GenerateHandScore(string hand)
        {
            IDictionary<char, int> cardOccurences = new Dictionary<char, int>();

            foreach (char card in hand.ToCharArray())
            {
                if (cardOccurences.ContainsKey(card))
                {
                    cardOccurences[card]++;
                }
                else
                {
                    cardOccurences.Add(card, 1);
                }
            }

            int score = 0;
            foreach (int cardOccurence in cardOccurences.Values)
            {
                score += (int)Math.Pow(cardOccurence, 2);
            }

            return score;
        }

        private static int GenerateJokerHandScore(string hand)
        {
            IDictionary<char, int> cardOccurences = new Dictionary<char, int>();
            int jokers = 0;

            foreach (char card in hand.ToCharArray())
            {
                if (card == 'J')
                {
                    jokers++;
                    continue;
                }

                if (cardOccurences.ContainsKey(card))
                {
                    cardOccurences[card]++;
                }
                else
                {
                    cardOccurences.Add(card, 1);
                }
            }

            if (jokers == 5)
            {
                return 25;
            }

            char bestCard = ' ';
            int occurences = 0;
            foreach (char card in cardOccurences.Keys)
            {
                if (cardOccurences[card] > occurences)
                {
                    occurences = cardOccurences[card];
                    bestCard = card;
                }
            }

            cardOccurences[bestCard] += jokers;

            int score = 0;
            foreach (int cardOccurence in cardOccurences.Values)
            {
                score += (int)Math.Pow(cardOccurence, 2);
            }

            return score;
        }
    }
}