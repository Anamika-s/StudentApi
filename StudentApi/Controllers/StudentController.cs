using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepo _repo;
        // GET: api/<StudentController>
        public StudentController(IStudentRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            if (_repo.GetStudents() == null) { return BadRequest("There are no records"); }
            else
             return Ok(_repo.GetStudents());
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return  Ok(_repo.GetStudentById(id));
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
              var temp =  _repo.AddStudent(student);
            if (temp != null)
            {
                return Created("OK", student);
            }
            else
                return BadRequest("There was some error");
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            _repo.EditStudent(student, id);
            return Ok(student);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.DeleteStudent(id);
            return Ok();
        }
    }
}





