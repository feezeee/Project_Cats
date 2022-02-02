using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CatService : ICatService
    {
        IService myService { get; set; }

        public CatService(IService service)
        {
            myService = service;
        }

        public void AddCat(CatDTO catBLL)
        {    
            if(catBLL != null)
            {
                myService.Cats.Create(new CatDAL { Name = catBLL.Name, Price = catBLL.Price, DateOfBirth = catBLL.DateOfBirth });
                myService.Save();
            }
            
        }

        public void DeleteCat(int id)
        {
            myService.Cats.Delete(id);
            myService.Save();
        }


        public IEnumerable<CatDTO> GetCatBy(Func<CatDTO, bool> predicate)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CatDAL, CatDTO>()).CreateMapper();

            // Временный костыль c Where) Не получается разобраться с автомапером Func<>

            return mapper.Map<IEnumerable<CatDAL>, IEnumerable<CatDTO>>(myService.Cats.GetAll()).Where(predicate);
        }

        public IEnumerable<CatDTO> GetCats()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CatDAL, CatDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<CatDAL>, IEnumerable<CatDTO>>(myService.Cats.GetAll());
        }

        public void UpdateCat(CatDTO catBLL)
        {
            if(catBLL != null)
            {
                myService.Cats.Update(new CatDAL { Id = catBLL.Id, Name = catBLL.Name, Price = catBLL.Price, DateOfBirth = catBLL.DateOfBirth });
                myService.Save();
            }           
        }

        public CatDTO? FindCat(int id)
        {
            var cat = myService.Cats.Get(id);
            if(cat != null)
            {
                return new CatDTO { Id = cat.Id, DateOfBirth = cat.DateOfBirth, Name = cat.Name, Price = cat.Price };
            }
            return null;
        }
    }
}
