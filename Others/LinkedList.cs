using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Others
{
    class Node
    {
        public Node Next;
        public string Data;
    }

    class LinkedList
    {
        private Node head;

        public void Add(Node node)
        {
            if (head == null)
                head = node;
            else
            {
                //node.Next = head;
                //head = node;
                Node tmp = head;
                while (tmp.Next != null)
                    tmp = tmp.Next;
                tmp.Next = node;
            }
        }

        public void ShowData()
        {
            Node curNode = head;
            while (curNode != null)
            {
                Console.Write("{0} ", curNode.Data);
                curNode = curNode.Next;
            }
            Console.WriteLine();
        }


        public void Reverse()
        {
            Node prev = null;

            while (head.Next != null)
            {
                Node next = head.Next;
                head.Next = prev;
                prev = head;
                head = next;
            }
            head.Next = prev;

            //Reverse(head, head.Next);
        }

        private void Reverse(Node prev, Node node)
        {
            if (node.Next != null)
                Reverse(node, node.Next);
            else
                head = node;

            node.Next = prev;
            prev.Next = null;
        }
    }

}
