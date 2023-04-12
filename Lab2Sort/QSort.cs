namespace Lab2Sort;

public class QSort : ISort
{
    public int ComparasionCount
    {
        get => compCount;
    }
    private int compCount;

    public int[] Sort(int[] arr)
    {
        compCount = 0;
        Quicksort(arr, 0, arr.Length-1);
        return arr;
    }
    
    private int Partition (int[] array, int start, int end) 
    {
        int temp;
        int pointer = start;
        for ( int i = start; i < end; i++ ) 
        {
            if ( array[i] < array[end] ) 
            {
                temp = array[pointer];
                array[pointer] = array[i];
                array[i] = temp;
                pointer++;
                compCount++;
            }
        }
        temp = array[pointer];
        array[pointer] = array[end];
        array[end] = temp; 
        return pointer;
    }

    private void Quicksort (int[] array, int start, int end)
    {
        if ( start >= end ) 
        {
            return;
        }
        int pivot = Partition (array, start, end);
        Quicksort (array, start, pivot-1);
        Quicksort (array, pivot+1, end);
    }
}