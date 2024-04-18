using BankBranches.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankBranches.Controllers
{
    public class BankController : Controller
    {
        static List<BankBranch> branches = new List<BankBranch>()

    {
        new BankBranch {id= 1,name="KFH Bank", location="Mishref",branchManager="Awdhah",employeeCount=30} ,
        new BankBranch {id = 2,name="KFH Bank", location="Bayan",branchManager="Fatma",employeeCount=40}
    };
        public IActionResult Index()
        {
            return View(branches);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(NewBranchForm model)
        {
            if (ModelState.IsValid)
            {
                
                var name = model.name;
                var location = model.location;
                var manager = model.branchManager;
                var employeecount = model.employeeCount;
                var newBranch = new BankBranch();
                newBranch.name = name;
                newBranch.location = location;
                newBranch.branchManager = manager;
                newBranch.employeeCount = employeecount;
                branches.Add(newBranch);
                return RedirectToAction("Index");
            }
            return View("New");

        }
        public IActionResult Details(int id)
        {
            var branch = branches.SingleOrDefault(a => a.id == id);
            if (branches == null)
            {
                return RedirectToAction("Index");

            }
            return View(branch);
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
