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
    public class LecturesController : ControllerBase
    {
        private readonly IService<Lecture> lectureService;

        public LecturesController(IService<Lecture> lectureService)
        {
            this.lectureService = lectureService;
        }

        [HttpGet]
        public List<Lecture> Get()
        {
            return lectureService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public Lecture Get(int id)
        {
            Lecture lecture = lectureService.Get(id);
            return lecture;
        }

        [HttpPost]
        public void Post(Lecture lecture)
        {
            lectureService.Create(lecture);
            lectureService.Save();
        }

        [HttpPut]
        public void Put(Lecture lecture)
        {
            lectureService.Update(lecture);
            lectureService.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            lectureService.Delete(id);
            lectureService.Save();
        }
    }
}
