using DEMO.data;
using DEMO.Models;
using DEMO.Models.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEMO.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeeController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()

        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Add(AddEmployeeviewModel AddEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = AddEmployeeRequest.Name,
                Email = AddEmployeeRequest.Email,
                Salary = AddEmployeeRequest.Salary,
                Department = AddEmployeeRequest.Department,
                DateOfBirth = AddEmployeeRequest.DateOfBirth

            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Index2(Guid id)
        {
            var employees = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employees != null)
            {
                var ViewModel = new UpdateEmployeeViewModel()
                {
                    Id = employees.Id,
                    Name = employees.Name,
                    Email = employees.Email,
                    Salary = employees.Salary,
                    Department = employees.Department,
                    DateOfBirth = employees.DateOfBirth

                };
                return await Task.Run(()=> View("Index2",ViewModel));
                    }


                return RedirectToAction("Index");
            }
        [HttpPost]
        public async Task<IActionResult> Index2 (UpdateEmployeeViewModel Model)
        {
            var employees = await mvcDemoDbContext.Employees.FindAsync(Model.Id);
            if(employees != null) 
            {
                employees.Name = Model.Name;
                employees.Email = Model.Email;
                employees.Salary = Model.Salary;
                employees.DateOfBirth = Model.DateOfBirth;
                employees.Department = Model.Department;
                 
           await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(UpdateEmployeeViewModel Model)
        {
            var employees = await mvcDemoDbContext.Employees.FindAsync(Model.Id);
            if (employees != null)
            {
               mvcDemoDbContext.Employees.Remove(employees);
                await mvcDemoDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
    }


