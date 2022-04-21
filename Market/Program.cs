using System;

namespace Market
{
    class Program
    {
        static void Main(string[] args)
        {
            var csv = new CsvData<GoodPrice>(args[0]);
            csv.Read();
            Console.WriteLine(csv);
            // while (true)
            // {
            //     var line = Console.ReadLine();
            //     Console.WriteLine(line);
            // }
        }

        private class GoodPrice
        {
            public string Good { get; set; }
            public float Price { get; set; }

            public override string ToString()
            {
                return $"{nameof(Good)}: {Good}, {nameof(Price)}: {Price}";
            }
        }
    }
}