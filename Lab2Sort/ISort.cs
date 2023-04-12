namespace Lab2Sort;

public interface ISort
{
    int ComparasionCount { get; }
    int[] Sort(int[] arr);
}