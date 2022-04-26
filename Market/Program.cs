using System;

namespace Market
{
    class Program
    {
        private const string Order = "order";
        private const string Trade = "trade";
        private const string Quit = "quit";
        
        static void Main(string[] args)
        {
            var csv = new CsvData<GoodPrice>(args[0]);
            csv.Read();
            Console.WriteLine("Loaded csv");
            Console.WriteLine(csv);
            
            while (true)
            {
                var line = Console.ReadLine();
                var command = line?.Split()[0];
                switch (command)
                {
                    case Quit:
                        return;
                    case Order:
                        break;
                    case Trade:
                        break;
                    default:
                        Console.WriteLine($"{command} is not valid");
                        break;
                }
            }
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