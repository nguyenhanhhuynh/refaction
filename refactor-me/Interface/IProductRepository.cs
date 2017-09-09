using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Interface
{
     public interface IProductRepository
    {
        List<Product> Get(string name );
        Product GetById(Guid id);
        Product Update(Product obj);
        Product Add(Product obj);
        void Delete(Guid id);
    }
}
