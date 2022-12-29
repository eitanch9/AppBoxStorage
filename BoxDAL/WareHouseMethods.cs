using DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxDAL
{
    public interface WareHouseMethods
    {
        /// <summary>
      ///  return the the bokes in the BoxDateExpired to list
      /// </summary>
      /// <returns></returns>
        IQueryable<Box> GetBoxes();

        /// <summary>
        /// return the the bokes in the offer to list
        /// </summary>
        /// <returns></returns>
        IQueryable<Box> GetTheOffer();

        /// <summary>
        /// return the num of box in the priorityBox 
        /// </summary>
        /// <returns></returns>
        int NumOfBoxInTheOffer();

        /// <summary>
        /// add box to the storage
        /// </summary>
        /// <param name="x">the length on width to the new box </param>
        /// <param name="y">the height to the new box</param>
        /// <param name="numOfBox">num of box</param>
        void AddBox(double x, double y, int numOfBox = 1);

        /// <summary>
        /// delete the box in the order from the tree and from the priorityBox 
        /// </summary>
        /// <returns></returns>
        bool Buy();

        /// <summary>
        /// return a list with the boxes in the order with regard to the input
        /// </summary>
        /// <param name="x">the length on width to the new box</param>
        /// <param name="y">the height to the new box</param>
        /// <param name="NumOfBox">num of box</param>
        /// <returns></returns>
        Tor<Box> GetPriceOffer(double x, double y, int NumOfBox);

        /// <summary>
        /// return the result offer. if it ampty return 0, if we have the same num of box in the offer and in the demand return 1,  if we have the same last in the offer return -1.
        /// </summary>
        /// <param name="NumOfBox">num of box in the demand</param>
        /// <returns></returns>
        int CheakOffer(int NumOfBox);

        /// <summary>
        /// delete the expired box from the tree and from the priorityBox and show a message dialog
        /// </summary>
        void DeleteExpiredBox();
    }
}
