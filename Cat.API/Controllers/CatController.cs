using AutoMapper;
using BLL.Entities;
using BLL.Services;
using Cat.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;
        private readonly ICatService _catService;
        private readonly IMapper _mapper;

        public CatController(ILogger<CatController> logger, ICatService catService)
        {
            _logger = logger;
            _catService = catService;

            // Не очень нравится мне
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Entities.Cat, CatModel>()).CreateMapper();

        }

        // GET: api/<CatController>
        [HttpGet]
        public async Task<IEnumerable<CatModel>> GetAllCatsAsync()
        {
            var cats = await _catService.GetCatsAsync();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Entities.Cat, CatModel>()).CreateMapper();
            var newcats = _mapper.Map<IEnumerable<BLL.Entities.Cat>, List<CatModel>>(cats);
            return newcats;
        }

        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public async Task<CatModel> Get(int id)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Entities.Cat, CatModel>());
            //// Настройка AutoMapper
            //var mapper = new Mapper(config);
            // сопоставление
            var cat = _mapper.Map<CatModel>(await _catService.FindCatAsync(id));
            return cat;
        }

        // POST api/<CatController>
        [HttpPost]
        public async Task Post([FromBody] CatModel cat)
        {
            await _catService.AddCatAsync(new BLL.Entities.Cat { Name = cat.Name, DateOfBirth = cat.DateOfBirth, Price = cat.Price });
        }

        // PUT api/<CatController>
        [HttpPut]
        public async Task Put([FromBody] CatModel cat)
        {
            await _catService.UpdateCatAsync(new BLL.Entities.Cat { Id = cat.Id, Name = cat.Name, DateOfBirth = cat.DateOfBirth, Price = cat.Price });
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _catService.DeleteCatAsync(id);
        }
    }
}
