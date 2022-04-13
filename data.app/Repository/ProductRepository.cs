using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data.app.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Api.Models;

namespace data.app.Repository
{
    public interface IProductRepository
    {
       IEnumerable<Product> GetAllProduct();
        Task<Product> GetProductById(int Id);

        Task<Product> AddProduct(Product product);

        Task<Product> UpdateProduct(Product product);

        void DeleteProduct(int employeeId);
        
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbcontext;

        public ProductRepository(ApplicationDbContext _dbContext)
        {
            this.dbcontext = _dbContext;
        }
        public async Task<Product> AddProduct(Product product)
        {
            
            try
            {
              
                if (product != null)
                {
                    var model = await dbcontext.Product.AddAsync(product);
                    await dbcontext.SaveChangesAsync();
                    return model.Entity;
                }
                return null;

                
            }
            catch (Exception ex)
            {
                throw new Exception($"Product{product.Name} does not added");
            }
        }

        public async void DeleteProduct(int productId)
        {
            try
            {
                var product= await dbcontext.Product.FirstOrDefaultAsync (p=>p.Id==productId);
                if (product != null)
                {
                    dbcontext.Product.Remove(product);
                    await dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Product does not Found");
            }
        }

        public IEnumerable<Product> GetAllProduct()
        {
            try
            {
                return  dbcontext.Product.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception($"Product does not Found");
            }
        }

        public async Task<Product> GetProductById(int Id)
        {
            return await dbcontext.Product.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async  Task<Product>UpdateProduct(Product product)
        {
            try
            {
                var prod = await dbcontext.Product.FirstOrDefaultAsync(x => x.Id == product.Id);
                if (prod != null)
                {
                    dbcontext.Product.Update(product);
                    await dbcontext.SaveChangesAsync();
                    return prod;
                }
                return null;
            }
            catch
            {
                throw new Exception($"Update Failed !");
            }
        }

      
    }
}
