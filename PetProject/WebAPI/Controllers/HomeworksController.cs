using CustomExceptions;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly IService<Homework> homeworkService;

        public HomeworksController(IService<Homework> homeworkService)
        {
            this.homeworkService = homeworkService;
        }

        [HttpGet]
        public List<Homework> Get()
        {
            return homeworkService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public Homework Get(int id)
        {
            Homework homework = homeworkService.Get(id);
            return homework;    
        }

        [HttpPost]
        public void Post(Homework homework)
        {
            homeworkService.Create(homework);
            homeworkService.Save();
        }

        [HttpPut]
        public void Put(Homework homework)
        {
            homeworkService.Update(homework);
            homeworkService.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            homeworkService.Delete(id);
            homeworkService.Save();
        }
    }
}
