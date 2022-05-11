using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
           public ProductController(StoreContext context)
            
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() => await _context.Products.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProduct(int id) 
        { 
        return await _context.Products.FindAsync(id);
        }
    }
}
