using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ajax.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ajax.Web.Controllers
{
    public class PeopleController : Controller
    {
        private string _connectionString =
           @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var repo = new PeopleRepository(_connectionString);
            List<Person> people = repo.GetAll();
            return Json(people);
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.AddPerson(person);
            return Json(person);
        }
        [HttpPost]
      
        public IActionResult Delete(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePerson(id);
            return Json(id);
            
        }
        public IActionResult Edit(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.EditPerson(person); 
            return Json(person);
        }
    }
}







        