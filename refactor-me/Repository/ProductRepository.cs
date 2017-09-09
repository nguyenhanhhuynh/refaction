using refactor_me.Interface;
using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private string _insertQuery = @"Insert into Product(Id, Name, Description, Price, DeliveryPrice) Values(@Id, @Name, @Description, @Price, @DeliveryPrice);";                                    
        private string _updateQuery = @"Update product set name = @Name, description = @Description, price = @Price, deliveryprice = @DeliveryPrice where id = @Id";
        private string _deleteQuery = @"Delete product where id = @Id";
        private string _selectQuery = @"select id, name, description, price, deliveryprice from product";
        private string _selectByIdQuery = @"select id, name, description, price, deliveryprice from product where id = @Id";

        #region Interface IProductRepository
        public List<Product> Get(string name) {
         
            var query = _selectQuery;
            if (!string.IsNullOrEmpty(name))
            {
               query +=  $" where lower(name) like '%{name.ToLower()}%'";
            }
            return ExecuteSqlDataTable<Product>(query);
        }

        public Product GetById(Guid id)
        {
            Reset();
            AddParameter("@Id", System.Data.SqlDbType.UniqueIdentifier, id);          
            var result = ExecuteSqlDataTable<Product>(_selectByIdQuery);
            if (result.Count == 1)
                return result[0];
            return null;
        }

        public Product Update(Product obj) {
            if (GetById(obj.Id) == null)
                return null;
            AddCommonParameters(obj);
            ExecuteNonQuery(_updateQuery);
            return obj;
        }

        public Product Add(Product obj)
        {
            if (GetById(obj.Id) != null)
                return null;
            AddCommonParameters(obj);
            ExecuteNonQuery(_insertQuery);
            return obj;

        }

       public void Delete(Guid id)
        {
            Reset();
            AddParameter("@Id", System.Data.SqlDbType.UniqueIdentifier, id);
            ExecuteNonQuery(_deleteQuery);            
        }
        #endregion Interface IProductRepository

        #region Override BaseRepository
        protected override void AddCommonParameters(Product obj)
        {
            Reset();
            AddParameter("@Id", System.Data.SqlDbType.UniqueIdentifier, obj.Id);
            AddParameter("@Name", System.Data.SqlDbType.NVarChar, obj.Name);
            AddParameter("@Description", System.Data.SqlDbType.NVarChar, obj.Description);
            AddParameter("@Price", System.Data.SqlDbType.Decimal, obj.Price);
            AddParameter("@DeliveryPrice", System.Data.SqlDbType.Decimal, obj.DeliveryPrice);
        }
        #endregion BaseRepository
    }
}