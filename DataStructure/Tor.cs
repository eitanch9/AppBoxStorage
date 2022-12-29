using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Tor<V> : IEnumerable<V>
    {
        public class Node
        {
            //definition
            private V value;
            private Node next;

            //constructors
            public Node(V value)
            {
                this.value = value;
                this.next = null;
            }
            public Node(V value, Node next)
            {
                this.value = value;
                this.next = next;
            }


            // Get,Set
            public V Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
            public Node Next
            {
                get { return this.next; }
                set { this.next = value; }
            }


            public bool HasNext()
            {
                return (this.next != null);
            }
        }

        private Node first;
        private Node last;

        public Tor()
        {
            this.first = null;
            this.last = null;
        }

        /// <summary>
        /// add value in the and of the tor
        /// </summary>
        /// <param name="t"></param>
        public void insert(V t)
        {
            if (first == null)
            {
                Node tmp = new Node(t);
                first = tmp;
                last = first;
            }
            else
            {
                last.Next = new Node(t);
                last = last.Next;
            }
        }

        /// <summary>
        /// return and delet the first in the tor
        /// </summary>
        /// <returns></returns>
        public V Dequeue()
        {
        
            if (first==null)
            {
                return default;
            }
            if (first.Next == null )
            {
                V val = first.Value;
                last = null;
                first= null;
                return val;
            }
            Node tmp = first;

            first = first.Next;

            return tmp.Value;
        }

        /// <summary>
        /// remove the last in the tor
        /// </summary>
        public void RemoveTheLast()
        {
            if (first==last)
            {
                first = null;
                last = null;
                return;
            }
            Node temp=first;
            while(temp.Next!=null)
            {
                if (temp.Next==last)
                {
                    temp = last;
                    return;
                }
                temp = temp.Next;
            }
        }

       /// <summary>
       /// cheak if the queue is empty
       /// </summary>
       /// <returns></returns>
        public bool IsEmpty() 
        {
            return first == null;
        }

        /// <summary>
        /// return the value of the  first elemnt
        /// </summary>
        /// <returns></returns>
        public V Head()
        {
            return first.Value;
        }

        /// <summary>
        /// return the num of value in the tor
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            if (first == null)
                return 0;
            else
            {
                int counter = 0;
                Node tmp = first;
                while (tmp != null)
                {
                    counter++;
                    tmp = tmp.Next;
                }
                return counter;
            }
        }


        public override string ToString()
        {
            Node tmp = first;
            string str = "";
            while (tmp != null)
            {
                str += $"{tmp.Value} \n";
                tmp = tmp.Next;
            }
            return str;
        }


        /// <summary>
        /// return the value class enumarabel 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<V> GetEnumerator()
        {
            Node note = first;

            while (note != null)
            {
                yield return note.Value;
                note = note.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
           
            Node note = first;

            while (note != null)
            {
                yield return note.Value;
                note = note.Next;
            }
        }
    }
}
