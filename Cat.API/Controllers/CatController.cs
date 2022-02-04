using AutoMapper;
using BLL.Entities;
using BLL.Services;
using Cat.API.Request;
using Cat.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Cat.API.Middleware.Authorize]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;
        private readonly ICatService _catService;
        private readonly IMapper _mapper;

        public CatController(ILogger<CatController> logger, ICatService catService, IMapper mapper)
        {
            _logger = logger;
            _catService = catService;
            _mapper = mapper;

        }

        // GET: api/<CatController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var cats = _catService.Get();
                if (cats.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(cats);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cat = await _catService.GetById(id);
                if(cat == null)
                {
                    return NoContent();
                }
                return Ok(cat);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        // POST api/<CatController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCatRequest cat)
        {
            try
            {
                await _catService.Create(_mapper.Map<BLL.Entities.Cat>(cat));
                return Ok("Котик добавлен )");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CatController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutCatRequest cat)
        {
            try
            {
                await _catService.Update(_mapper.Map<BLL.Entities.Cat>(cat));
                return Ok("Котик изменен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CatController>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCatRequest cat)
        {
            try
            {
                await _catService.Delete(_mapper.Map<BLL.Entities.Cat>(cat));
                return Ok("Котик удален :(");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
