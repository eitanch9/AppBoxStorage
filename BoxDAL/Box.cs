using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxDAL
{
    public class Box
    {
        public int NumOfBox { get; set; }
        public int boxInTheOffer { get; set; }
        DateTime Expiry { get; set; }
        public double Y { get; set; }
        public double X { get; set; }


        public Box(double x, double y, int numBox = 1)
        {
            NumOfBox = numBox;
            boxInTheOffer = 0;
            Expiry = DateTime.Now.AddDays(30);
            Y = y;
            X = x;
        }

        /// <summary>
        /// add boxes to the item box
        /// </summary>
        /// <param name="num">num of box for add</param>
        public void AddBoxs(int num)
        {
            NumOfBox += num;
        }

        /// <summary>
        /// check if the date expiry hase passed
        /// </summary>
        /// <returns></returns>
        public bool IsExpiry()
        {
            if (Expiry <= DateTime.Now)
            { return true; }
            return false;
        }

        /// <summary>
        /// remove the box in the order from the num of box
        /// </summary>
        public void RemoveBox()
        {
            NumOfBox -= boxInTheOffer;
            Expiry = DateTime.Now.AddDays(30);
        }


        public override string ToString()
        {
            return $" {X}:{Y} , box in the offer: {boxInTheOffer} , box in total: {NumOfBox }, Expiry date : {Expiry:d}";
        }

     
    }
}
