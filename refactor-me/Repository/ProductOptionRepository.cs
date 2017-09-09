using refactor_me.Interface;
using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Repository
{
    public class ProductOptionRepository : BaseRepository<ProductOption>, IProductOptionRepository
    {
        private string _insertQuery = @"Insert into ProductOption(id, productid, name, description) Values(@Id, @ProductId, @Name, @Description);";
        private string _updateQuery = @"Update ProductOption set name = @Name, description = @Description where id = @Id";
        private string _deleteQuery = @"Delete ProductOption where id = @Id";
        private string _deleteByProductIdbQuery = @"Delete ProductOption where productId=@ProductId";
        private string _selectQuery = @"select id, name, productid, description from ProductOption where productid=@ProductId";
        private string _selectByIdQuery = @"select id, name, description, productid from ProductOption where id = @Id";

        #region IProductOptionRepository
        public List<ProductOption> Get(Guid productId)
        {          
            Reset();
            AddParameter("@ProductId", System.Data.SqlDbType.UniqueIdentifier, productId);
            return ExecuteSqlDataTable<ProductOption>(_selectQuery);
        }

        public ProductOption GetById(Guid id)
        {
            Reset();
            AddParameter("@Id", System.Data.SqlDbType.UniqueIdentifier, id);
            var result = ExecuteSqlDataTable<ProductOption>(_selectByIdQuery);
            if (result.Count == 1)
                return result[0];
            return null;
        }

        public ProductOption Update(ProductOption obj)
        {
            if (GetById(obj.Id) == null)
                return null;
            AddCommonParameters(obj);
            ExecuteNonQuery(_updateQuery);
            return obj;
        }

        public ProductOption Add(ProductOption obj)
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

        public void DeleteByProductId(Guid productId)
        {
            Reset();
            AddParameter("@ProductId", System.Data.SqlDbType.UniqueIdentifier, productId);
            ExecuteNonQuery(_deleteByProductIdbQuery);
        }
        #endregion IProductOptionRepository

        #region Override BaseRepository
        protected override void AddCommonParameters(ProductOption obj)
        {
            Reset();
            AddParameter("@Id", System.Data.SqlDbType.UniqueIdentifier, obj.Id);
            AddParameter("@Name", System.Data.SqlDbType.NVarChar, obj.Name);
            AddParameter("@Description", System.Data.SqlDbType.NVarChar, obj.Description);
            AddParameter("@ProductId", System.Data.SqlDbType.UniqueIdentifier, obj.ProductId);
        }
        #endregion Override BaseRepository
    }
}