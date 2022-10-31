using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Test_Task_Saber_Interactive
{
    internal class ListRandom
    {
        public ListNode Head { get; set; }
        public ListNode Tail { get; set; }

        public int Count { get; set; }
        public ListRandom() { }

        public ListRandom(string data)
        {
            ListNode item = new ListNode(data);
            Head = item;
            Head.Random = item;
            Tail = item;
            Count = 1;
        }
        ListNode GetRandom()
        {
            Random rand = new Random();
            double chance = rand.NextDouble();
            var currentHead = Head;
            int index = Count;
            while (currentHead != null)
            {
                double probability = (double)1 / index;
                if (probability > chance)
                    return currentHead;
                else
                    currentHead = currentHead.Next;
                index--;
            }
            return null;
        }
        int GetIndex(ListNode node)
        {
            int index = 0;
            var currentHead = Head;
            while (currentHead != null)
            {
                if (currentHead == node)
                    return index;
                currentHead = currentHead.Next;
                index++;
            }
            return index;
        }
        ListNode GetItem(int index)
        {
            var currentHead = Head;
            int ind = 0;
            while (currentHead != null)
            {
                if (ind == index)
                    return currentHead;
                currentHead = currentHead.Next;
                ind++;
            }
            return null;
        }
        public void AddLast(string data)
        {
            ListNode item = new ListNode(data);
            if (Count == 0)
                Head = item;
            else
            {
                Tail.Next = item;
                item.Previous = Tail;
            }

            Tail = item;

            item.Random = GetRandom();
            Count++;
        }

        public void AddFirst(string data)
        {
            ListNode item = new ListNode(data);
            var currentHead = Head;
            item.Next = currentHead;
            Head = item;
            if (Count == 0)
                Tail = Head;
            else
            {
                currentHead.Previous = item;
            }
            item.Random = GetRandom();
            Count++;

        }

        public void Serialize(Stream s)
        {
            byte[] byteCount = BitConverter.GetBytes(Count);
            s.Write(byteCount, 0, 4);

            var currentHead = Head;
            int index = 0;
            while (currentHead != null)
            {


                byte[] byteData = Encoding.UTF8.GetBytes(currentHead.Data);
                byte[] byteCountData = BitConverter.GetBytes(byteData.Length);
                s.Write(byteCountData, 0, 4);
                s.Write(byteData, 0, byteData.Length);
                currentHead = currentHead.Next;
                index++;
            }

            currentHead = Head;
            index = 0;
            while (currentHead != null)
            {
                int randRef = GetIndex(currentHead.Random);

                byte[] byteData = BitConverter.GetBytes(randRef);
                s.Write(byteData, 0, 4);
                currentHead = currentHead.Next;
                index++;
            }

            Debug.WriteLine("Count " + Count, "SERIALIZATION :: ");
        }

        public void Deserialize(Stream s)
        {
            byte[] byteCount = new byte[4];
            s.Read(byteCount, 0, 4);
            int count = BitConverter.ToInt32(byteCount,0);
            Head = null;
            Tail = null;
            Count = 0;
            for(int i=0;i< count; i++)
            {
                byte[] byteCountData = new byte[4];
                s.Read(byteCountData, 0, 4);
                int countData = BitConverter.ToInt32(byteCountData, 0);

                byte[] byteData = new byte[countData];
                s.Read(byteData, 0, countData);
                string dataStr = Encoding.UTF8.GetString(byteData);
                ListNode item = new ListNode(dataStr);
                if (Count == 0)
                    Head = item;
                else
                {
                    Tail.Next = item;
                    item.Previous = Tail;
                }
                Tail = item;
                Count++;
            }

            var currentHead = Head;
            for (int i = 0; i < count; i++)
            {
                byte[] byteRandData = new byte[4];
                s.Read(byteRandData, 0, 4);
                int randIndex = BitConverter.ToInt32(byteRandData, 0);
                currentHead.Random = GetItem(randIndex);

                currentHead = currentHead.Next;
            }

            Debug.WriteLine("Count " + Count, "DESERIALIZATION :: ");
        }

    }
}
