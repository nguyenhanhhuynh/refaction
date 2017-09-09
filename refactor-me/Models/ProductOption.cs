using System;
using System.Data;

namespace refactor_me.Models
{

    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public ProductOption() { }
        public ProductOption(DataRow row)
        {
            Id = Guid.Parse(row["Id"].ToString());
            Name = row["Name"].ToString();
            Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString();
            ProductId = Guid.Parse(row["ProductId"].ToString());
        }    
    }

}