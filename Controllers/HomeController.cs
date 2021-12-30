using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Controllers
{
    [Authorize]
    public class HomeController : Controller

    {   

        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ILogger logger;
    //  private readonly IHostingEnvironment  hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository , IWebHostEnvironment hostEnvironment ,ILogger<HomeController> logger)
        {
            this.employeeRepository = employeeRepository;
            this.hostEnvironment = hostEnvironment;
            this.logger = logger;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {

            IEnumerable<Employee> model = employeeRepository.GetAllEmployees();
            return View(model);
        }


        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            // throw new Exception("Error in Details View");
            //logger.LogInformation("Information log");
            //logger.LogError("Error log");
            //logger.LogDebug("Debug log");
            //logger.LogWarning("Warning log");
            //logger.LogTrace("Trace log");
            //logger.LogCritical("Critical log");

            Employee employee = employeeRepository.GetEmployee(id.Value);

           
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View ("EmployeeNotFound" ,id.Value);
            }
                HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                
                
                Employee = employeeRepository.GetEmployee(id??1),
                PageTitle= "Employee Details"

            };
            
            
            

            return View(homeDetailsViewModel);
        }
        [HttpGet]
        
        public ViewResult Create()
        {
            return View();
        }

        //[HttpPost]

        //public ViewResult Delete(int id)
        //{
        //    Employee employee = employeeRepository.GetEmployee(id);
        //    if (employee != null)
        //    {
        //      employee.ToString().Remove(id);
                
        //        RedirectToAction("Index");
                
        //    }

        //    return View("Details");
        //}

        
        [HttpPost]
       
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid) {
                string UniqueFileName = ProcessUploadFile(model);
                Employee newemployee = new Employee { 
                  Name=model.Name,
                  Department=model.Department,
                  Email=model.Email,
                  PhotoPath= UniqueFileName
                };
                employeeRepository.Add(newemployee);

               return RedirectToAction("details", new { id = newemployee.Id });
            }
            return View();
        }
        [HttpGet]
       
        public ViewResult Edit(int id)
        {
            Employee employee = employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id =employee.Id,
                Name =employee.Name,
                Email =employee.Email,
                Department =employee.Department,
                ExistingPhotoPath =employee.PhotoPath

            };
            return View(employeeEditViewModel);

        }
        [HttpPost]
      
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filepath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filepath);
                    }
                    employee.PhotoPath = ProcessUploadFile(model);
                }
                
                employeeRepository.Update(employee);

                return RedirectToAction("details", new { id = employee.Id });
            }
            return View();
        }

        //public ViewResult Delete(int id)
        //{
        //    Employee employee = employeeRepository.GetEmployee(id);
           

        //    return View();
        //}

        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string UniqueFileName = null;
            if (model.Photo != null)
            {
                string UpladFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string FilePath = Path.Combine(UpladFolder, UniqueFileName);
                using(var filestream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Photo.CopyTo(filestream);
                }
                

            }

            return UniqueFileName;
        }
    }
}
