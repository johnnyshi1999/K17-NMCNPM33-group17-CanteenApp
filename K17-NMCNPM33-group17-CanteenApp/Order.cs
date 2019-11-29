using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace K17_NMCNPM33_group17_CanteenApp
{
    class OrderDetail
    {
        public Product product;
        public int quantity;
    }

    class Order
    {
        public string OrderID;
        public int number;
        string Employee;

        public DateTime TimeCreated;
        public List<OrderDetail> detail;

        int Sum {
            get
            {
                int result = 0;
                for (int i = 0; i < detail.Count; i++)
                {
                    result += detail[i].quantity * detail[i].product.price;
                }
                return result;
            }
            }
        int Receive;
        int Change;

        public Order()
        {
            detail = new List<OrderDetail>();
        }


    }
}
