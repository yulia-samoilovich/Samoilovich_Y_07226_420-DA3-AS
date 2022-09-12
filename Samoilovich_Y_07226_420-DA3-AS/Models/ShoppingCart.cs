using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samoilovich_Y_07226_420_DA3_AS.Utils;

namespace Samoilovich_Y_07226_420_DA3_AS.Models
{
    public class ShoppingCart : IModel<ShoppingCart>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOrdered{ get; set; }
        public DateTime DateShipped { get; set; }
        public ShoppingCart Delete()
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetById()
        {
            throw new NotImplementedException();
        }

        public ShoppingCart Insert()
        {
            if(this.Id == 0)
            {
                throw new Exception("Fetching of objects should already have an Id");
            }
            
            using (SqlConnection sqlConnection = DbUtils<SqlConnection>.GetDefaultConnection())
            {
                string insertCommandText = "Insert into dbo.ShoppingCart (customerId " + "billingAddress, " +
                    "shippingAddress, dateCreated) VALUES (" +
                    this.CustomerId + ", " +
                    this.BillingAddress + ", " +
                    this.ShippingAddress + ", " +
                    this.DateCreated + ", ";

            }
                
        }

        public ShoppingCart Update()
        {
            throw new NotImplementedException();
        }
    }
}
