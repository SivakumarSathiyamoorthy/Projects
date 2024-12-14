using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAPI_CRUDOperations.Models;

namespace WebAPI_CRUDOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiController]
    //[Route("/")]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;
        public BrandController(BrandContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public DateTime GetBrands()
        {
            return DateTime.Now;

        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        //{
        //    if (_dbContext.Brands == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _dbContext.Brands.ToListAsync();

        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandByID(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null) { return NotFound(); }
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
           // return Ok(brand);
            return CreatedAtAction("GetBrandByID",new {id=brand.ID} ,brand);

        }

        [HttpPut]
        public async Task<ActionResult> PutBrand(int id, Brand brand)
        {
            if (_dbContext.Brands == null) { return NotFound(); }

            if (id != brand.ID) { return BadRequest(); }
            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id)) { return NotFound(); }
                else { throw; }
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Brands == null) { return NotFound(); }
            var brand=await _dbContext.Brands.FindAsync(id);
            if (brand==null) { return NotFound(); }
            _dbContext.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }

        private bool BrandAvailable(int id)
        {
            return (_dbContext.Brands?.Any(x => x.ID == id)).GetValueOrDefault();
        }

    }
}
