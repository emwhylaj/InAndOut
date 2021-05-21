using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> itemList = _dbContext.Expenses;
            return View(itemList);
        }

        //GET Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> TypeDropDown = _dbContext.ExpenseTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.TypeDropDown = TypeDropDown;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _dbContext.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.Expenses.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET-Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Expense obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}