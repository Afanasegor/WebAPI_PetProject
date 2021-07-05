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
    public class StudentsController : ControllerBase
    {
        private readonly IService<Student> studentService;

        public StudentsController(IService<Student> studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public List<Student> Get()
        {
            return studentService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            Student student = studentService.Get(id);
            return student;            
        }

        [HttpPost]
        public void Post(Student student)
        {
            studentService.Create(student);
            studentService.Save();
        }

        [HttpPut]
        public void Put(Student student)
        {
            studentService.Update(student);
            studentService.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            studentService.Delete(id);
            studentService.Save();
        }
    }
}
