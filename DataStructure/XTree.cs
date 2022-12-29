using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class XTree<k, v> where k : IComparable<k>
    {
        public class Node
        {
            public k Key { get; set; }
            public v Value { get; set; }
            private Node _left, _right, _current;



            public Node Left { get { return _left; } set { _left = value; } }

            public Node Right { get { return _right; } set { _right = value; } }


            public Node(k key, v value)
            {
                this.Key = key;
                this.Value = value;
            }


            public override string ToString()
            {
                return $"{Value}";
            }

        }

        public Node Root { get; set; }

        public XTree() { Root = null; }

        /// <summary>
        /// add node in the tree 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void addNode(k key, v value)
        {
            if (Root == null) { Root = new Node(key, value); }
            else
                addNode(key, value, Root);
        }

        private void addNode(k key, v value, Node node)
        {
            if (key.CompareTo(node.Key) > 0)
            {
                if (node.Right == null)
                    node.Right = new Node(key, value);
                else
                    addNode(key, value, node.Right);
            }
            else
            {
                if (node.Left == null)
                    node.Left = new Node(key, value);
                else
                    addNode(key, value, node.Left);
            }
        }


        /// <summary>
        /// return if the key Exists in the tree
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool find(k key) => find(key, Root);

        private bool find(k key, Node n)
        {
            if (n == null)
            { return false; }


            int compare = key.CompareTo(n.Key);


            if (compare == 0)
            { return true; }


            else if (compare > 0)
            { return find(key, n.Right); }


            else
            { return find(key, n.Left); }

        }


        /// <summary>
        /// rturne value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public v ReturnValue(k key)
        {
            return ReturnValue(key, Root);
        }

        private v ReturnValue(k key, Node node)
        {
            if (node == null)
            {
                return default;
            }

            int compare = key.CompareTo(node.Key);

            if (compare == 0)
            { return node.Value; }


            else if (compare > 0)
            { return ReturnValue(key, node.Right); }


            else
            { return ReturnValue(key, node.Left); }
        }


        /// <summary>
        /// return true if the key is the root
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool isTheRoot(k key)
        {
            if (Root.Key.CompareTo(key) == 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// return true if the key is the singel key in the tree
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool isASingelOnTheTree(k Key)
        {
            if (isTheRoot(Key))
            {
                if (Root.Left == null && Root.Right == null)
                { return true; }

            }
            return false;
        }

        private Node GetSuccessor(Node node)
        {
            Node parentOfSuccessor = node;
            Node successor = node;
            Node current = node.Right;

            //starting at the right child we go down every left child node
            while (current != null)
            {
                parentOfSuccessor = successor;
                successor = current;
                current = current.Left;// go to next left node
            }
            //if the succesor is not just the right node then
            if (successor != node.Right)
            {
                //set the Left node on the parent node of the succesor node to the right child node of the successor in case it has one
                parentOfSuccessor.Left = successor.Right;
                //attach the right child node of the node being deleted to the successors right node
                successor.Right = node.Right;
            }
            //attach the left child node of the node being deleted to the successors leftnode node
            successor.Left = node.Left;

            return successor;
        }

        /// <summary>
        /// remove a note from the tree by key
        /// </summary>
        /// <param name="Key">the key we wont to remove</param>
        public void RemoveNode(k Key)
        {//Set the current and parent node to root, so when we remove we can remove using the parents reference
            var current = Root;
            var parent = Root;
            bool isLeftChild = false;

            //empty tree
            if (current == null)
            {
                return;
            }


            var compare = Key.CompareTo(current.Key);

            //Find the Node
            //loop through until node is not found or if we found the node with matching data
            while (current != null && compare != 0)
            {
                //set current node to be new parent reference, then we look at its children

                parent = current;

                //if the data we are looking for is less than the current node then we look at its left child

                if (compare < 0)
                {
                    current = current.Left;
                    isLeftChild = true;//Set the variable to determin which child we are looking at
                }
                else
                {//Otherwise we look at its right child
                    current = current.Right;
                    isLeftChild = false;
                }

                compare = Key.CompareTo(current.Key);
            }
            //if the node is not found nothing to delete just return
            if (current == null)
            {
                return;
            }

            //We found a Leaf node aka no children
            if (current.Right == null && current.Left == null)
            {
                //The root doesn't have parent to check what child it is,so just set to null
                if (current == Root)
                {
                    current = null;
                }
                else
                {
                    //When not the root node
                    //see which child of the parent should be deleted
                    if (isLeftChild)
                    {
                        //remove reference to left child node
                        parent.Left = null;
                    }
                    else
                    {   //remove reference to right child node
                        parent.Right = null;
                    }
                }
            }

            else if (current.Right == null) //current only has left child, so we set the parents node child to be this nodes left child
            {
                //If the current node is the root then we just set root to Left child node
                if (current == Root)
                {
                    Root = current.Left;
                }
                else
                {
                    //see which child of the parent should be deleted
                    if (isLeftChild)//is this the right child or left child
                    {
                        //current is left child so we set the left node of the parent to the current nodes left child
                        parent.Left = current.Left;
                    }
                    else
                    {   //current is right child so we set the right node of the parent to the current nodes left child
                        parent.Right = current.Left;
                    }
                }
            }

            else if (current.Left == null) //current only has right child, so we set the parents node child to be this nodes right child
            {
                //If the current node is the root then we just set root to Right child node
                if (current == Root)
                {
                    Root = current.Right;
                }
                else
                {
                    //see which child of the parent should be deleted
                    if (isLeftChild)
                    {   //current is left child so we set the left node of the parent to the current nodes right child
                        parent.Left = current.Right;
                    }
                    else
                    {   //current is right child so we set the right node of the parent to the current nodes right child
                        parent.Right = current.Right;
                    }
                }
            }

            else//Current Node has both a left and a right child
            {
                //When both child nodes exist we can go to the right node and then find the leaf node of the left child as this will be the least number
                //that is greater than the current node. It may have right child, so the right child would become..left child of the parent of this leaf aka successer node

                //Find the successor node aka least greater node
                Node successor = GetSuccessor(current);
                //if the current node is the root node then the new root is the successor node
                if (current == Root)
                {
                    Root = successor;
                }
                else if (isLeftChild)
                {//if this is the left child set the parents left child node as the successor node
                    parent.Left = successor;
                }
                else
                {//if this is the right child set the parents right child node as the successor node
                    parent.Right = successor;
                }
            }
        }

        
       



      
        private v GetBiggerKey(k key, Node tempNode, Node node)
        {
            //comper the key in with the key in the node 
            int comper = key.CompareTo(node.Key);


            if (comper < 0)
            {
                if (node.Left != null)
                {
                    //save the note in the temp 
                    tempNode = node;
                    //go to left for bigger key
                    return GetBiggerKey(key, tempNode, node.Left);
                }
                //return the bigger key 
                return ReturnValue(node.Key);

            }

            //go to right for bigger key
            if (node.Right != null)
            {
                return GetBiggerKey(key, tempNode, node.Right);
            }

            //if the key in the temp is too small return difault els return the key in the tempnote
            if (key.CompareTo(tempNode.Key) < 0)
            {
                return ReturnValue(tempNode.Key);
            }

            //There is no greater value
            return default;
        }

        /// <summary>
        ///  return the bigger key after the key in
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public v GetBiggerKey(k key)
        {
            return GetBiggerKey(key, Root, Root);
        }


    }
}
