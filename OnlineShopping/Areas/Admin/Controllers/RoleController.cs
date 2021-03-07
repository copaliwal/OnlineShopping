using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Areas.Admin.ViewModels;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")] //[CP] We need to mention Are attribute other wise the routig will not work
    public class RoleController : Controller
    {
        #region Constructor

        private readonly RoleManager<IdentityRole> roleManager;
        
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        #endregion

        // GET: Role
        public ActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            var userRoles = new List<RoleViewModel>();

            foreach (var role in roles)
            {
                userRoles.Add(new RoleViewModel(role.Id, role.Name));
            }
            return View(userRoles);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel role)
        {
            if (!roleManager.RoleExistsAsync(role.Name).Result)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role.Name));

                if(!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().Description);
                    return View(role);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = roleManager.Roles.SingleOrDefault(r => r.Id == id);
            RoleViewModel userRole = new RoleViewModel(role.Id, role.Name);
            return View(userRole);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleViewModel role)
        {
            var identityRole = roleManager.Roles.FirstOrDefault(r => r.Id == role.RoleId);
            identityRole.Name = role.Name;
            identityRole.NormalizedName = role.Name;

            IdentityResult result = await roleManager.UpdateAsync(identityRole);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Errors.First().ToString());
                return View(role);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = roleManager.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel roleViewModel = new RoleViewModel(role.Id, role.Name);

            return View(roleViewModel);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var role = roleManager.Roles.FirstOrDefault(r => r.Id == id);

            IdentityResult result = await roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Errors.First().Description);
                return View(new RoleViewModel(role.Id, role.Name));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}