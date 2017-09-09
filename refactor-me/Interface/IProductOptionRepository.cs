using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Interface
{
    public interface IProductOptionRepository
    {      
        List<ProductOption> Get(Guid productId);
        ProductOption GetById(Guid id);
        ProductOption Update(ProductOption obj);
        ProductOption Add(ProductOption obj);
        
        void Delete(Guid id);
        void DeleteByProductId(Guid productId);
    }
}
