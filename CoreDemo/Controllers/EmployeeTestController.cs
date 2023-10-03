using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class EmployeeTestController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44379/api/Default");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString);
            return View(values);
        }
        public IActionResult AddEmployee()
        {
            return View();  
        }
        [HttpPost]
        public async Task< IActionResult> AddEmployee(Class1 p)
        {
            var httpClient= new HttpClient();
            var jsonEmployee=JsonConvert.SerializeObject(p);
            StringContent stringContent = new StringContent(jsonEmployee,Encoding.UTF8,"application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44379/api/Default", stringContent);
            if(responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View(p);
        }
      
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44379/api/Default/"+id);
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee=await responseMessage.Content.ReadAsStringAsync(); 
                var values=JsonConvert.DeserializeObject<Class1>(jsonEmployee);
                return View(values);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(Class1 class1)
        {
           var  httpClient=new HttpClient();
            var jsonEmployee= JsonConvert.SerializeObject(class1);
            StringContent stringContent = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:44379/api/Default/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View(class1);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:44379/api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        } 
    }
    public class Class1
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


}
