using data.app.Models;
using data.app.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.App.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSide.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private IProductRepository iproductRepository;

        public ProductController(IProductRepository _productRepository)
        {
            this.iproductRepository = _productRepository;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ProductViewModel> model = iproductRepository.GetAllProduct().Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Type = s.Type
            }); ;
            return Ok(model);
        }


        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                var result = await iproductRepository.GetProductById(id);

                if (result == null)
                {
                    return NotFound();
                }
                {
                    model.Id = result.Id;
                    model.Name = result.Name;
                    model.Type = result.Type;
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> AddProduct([FromBody] ProductViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid inputs .please try agaim !");
                }
                Product p = new Product();
                {
                    p.Name = model.Name;
                    p.Type = model.Type;
                }
                await iproductRepository.AddProduct(p);
                return Ok(model);

                //if (ModelState.IsValid)
                //{


                //}
                //return BadRequest("product field does not valid!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new product record");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductViewModel>> Put(int id, [FromBody] ProductViewModel model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest("Product ID mismatch");

                var productToUpdate = await iproductRepository.GetProductById(model.Id);

                if (productToUpdate == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }
                await iproductRepository.UpdateProduct(new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Type = model.Type
                });
                return Ok(model);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public  void DeleteEmployee(int id)
        {
            try
            {
                var productToDelete = iproductRepository.GetProductById(id);

                if (productToDelete == null)
                {
                     NotFound($"Employee with Id = {id} not found");
                }
                 iproductRepository.DeleteProduct(productToDelete.Id);
                
            }
            catch (Exception)
            {
                 StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    
    }
}
