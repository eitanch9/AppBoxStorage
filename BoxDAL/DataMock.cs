using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoxDAL
{
    public class DataMock
    {
        public WareHouseMethods boxes { get; private set; }

        private static DataMock _instance;

        public static DataMock Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataMock();
                return _instance;
            }
        }
        private DataMock()
        {


            boxes = new WareHouse();
            Init();
        }


        //add boxes to the datamock
        private void Init()
        {

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    boxes.AddBox(i, j);
                    //Thread.Sleep(1000);
                }
            }







            boxes.AddBox(6, 6);
            boxes.AddBox(7, 7);
            boxes.AddBox(6, 8);
            boxes.AddBox(7, 9);
            boxes.AddBox(6, 10);
            boxes.AddBox(7, 11);
            boxes.AddBox(6, 6, 4);
            boxes.AddBox(6, 6.5);
            boxes.AddBox(7, 1);
            boxes.AddBox(6, 6);
            boxes.AddBox(8, 3);
            boxes.AddBox(9, 3);
            boxes.AddBox(6, 15);
            boxes.AddBox(6, 5.5);
            boxes.AddBox(3.7, 6.8, 6);
            boxes.AddBox(6, 6);
            boxes.AddBox(7, 7);
            boxes.AddBox(6, 8);
            boxes.AddBox(7, 9);
            boxes.AddBox(6, 10);
            boxes.AddBox(7, 11);
            boxes.AddBox(6, 6, 4);
            boxes.AddBox(6, 6.5);
            boxes.AddBox(7, 1);
            boxes.AddBox(6, 6);
            boxes.AddBox(8, 3);
            boxes.AddBox(9, 3);
            boxes.AddBox(6, 15);
            boxes.AddBox(6, 5.5);
            boxes.AddBox(1, 1, 5);
            boxes.AddBox(1.1, 1.1, 5);
            boxes.AddBox(1.3, 1.2, 5);
            boxes.AddBox(1.2, 1.2, 5);
            boxes.AddBox(2, 2, 5);
            boxes.AddBox(1.1, 2, 15);
            boxes.AddBox(1.8, 1.2, 1);
            boxes.AddBox(1.1, 1.9, 20);
            boxes.AddBox(1.9, 1.1, 20);
            boxes.AddBox(1, 1, 20);
            boxes.AddBox(1.9, 1.1, 20);
           


        }
    }
}
