using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task_Saber_Interactive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListRandom list = new ListRandom();
            list.AddLast("4");
            list.AddFirst("8");
            list.AddFirst("2");
            list.AddLast("1");
            list.AddLast("5");

            PrintList(list);

            using (Stream s = new FileStream("data", FileMode.Create))
            {
                list.Serialize(s);
            }

            Console.WriteLine();

            using (Stream s = new FileStream("data", FileMode.Open))
            {
                list.Deserialize(s);
            }

            PrintList(list);

            Console.ReadLine();
        }

        static void PrintList(ListRandom list)
        {
            var currentHead = list.Head;
            while (currentHead != null)
            {
                Console.Write("{ ");

                if (currentHead.Previous != null)
                    Console.Write("Prev: " + currentHead.Previous.Data + "; ");
                else
                    Console.Write("Prev: null" + "; ");

                Console.Write("Data: " + currentHead.Data + "; ");

                if (currentHead.Next != null)
                    Console.Write("Next: " + currentHead.Next.Data + "; ");
                else
                    Console.Write("Next: null" + "; ");

                Console.Write("Random: " + currentHead.Random.Data + " [ ");

                if (currentHead.Random.Previous != null)
                    Console.Write(currentHead.Random.Previous.Data + "; ");
                else
                    Console.Write("null; ");

                if (currentHead.Random.Next != null)
                    Console.Write(currentHead.Random.Next.Data + " ]");
                else
                    Console.Write("null ]");

                Console.Write(" } \r\n");

                currentHead = currentHead.Next;
            }
        }
    }
}
