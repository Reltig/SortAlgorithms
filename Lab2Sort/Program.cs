// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using Lab2Sort;
using Extensions = Lab2Sort.Extensions;

class Program
{
    private static int[] counts = new[]
    {
        1000,
        2000,
        10000,
        20000,
    };

    private static ISort[] sortAlgoritms = new ISort[]
    {
        new InsertionSort(),
        new TreeSort(),
        new QSort()
    };
    
    public static void Main(string[] args)
    {
        if (args.Length != 0)
            counts = args.Select(s => int.Parse(s)).ToArray();
        var data = SetExperiments();
        RSS(data);
        SaveToXml(data);
    }
    
    private static void RSS(ExperimentData[] experimentDatas)
    {
        Func<int, double> f;
        var n = counts.Length;
        foreach (var groupedExp in experimentDatas.GroupBy(exp => exp.AlgorithmName))
        {
            if (groupedExp.Key == nameof(InsertionSort))
                f = x => x * x;
            else
                f = x => x*Math.Log2(x);
            var sf = groupedExp.Select(x => f(x.Lenght)).Sum();
            var sy = groupedExp.Select(x => x.Count).Sum();
            var sfy = groupedExp.Select(x => f(x.Lenght) * x.Count).Sum();
            var sf2 = groupedExp.Select(x => f(x.Lenght)).Select(i=>i*i).Sum();
            var a = (n * sfy - sf * sy)/(n*sf2 - sf*sf);
            var b = (sy - a * sf) / n;
            Console.WriteLine($"{groupedExp.Key} a={a} b={b}");
        }
    }

    private static ExperimentData[] SetExperiments()
    {
        var result = new ExperimentData[counts.Length * sortAlgoritms.Length];
        var pointer = 0;
        foreach(var count in counts)
        {
            var source = Extensions.GetRandomizedArray(count);
            foreach (var algoritm in sortAlgoritms)
            {
                algoritm.Sort((int[])source.Clone());
                result[pointer++] = new ExperimentData()
                {
                    Count = algoritm.ComparasionCount, 
                    Lenght = count, 
                    AlgorithmName = algoritm.GetType().Name
                };
            }
        }

        return result;
    }

    private static void SaveToXml(ExperimentData[] data)
    {
        var xdoc = new XDocument();
        var experiments = new XElement("experiments");

        foreach (var groupedExp in data.GroupBy(d => d.AlgorithmName))
        {
            var algorithm = new XElement("algorithm");
            algorithm.Add(new XAttribute("name", groupedExp.Key));
            foreach (var expData in groupedExp)
            {
                var experiment = new XElement("experiment");
                var elementsCountAttr = new XAttribute("Lenght", expData.Lenght);
                var comparasionCountAttr = new XAttribute("Count", expData.Count);
                experiment.Add(elementsCountAttr);
                experiment.Add(comparasionCountAttr);
                algorithm.Add(experiment);
            }
            experiments.Add(algorithm);
        }

        xdoc.Add(experiments);
        xdoc.Save("exp.xml");
    }
}