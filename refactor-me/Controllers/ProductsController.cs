using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using System.Collections.Generic;
using refactor_me.Helper;
using System.Web;
using refactor_me.Interface;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {

        private IProductRepository _productRepository;
        private IProductOptionRepository _productOptionRepository;
        public ProductsController(IProductRepository productRespository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRespository;
            _productOptionRepository = productOptionRepository;
        }

        [Route("")]
        public List<Product> Get()
        {
            var name = HttpHelpers.GetQueryString(HttpContext.Current.Request.QueryString, "name");
            return _productRepository.Get(name);
        }

        [Route("{id}")]
        public Product Get(Guid id)
        {         
            var product = _productRepository.GetById(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return product;
        }

        [Route("")]
        [HttpPost]
        public Product Post([FromBody]Product product)
        {
            if (_productRepository.Add(product)==null)
                throw new HttpResponseException(HttpStatusCode.Found);
            return product;
        }

        [Route("{id}")]
        [HttpPut]
        public Product Put(Guid id, [FromBody]Product product)
        {
            if (product.Id == Guid.Empty)
                product.Id = id;
            if ( _productRepository.Update(product)==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return product;
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productOptionRepository.DeleteByProductId(id);
            _productRepository.Delete(id);     
        }

        [Route("{productId}/options")]
        [HttpGet]
        public List<ProductOption> GetOptions(Guid productId)
        {
            return _productOptionRepository.Get(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = _productOptionRepository.GetById(id);
          if (option==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public ProductOption CreateOption(Guid productId, [FromBody]ProductOption option)
        {
            var returnValue = _productOptionRepository.Add(option);
            if (returnValue == null)
                throw new HttpResponseException(HttpStatusCode.Found);
            return returnValue;
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public ProductOption UpdateOption(Guid productId, Guid id, [FromBody]ProductOption option)
        {
            if (option.Id == Guid.Empty)
                option.Id = id;
            if (option.ProductId == Guid.Empty)
                option.ProductId = productId;
            var returnValue = _productOptionRepository.Update(option);
            if (returnValue == null)
                throw new HttpResponseException(HttpStatusCode.Found);
            return returnValue;
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            _productOptionRepository.Delete(id);       
        }
    }
}
