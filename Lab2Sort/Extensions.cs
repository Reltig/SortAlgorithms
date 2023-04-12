namespace Lab2Sort;

public static class Extensions
{
    public static int[] GetRandomizedArray(int size)
    {
        var rnd = new Random(DateTime.Now.Millisecond);
        return Enumerable.Range(1, size).OrderBy(x => rnd.Next()).ToArray();
    }
}