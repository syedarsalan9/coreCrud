using CRUDCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CRUDCoreMVC.Models;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Web.Mvc;

namespace CRUDCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeContext _dbContext;

        public HomeController(ILogger<HomeController> logger, EmployeeContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var _emplst = _dbContext.tblEmployees.
                            Join(_dbContext.tblSkills, e => e.SkillID, s => s.SkillID,
                            (e, s) => new EmployeeViewModel
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                PhoneNumber = e.PhoneNumber,
                                Skill = s.Title,
                                YearsExperience = e.YearsExperience
                            }).ToList();
            IList<EmployeeViewModel> emplst = _emplst;
            return View();
        }

        public List<EmployeeCustomModel> Employee()
        {
            var _employeeList = _dbContext.tblEmployees
                                .Join(_dbContext.tblSkills, e => e.SkillID, s => s.SkillID, (e, s) => new EmployeeCustomModel
                                {
                                    EmployeeId = e.EmployeeID,
                                    EmployeeName = e.EmployeeName,
                                    PhoneNumber = e.PhoneNumber,
                                    skillName = s.Title,
                                }).ToList();
            
            return _employeeList;
        }

        public List<EmployeeCustomModel> EmployeeLst()
        {
            var employee = _dbContext.tblEmployees
                            .Join(_dbContext.tblSkills, e => e.SkillID, s => s.SkillID, (e, s) => new EmployeeCustomModel {
                                EmployeeId = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                PhoneNumber = e.PhoneNumber,
                                skillName = s.Title,
                            }).Where(e => e.EmployeeId == 4).ToList();
            return employee;
        }

        public List<tblEmployees> EmployeesFilter()
        {
            
            int id = 4;
            int LocationId = 1;
            int PayrollNo = 10;
            int rowVersion = 10;
            
            var result = this._dbContext.tblEmployees.Where(x => x.EmployeeID == id);
            if (LocationId != null)
                result = result.Where(x => x.EmployeeName.Contains(Convert.ToString(LocationId)));
            if (PayrollNo > 0)
                result = result.Where(x => x.SkillID == PayrollNo);
            if (rowVersion != null)
                result = result.Where(x => x.YearsExperience > rowVersion);

            return result.ToList();
        }
        public List<tblEmployees> EmployeesList()
        {
            var _ListOfEmployees = _dbContext.tblEmployees.ToList();
            return _ListOfEmployees;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ExportTeamMembersInExcel(string UserName, long? RoleId, string[] UserIds)
        {
            MemoryStream stream = new MemoryStream();
            FileContentResult robj;

            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable data = _dbContext.tblEmployees.ToList();
                wb.Worksheets.Add(data, "TeamMembers");
                using (stream)
                {
                    wb.SaveAs(stream);
                }
            }
            robj = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "TeamMembers.xlsx");
            return Json(robj, JsonRequestBehavior.AllowGet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
