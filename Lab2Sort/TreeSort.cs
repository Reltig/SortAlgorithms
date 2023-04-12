namespace Lab2Sort;

public class TreeSort : ISort
{
    public int ComparasionCount
    {
        get => Node.compCount;
    }

    private Node root;
    public int[] Sort(int[] arr)
    {
        Node.compCount = 0;
        BuidlTree(arr);
        return root.Sorted().ToArray();
    }

    private void BuidlTree(int[] arr)
    {
        root = new Node(arr[0]);
        for(int i = 1; i < arr.Length; i++)
        {
            AddNode(arr[i]);
        }
    }

    private void AddNode(int value)
    {
        root.AddNode(new Node(value));
    }
}

public class Node
{
    public static int compCount;
    public Node(int value)
    {
        Value = value;
    }

    public Node RightChild { get; private set; }
    public Node LeftChild { get; private set; }
    public int Value { get; set; }

    public void AddNode(Node node)
    {
        compCount++;
        if (node.Value <= Value)
        {
            if (LeftChild == null)
                LeftChild = node;
            else
                LeftChild.AddNode(node);
        }
        else
        {
            if (RightChild == null)
                RightChild = node;
            else
                RightChild.AddNode(node);
        }
    }

    public IEnumerable<int> Sorted()
    {
        if (LeftChild != null)
            foreach (var child in LeftChild.Sorted())
                yield return child;
        yield return Value;
        if (RightChild != null)
            foreach (var child in RightChild.Sorted())
                yield return child;
    }
}