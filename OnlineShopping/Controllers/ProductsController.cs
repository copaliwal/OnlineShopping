using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.ModelView;
using OnlineShopping.Primary.Interfaces;

namespace OnlineShopping.Controllers
{
    public class ProductsController : Controller
    {
        #region constructor & DI
        private readonly IProductRepository productRepository;
        private readonly IRepository<Category> categoryRepository;

        public ProductsController(IProductRepository productRepository, IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }
        #endregion

        // GET: Products
        public IActionResult Index(int? id)
        {
            IEnumerable<Product> products;
            if (id == null)
            {
                products = productRepository.GetAll();
            }
            else
            {
                products = productRepository.GetByCategoryId(Convert.ToInt32(id));
            }

            return View(products);
        }

        // GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _productRepository.GetById(Convert.ToInt32(id));
            var product = productRepository.GetById(Convert.ToInt32(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var categories = categoryRepository.GetAll();

            foreach (var category in categories)
            {
                list.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.CategoryId.ToString()
                });
            }

            ViewBag.Categories = list;

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProductId,Name,ImageUrl,Details,Price,CatedoryId")] Product product)
        public IActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductImage != null)
                {
                    string fileName = Path.GetFileName(product.ProductImage.FileName);

                    var path = Path.Combine(
                          Directory.GetCurrentDirectory(), "wwwroot",
                          product.ProductImage.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        product.ProductImage.CopyTo(stream);
                    }

                    Product newProduct = new Product()
                    {
                        Name = product.Name,
                        Details = product.Details,
                        CatedoryId = product.CatedoryId,
                        Price = product.Price,
                        ImageUrl = "/Images/" + fileName
                    };

                    productRepository.Add(newProduct);

                    return RedirectToAction("Details/" + newProduct.ProductId);
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _productRepository.GetById(Convert.ToInt32(id));
            var product = productRepository.GetById(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ImageUrl,Details,Price,CatedoryId")] Product product)
        public IActionResult Edit(int id, [Bind("ProductId,Name,ImageUrl,Details,Price,CatedoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
