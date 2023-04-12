namespace Lab2Sort;

public class InsertionSort : ISort
{
    public int ComparasionCount
    {
        get => compCount;
    }
    private int compCount;

    public int[] Sort(int[] arr)
    {
        compCount = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            int cur = arr[i];
            int j = i;
            while (j > 0 && cur < arr[j - 1])
            {
                arr[j] = arr[j - 1];
                j--;
                compCount++;
            }
            arr[j] = cur;
        }

        return arr;
    }
}