using DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BoxDAL
{
    public class WareHouse : WareHouseMethods
    {

        private Configuration data { get; set; }

        private double Limit { get; set; }





        public WareHouse()
        {
            Tree = new XTree<double, XTree<double, Box>>();
            BoxDateExpired = new Tor<Box>();
            priorityBox = new Tor<Box>();
            //the limit from the json file
            data = new Configuration();
            Limit = data.Data.Limit;
        }


        public Tor<Box> BoxDateExpired { get; private set; }


        public Tor<Box> priorityBox { get; private set; }


        public XTree<double, XTree<double, Box>> Tree { get; private set; }



        /// <summary>
        ///  return the the bokes in the BoxDateExpired to list
        /// </summary>
        /// <returns></returns>
        public IQueryable<Box> GetBoxes()
        {
            return BoxDateExpired.AsQueryable();
        }

        /// <summary>
        /// return the the bokes in the offer to list
        /// </summary>
        /// <returns></returns>
        public IQueryable<Box> GetTheOffer()
        {
            return priorityBox.AsQueryable();
        }

        /// <summary>
        /// add box to the storage
        /// </summary>
        /// <param name="x">the length on width to the new box </param>
        /// <param name="y">the height to the new box</param>
        /// <param name="numOfBox">num of box</param>
        public void AddBox(double x, double y, int numOfBox = 1)
        {
           
            if (Tree.find(x))
            {
                //if ew have a same 'x' and a same 'y' add num of box in this item box
                if (Tree.ReturnValue(x).find(y))
                {
                    Tree.ReturnValue(x).ReturnValue(y).AddBoxs(numOfBox);
                }
                //if ew have a same 'x' but not a same 'y' add new box in the 'y' key
                else
                {
                    Box tempBox = new Box(x, y, numOfBox);

                    BoxDateExpired.insert(tempBox);

                    Tree.ReturnValue(x).addNode(y, tempBox);
                }
            }

            //if there is not 'x' add new box in new tree on new 'x' key
            else
            {
                XTree<double, Box> tempTree = new XTree<double, Box>();

                Box tempBox = new Box(x, y, numOfBox);

                tempTree.addNode(y, tempBox);

                BoxDateExpired.insert(tempBox);

                Tree.addNode(x, tempTree);
            }
        }

        /// <summary>
        /// remove the box from the storage 
        /// </summary>
        /// <param name="box">the box we hant to delete</param>
        private void RemoveBox(Box box)
        {
            //chack if the box Exists
            if (Tree.find(box.X))
            {
                if (Tree.ReturnValue(box.X).find(box.Y))
                {
                    Box tempBox = Tree.ReturnValue(box.X).ReturnValue(box.Y);
                    //remove the box in the order from the num of box
                    tempBox.RemoveBox();


                    //if There is no more left box in the box item delet the item
                    if (tempBox.NumOfBox == 0)
                    {
                        //if the box is singel on the 'y' tree delet this tree
                        if (Tree.ReturnValue(box.X).isASingelOnTheTree(box.Y))
                        {
                            Tree.RemoveNode(box.X);
                        }
                        //else remove the box from the 'Y' tree
                        else
                        { Tree.ReturnValue(box.X).RemoveNode(box.Y); }
                        UpdateQueue(tempBox);
                        priorityBox.RemoveTheLast();

                    }
                    //update the date expired
                    else
                    { UpdateQueue(tempBox); }




                }
            }

            return;
        }

        /// <summary>
        /// get the box we (x,y) input if it Exists
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Box GetEqualseBox(double x, double y)
        {
            Box tempBox;
            if (Tree.find(x))
            {
                if (Tree.ReturnValue(x).find(y))
                {
                    tempBox = Tree.ReturnValue(x).ReturnValue(y);
                    return tempBox;
                }
            }
            return null;
        }

        /// <summary>
        /// return the best box for the (x,y) input
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Box GetBestBox(double x, double y)
        {
            //if we have a equalse boe we returne thet box
            if (GetEqualseBox(x, y) != null)
            {
                return GetEqualseBox(x, y);
            }

            Box tempBox;

            //we search a box with the same 'x' and bigger 'y' 
            if (Tree.find(x))
            {
                tempBox = Tree.ReturnValue(x).GetBiggerKey(y);
                if (tempBox != null)
                {
                    if (tempBox.Y / y <= Limit)
                    {
                        return tempBox;
                    }
                }
            }
            
            if (Tree.GetBiggerKey(x) != null)
            {
                //we search a box with a bigger 'x' and same 'y' and returne him if it exist
                if (Tree.GetBiggerKey(x).find(y))
                {
                    tempBox = Tree.GetBiggerKey(x).ReturnValue(y);
                    if (tempBox.X / x <= Limit)
                    {
                        return tempBox;
                    }
                }

                //else we search a box with a bigger 'x' and bigger 'y'
                tempBox = Tree.GetBiggerKey(x).GetBiggerKey(y);
                if (tempBox != null)
                {
                    if (tempBox.X / x <= Limit && tempBox.Y / y <= Limit)
                    {
                        return tempBox;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// return the bigger box after the box input
        /// </summary>
        /// <param name="x">the length on width to the new box </param>
        /// <param name="y">the height to the new box</param>
        /// <param name="box">box for the comperison</param>
        /// <returns></returns>
        private Box GetBiggerBox(double x, double y, Box box)
        {
            //if the 'x' or the 'y' Passing the limit the box is not ligal
            Box tempBox;

            //we search a box wiht a bigger 'y' and the same 'x'
            tempBox = Tree.ReturnValue(box.X).GetBiggerKey(box.Y);

            if (tempBox != null)
            {
                if (tempBox.Y / y <= Limit)
                {
                    return tempBox;
                }
            }

            //if we don't have a box with a bigger 'x' we return null 
            if (Tree.GetBiggerKey(box.X) != null)
            {
                //search a box with a bigger 'x' and the same 'y' like the demand
                if (Tree.GetBiggerKey(box.X).find(y))
                {
                    tempBox = Tree.GetBiggerKey(box.X).ReturnValue(y);

                    if (tempBox.X / x <= Limit)
                    {
                        return tempBox;
                    }
                }
            }
            //search a box with a bigger 'x' and bigger 'y'
            tempBox = Tree.GetBiggerKey(box.X)?.GetBiggerKey(y);
            if (tempBox != null)
            {
                if (tempBox.X / x <= Limit && tempBox.Y / y <= Limit)
                {
                    return tempBox;
                }
            }

            return null;
        }

        /// <summary>
        /// return a list with the boxes in the order with regard to the input
        /// </summary>
        /// <param name="x">the length on width to the new box</param>
        /// <param name="y">the height to the new box</param>
        /// <param name="NumOfBox">num of box</param>
        /// <returns></returns>
        public Tor<Box> GetPriceOffer(double x, double y, int NumOfBox)
        {
            priorityBox = new Tor<Box>();
            //if we don't have the bast box we return a anpty list 
            if (GetBestBox(x, y) == null)
                return priorityBox;


            Box tmp = GetBestBox(x, y);

            //if in the best box we complit the num of box we need we return the list with the best box
            if (tmp.NumOfBox >= NumOfBox)
            {
                priorityBox = new Tor<Box>();
                tmp.boxInTheOffer = NumOfBox;
                priorityBox.insert(tmp);
                return priorityBox;
            }



            tmp.boxInTheOffer = tmp.NumOfBox;
            priorityBox.insert(tmp);
            int counter = NumOfBox - tmp.boxInTheOffer;
            tmp = GetBiggerBox(x, y, tmp);

            //we add box Until we complit the demand or When we dont have another box
            while (counter > 0)
            {
                if (tmp != null)
                {
                    if (tmp.NumOfBox >= counter)
                    {
                        tmp.boxInTheOffer = counter;
                        counter = 0;
                        priorityBox.insert(tmp);

                    }
                    else
                    {
                        tmp.boxInTheOffer = tmp.NumOfBox;
                        counter -= tmp.NumOfBox;
                        priorityBox.insert(tmp);
                    }

                    tmp = GetBiggerBox(x, y, tmp);
                }
                else
                {
                    return priorityBox;
                }
            }


            return priorityBox;
        }

        /// <summary>
        /// return the result offer. if it ampty return 0, if we have the same num of box in the offer and in the demand return 1,  if we have the same last in the offer return -1.
        /// </summary>
        /// <param name="NumOfBox">num of box in the demand</param>
        /// <returns></returns>
        public int CheakOffer(int NumOfBox)
        {//i
            if (priorityBox.IsEmpty())
                return 0;
            int count = 0;
            foreach (Box box in priorityBox)
            {
                count += box.boxInTheOffer;
            }
            if (count == NumOfBox)
                return 1;
            return -1;
        }

        private void UpdateQueue(Box box)
        {
            int counter = 0;
            Tor<Box> tmp = new Tor<Box>();

            Box theEqualsBox = null;

            for (int i = 0; i < BoxDateExpired.Count(); i++)
            {
                Box tempBox = BoxDateExpired.Dequeue();

                if (tempBox != box)
                    tmp.insert(tempBox);
                else
                {
                    theEqualsBox = tempBox;
                }
            }

            if (theEqualsBox != null)
                tmp.insert(theEqualsBox);


            BoxDateExpired = tmp;
        }

        /// <summary>
        /// delete the box in the order from the tree and from the priorityBox 
        /// </summary>
        /// <returns></returns>
        public bool Buy()
        {
            if (priorityBox?.Count() == 0 || priorityBox == null)
            {
                return false;
            }

            foreach (var item in priorityBox)
            {
                RemoveBox(item);
            }
           
            priorityBox = null;
            return true;

        }

        /// <summary>
        /// return the num of box in the priorityBox 
        /// </summary>
        /// <returns></returns>
        public int NumOfBoxInTheOffer()
        {
            return priorityBox.Count();
        }

        /// <summary>
        /// delete the expired box from the tree and from the priorityBox and show a message dialog
        /// </summary>
        public void DeleteExpiredBox()
        {
            if (BoxDateExpired.Head() != null)
            {

                while (BoxDateExpired.Head().IsExpiry())
                {
                     RemoveBox(BoxDateExpired.Head());
                        break;
                }
            }
        }
    }
}
