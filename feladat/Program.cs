using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace feladat
{
    class Program
    {
        static Connection connection;
        static void Main(string[] args)
        {
            Start();
        }
        static void Start()
        {
            connection = new Connection("http://127.1.1.1:3000");

            inp();
        }
        static void inp()
        {
            Console.WriteLine("Mit szeretnél csinálni? (vásárolni, nézelődni, törölni)");
            string input = Console.ReadLine();
            if (input == "q")
            {

            }
            else if (input == "")
            {
                Console.Clear();
                inp();
            }
            else
            {
                Load(input);
                inp();
            }
        }
        static async void Load(string input)
        {
            if (input == "vásárolni" || input == "1")
            {
                Console.WriteLine("Mi legyen a neve?");
                string name = Console.ReadLine();
                Console.WriteLine("Mi legyen az értékelése?");
                float score = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Mi legyen az ára?");
                int price = Convert.ToInt32(Console.ReadLine());

                connection.Create(name, score, price);
            }
            else if (input == "nézelődni" || input == "2")
            {
                foreach (string item in await connection.All())
                {
                    Console.WriteLine(item);
                }
            }
            else if (input == "törölni" || input == "3")
            {
                Console.WriteLine("Mi legyen az id-ja?");
                int id = Convert.ToInt32(Console.ReadLine());

                connection.Delete(id);
            }
            else
            {
                Console.WriteLine("Hejtelen kommand");
            }
        }
    }
}
