using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.Primary.Interfaces;

namespace OnlineShopping.Controllers
{
    public class CategoriesController : Controller
    {
        #region Constructor
        private readonly IRepository<Category> categoryRepository;

        public CategoriesController(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        #endregion

        // GET: Categories
        public IActionResult Index()
        {
            return View(categoryRepository.GetAll());
        }

        // GET: Categories/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await categoryRepository.GetDetails(Convert.ToInt32(id));
            //var category = categoryRepository.GetDetails(Convert.ToInt32(id));
            var category = categoryRepository.GetById(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category)
        public IActionResult Create([Bind("CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                //await categoryRepository.AddCategory(category);
                categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await categoryRepository.GetDetails(Convert.ToInt32(id));
            var category = categoryRepository.GetById(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category)
        public IActionResult Edit(int id, [Bind("CategoryId,Name")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //await categoryRepository.UpdateCategory(category);
                categoryRepository.Update(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await categoryRepository.GetDetails(Convert.ToInt32(id));
            var category = categoryRepository.GetById(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        public IActionResult DeleteConfirmed(int id)
        {
            var category = categoryRepository.GetById(id);
            categoryRepository.Delete(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
