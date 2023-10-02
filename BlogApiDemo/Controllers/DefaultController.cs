using BlogApiDemo.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult EmployeeList()
        {
            using (var c=new Context())
            {
                var values = c.Employees.ToList();
                return Ok(values); 
            }
        }
        [HttpPost]
        public IActionResult EmployeeAdd(Employee e)
        {
            using (var c=new Context())
            {
                c.Employees.Add(e);
                c.SaveChanges();
                return Ok();
            }
        }
        [HttpGet("{id}")]
        public IActionResult EmployeeGet(int id)
        {
            using (var c=new Context())
            {
                var employee = c.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(employee);    
                }
            }
        }
        [HttpDelete]
        public IActionResult EmployeeDelete(int id)
        {
            using (var c=new Context())
            {
                var employee=c.Employees.Find(id);
                if (employee != null)
                {
                    c.Employees.Remove(employee);
                    c.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
        [HttpPut]
        public IActionResult EmployeeUpdate(Employee e)
        {
            using (var c = new Context())
            {
                var employee = c.Employees.FirstOrDefault(x=>x.ID==e.ID);
                if (employee != null)
                {
                    employee.Name=e.Name;
                    c.Employees.Update(employee);
                    c.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
