using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Project_Cats_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Cats_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;
        private readonly IServiceManager _serviceManager;

        public CatController(ILogger<CatController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        // GET: api/<CatController>
        [HttpGet]
        public IEnumerable<CatModel> GetAllCats()
        {
            var cats = _serviceManager.catService.GetCats();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CatDTO, CatModel>()).CreateMapper();
            var newcats = mapper.Map<IEnumerable<CatDTO>, List<CatModel>>(cats);
            return newcats;
        }

        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public CatModel Get(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CatDTO, CatModel>());
            // Настройка AutoMapper
            var mapper = new Mapper(config);
            // сопоставление
            var cat = mapper.Map<CatModel>(_serviceManager.catService.FindCat(id));
            return cat;
        }

        // POST api/<CatController>
        [HttpPost]
        public void Post([FromBody] CatModel cat)
        {
            _serviceManager.catService.AddCat(new CatDTO { Id = cat.Id, Name = cat.Name, DateOfBirth = cat.DateOfBirth, Price = cat.Price });
        }

        // PUT api/<CatController>
        [HttpPut]
        public void Put([FromBody] CatModel cat)
        {
            _serviceManager.catService.UpdateCat(new CatDTO { Id = cat.Id, Name = cat.Name, DateOfBirth = cat.DateOfBirth, Price = cat.Price });
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _serviceManager.catService.DeleteCat(id);
        }
    }
}
