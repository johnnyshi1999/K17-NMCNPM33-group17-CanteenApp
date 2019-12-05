using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    class Order : INotifyPropertyChanged
    {
        public string OrderID;
        public int number;
        public string Employee;

        public int orderSum = 0;
        public DateTime TimeCreated;
        public List<OrderDetail> detail;

        public int Quantity
        {
            get
            {
                int result = 0;
                for (int i = 0; i < detail.Count; i++)
                {
                    result += detail[i].quantity;
                }
                return result;
            }
        }

        public int OrderSum
        {
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



        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string attrib)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attrib));
        }

        public int Receive;
        public int Change
        {
            get
            {
                return Receive - OrderSum;
            }
        }

        public Order()
        {
            detail = new List<OrderDetail>();
        }


    }
}
