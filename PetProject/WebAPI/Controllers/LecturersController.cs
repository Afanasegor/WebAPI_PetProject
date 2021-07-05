using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Service;
using CustomExceptions;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly IService<Lecturer> lecturerService;

        public LecturersController(IService<Lecturer> lecturerService)
        {
            this.lecturerService = lecturerService;
        }

        [HttpGet]
        public List<Lecturer> Get()
        {
            return lecturerService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public Lecturer Get(int id)
        {
            Lecturer lecturer = lecturerService.Get(id);
            return lecturer;
        }

        [HttpPost]
        public void Post(Lecturer lecturer)
        {
            lecturerService.Create(lecturer);
            lecturerService.Save();
        }

        [HttpPut]
        public void Put(Lecturer lecturer)
        {
            lecturerService.Update(lecturer);
            lecturerService.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            lecturerService.Delete(id);
            lecturerService.Save();
        }
    }
}
