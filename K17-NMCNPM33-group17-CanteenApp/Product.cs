﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K17_NMCNPM33_group17_CanteenApp
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int price { get; set; }
        public int type { get; set; }
        public string AvatarLink;
        public static int typeStringToInt(string sType)
        {
            if (sType == "Món ăn")
                return 1;
            if (sType == "Thức uống")
                return 2;
            if (sType == "Snack")
                return 3;
            return 0;
        }
    }
}
