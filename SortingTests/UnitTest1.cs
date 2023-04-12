using Lab2Sort;

namespace SortingTests;

public class Tests
{
    private ISort[] sortingAlgrorithms =
    {
        new TreeSort(),
        new QSort(),
        new InsertionSort()
    };

    [Test]
    public void RandomizesTests()
    {
        for (int i = 2; i < 100; i++)
        {
            var arr = Extensions.GetRandomizedArray(i);
            var sortedArray = (int[])arr.Clone();
            Array.Sort(sortedArray);
            foreach (var algorithm in sortingAlgrorithms)
            {
                var res = algorithm.Sort((int[])arr.Clone());
                Assert.IsTrue(Enumerable.SequenceEqual(res, sortedArray));
            }
        }
    }
}