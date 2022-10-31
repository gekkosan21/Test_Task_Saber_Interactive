
namespace Test_Task_Saber_Interactive
{
    internal class ListNode
    {
        public ListNode Previous { get; set; }
        public ListNode Next { get; set; }
        public ListNode Random { get; set; } // произвольный элемент внутри списка
        public string Data { get; set; }

        public ListNode() { }
        public ListNode(string data)
        {
            Data = data;
        }
    }
}
