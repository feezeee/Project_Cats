using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Cats_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;

        public CatController(ILogger<CatController> logger)
        {
            _logger = logger;
        }

        // GET: api/<CatController>
        [HttpGet]
        public IEnumerable<string> GetAllCats()
        {
            return new string[] { "котик 1", "котик 2" };
        }

        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<CatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
