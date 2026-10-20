using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokApi.Models;
using OlimpikonokApi.Services;

namespace OlimpikonokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            try
            {
                List<Orszag> result = new OrszagService().GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba történt a kérés teljesítése közben. " + ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    Orszag result = context .Orszags.FirstOrDefault(o => o.Id == id);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return NotFound($"Nincs ilyen azonosítójú ország ({id})");
                }


            }
            catch (Exception ex)
            {
                return BadRequest("Hiba történt a kérés teljesítése közben. " + ex.Message);
            }
        }
    }
}