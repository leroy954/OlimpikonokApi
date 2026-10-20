using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokApi.DTOs;
using OlimpikonokApi.Models;

namespace OlimpikonokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportoloController : ControllerBase
    {
        [HttpGet("OrszagSportag")]
        public IActionResult GetOrszagSportag()
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    List<SportoloOrszagSportagDTO> result = new OlimpikonokContext().Sportolos.Include(s => s.Orszag).Include(s => s.Sportag)
                        .Select(s => new SportoloOrszagSportagDTO()
                        {
                            Id = s.Id,
                            Nev = s.Nev,
                            Sportagnev = s.Sportag.Megnevezes,
                            Orszagnev = s.Orszag.Nev
                        }).ToList();
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Hiba történt: " + ex.Message);
            }
        }
    }
}
