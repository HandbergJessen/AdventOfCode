using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Process;

public class Day8 : IDay
{
    public string PartA(string[] input)
    {
        Node startNode = GetStartNode(input);
        char[] instructions = input[0].ToCharArray();
        int instructionCount = GetInstructionCount(instructions, startNode);

        return instructionCount.ToString();
    }

    public string PartB(string[] input)
    {
        List<Node> startNodes = GetStartNodes(input);
        char[] instructions = input[0].ToCharArray();

        long result = GetInstructionCount(instructions, startNodes[0]);

        for (int i = 1; i < startNodes.Count; i++)
        {
            result = LeastCommonMultiple(result, GetInstructionCount(instructions, startNodes[i]));
        }

        return result.ToString();
    }

    private static int GetInstructionCount(char[] instructions, Node node)
    {
        int countInstructions = 0;
        while (true)
        {
            foreach (char instruction in instructions)
            {
                countInstructions++;

                if (instruction == 'L')
                {
                    node = node.LeftNode!;
                }
                else if (instruction == 'R')
                {
                    node = node.RightNode!;
                }

                if (node.Name.Contains('Z'))
                {
                    return countInstructions;
                }
            }
        }
    }

    private static long GreatestCommonFactor(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    private static long LeastCommonMultiple(long a, long b)
    {
        return a / GreatestCommonFactor(a, b) * b;
    }

    private class Node
    {
        public string Name { get; private set; }
        public Node? LeftNode { get; set; }
        public Node? RightNode { get; set; }

        public Node(string name)
        {
            Name = name;
        }
    }

    private static Node GetStartNode(string[] input)
    {
        Dictionary<string, Node> nodes = new();
        Node startNode = new("");

        for (int i = 2; i < input.Length; i++)
        {
            string nodeName = input[i].Split(' ')[0];
            Node node = new(nodeName);

            if (nodeName == "AAA")
            {
                startNode = node;
            }

            nodes.Add(nodeName, node);
        }

        for (int i = 2; i < input.Length; i++)
        {
            string[] lineParts = input[i].Split(' ', '(', ',', ')');
            Node node = nodes[lineParts[0]];
            Node leftNode = nodes[lineParts[3]];
            Node rightNode = nodes[lineParts[5]];

            node.LeftNode = leftNode;
            node.RightNode = rightNode;
        }

        return startNode;
    }

    private static List<Node> GetStartNodes(string[] input)
    {
        Dictionary<string, Node> nodes = new();
        List<Node> startNodes = new();

        for (int i = 2; i < input.Length; i++)
        {
            string nodeName = input[i].Split(' ')[0];
            Node node = new(nodeName);

            if (nodeName.Contains('A'))
            {
                startNodes.Add(node);
            }

            nodes.Add(nodeName, node);
        }

        for (int i = 2; i < input.Length; i++)
        {
            string[] lineParts = input[i].Split(' ', '(', ',', ')');
            Node node = nodes[lineParts[0]];
            Node leftNode = nodes[lineParts[3]];
            Node rightNode = nodes[lineParts[5]];

            node.LeftNode = leftNode;
            node.RightNode = rightNode;
        }

        return startNodes;
    }
}