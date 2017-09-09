using System;
using System.Data;

namespace refactor_me.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
               
        public Product()
        {          
        }

        public Product(DataRow row)
        {
            Id = Guid.Parse(row["Id"].ToString());
            Name = row["Name"].ToString();
            Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString();
            Price = decimal.Parse(row["Price"].ToString());
            DeliveryPrice = decimal.Parse(row["DeliveryPrice"].ToString());
        } 
    }
}