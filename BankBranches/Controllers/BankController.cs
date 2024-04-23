using BankBranches.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BankBranches.Controllers
{
    public class BankController : Controller

    {

            private readonly BankContext _context;

            public BankController(BankContext context)
            {
                _context = context;
            }
            static List<BankBranch> branches = new List<BankBranch>()

    {
        new BankBranch {id= 1,name="KFH Bank", location="Mishref",branchManager="Awdhah",employeeCount=30} ,
        new BankBranch {id = 2,name="KFH Bank", location="Bayan",branchManager="Fatma",employeeCount=40}
    };
        
            public IActionResult Index()
            {
                var viewModel = new BankDashboardViewModel();

                viewModel.TotalBranches = _context.BankBranches.Count();
                viewModel.TotalEmployees = _context.employees.Count();
                viewModel.BranchWithMostEmployees = _context.BankBranches
                    .OrderByDescending(b => b.employees.Count)
                    .FirstOrDefault();
                viewModel.BranchList = _context.BankBranches.ToList();

                return View(viewModel);
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

                using (_context)
                {
                    _context.BankBranches.Add(newBranch);
                    _context.SaveChanges();
                }


                return RedirectToAction("Index");

            }
            return View("New", model);

        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var branch = branches.SingleOrDefault(a => a.Id == id);
            //if (branches == null)
            //{
            //    return RedirectToAction("Index");

            //}
            BankBranch branch = null;
            using (_context)
            {
                branch = _context.BankBranches.Include(a=> a.employees).FirstOrDefault(b => b.id == id);
            }
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        private bool BranchExists(int id)
        {
            using (_context)
            {
                return _context.BankBranches.Any(e => e.id == id);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BankBranch branch;
            using (_context)
            {
                branch = _context.BankBranches.FirstOrDefault(b => b.id == id);
                if (branch == null)
                {
                    return NotFound();
                }

                return View(branch);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BankBranch branch)
        {
            if (id != branch.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (_context)
                    {
                        _context.Update(branch);
                        _context.SaveChanges();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }


        public IActionResult Register()
        {
            return View();
        }

    [HttpGet]
    public IActionResult AddEmployee(int id)
    {
        using (_context)
        {
            var branch = _context.BankBranches.FirstOrDefault(b => b.id == id);
            if (branch == null)
            {
                return NotFound();
            }

            ViewBag.BranchId = id;
            return View();
        }
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(int id, AddEmployeeForm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (_context)
            {
                var branch = _context.BankBranches.Include(b => b.employees).FirstOrDefault(b => b.id == id);
                if (branch == null)
                {
                    ModelState.AddModelError("", $"Branch with ID {id} not found.");
                    return View(model);
                }

                if (_context.employees.Any(e => e.CivilId == model.CivilId))
                {
                    ModelState.AddModelError("CivilId", "Duplicate CivilId");
                    return View(model);
                }

                var employee = new Employee
                {
                    Name = model.Name,
                    Position = model.Position,
                    CivilId = model.CivilId,
                    BankBranchId = id
                };

                branch.employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = id });
            }
        }

        public IActionResult Charts()
        {
            var branchData = _context.BankBranches.Select(b => new { BranchName = b.name, EmployeeCount = b.employeeCount }).ToList();
            return View(branchData);
        }


        //if (!ModelState.IsValid)
        //{
        //    if (ModelState.ContainsKey("CivilId") && ModelState["CivilId"].Errors.Any(e => e.ErrorMessage == "Duplicate CivilId"))
        //    {
        //        ModelState.AddModelError("", "A duplicate Civil ID was provided.");
        //    }
        //    return View(model);
        //}

    }

}



        
