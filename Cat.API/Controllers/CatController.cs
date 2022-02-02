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

        public CatController(ILogger<CatController> logger, ICatService catService)
        {
            _logger = logger;
            _catService = catService;
        }

        // GET: api/<CatController>
        [HttpGet]
        public IEnumerable<CatModel> GetAllCats()
        {
            var cats = _catService.GetCats();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Entities.Cat, CatModel>()).CreateMapper();
           
            var newcats = mapper.Map<IEnumerable<BLL.Entities.Cat>, List<CatModel>>(cats);
            return newcats;
        }

        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public CatModel Get(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Entities.Cat, CatModel>());
            // Настройка AutoMapper
            var mapper = new Mapper(config);
            // сопоставление
            var cat = mapper.Map<CatModel>(_catService.FindCat(id));
            return cat;
        }

        // POST api/<CatController>
        [HttpPost]
        public void Post([FromBody] CatModel cat)
        {
            _catService.AddCat(new BLL.Entities.Cat { Id = cat.Id, Name = cat.Name, DateOfBirth = cat.DateOfBirth, Price = cat.Price });
        }

        // PUT api/<CatController>
        [HttpPut]
        public void Put([FromBody] CatModel cat)
        {
            _catService.UpdateCat(new BLL.Entities.Cat { Id = cat.Id, Name = cat.Name, DateOfBirth = cat.DateOfBirth, Price = cat.Price });
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _catService.DeleteCat(id);
        }
    }
}
