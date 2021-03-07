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
    public class UserRoleController : Controller
    {
        #region Constructor

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public UserRoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        #endregion

        // GET: UserRole
        public ActionResult Index()
        {
            List<UserRoleViewModel> userRoles = new List<UserRoleViewModel>();

            userRoles = (from user in userManager.Users.ToList()
                         select new UserRoleViewModel()
                         {
                             UserId = user.Id,
                             UserName = user.UserName,
                             Roles = (userManager.GetRolesAsync(user)).Result.ToList<string>()
                         }).ToList();

            return View(userRoles);
        }

        // GET: UserRole/Edit/5
        public ActionResult Edit(string userId)
        {
            var appUser = userManager.Users.SingleOrDefault(u => u.Id == userId);

            var role = (userManager.GetRolesAsync(appUser)).Result.ToList<string>();

            UserRoleViewModel user = new UserRoleViewModel();
            user.UserId = appUser.Id;
            user.UserName = appUser.UserName;
            user.Roles = role;

            ViewBag.Roles = roleManager.Roles.Select(r => r.Name).ToList();

            return View(user);
        }

        // POST: UserRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserRoleViewModel user)
        {
            var currentUser = userManager.Users.SingleOrDefault(u => u.Id == user.UserId);

            var currentRoles = (userManager.GetRolesAsync(currentUser)).Result.ToList<string>();

            //assign the new roles
            foreach (string role in user.Roles)
            {
                if (!userManager.IsInRoleAsync(currentUser, role).Result)
                {
                    //if he is not in that role then add 
                    IdentityResult result = await userManager.AddToRoleAsync(currentUser, role);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First().Description);

                        ViewBag.Roles = roleManager.Roles.Select(r => r.Name).ToList();
                        return View(user);
                    }
                }
            }

            //remove old roles
            foreach (var role in currentRoles)
            {
                if (!user.Roles.Contains(role))
                {
                    IdentityResult result = await userManager.RemoveFromRoleAsync(currentUser, role);
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}