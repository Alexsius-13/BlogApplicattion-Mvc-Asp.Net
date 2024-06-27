﻿using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository, 
			UserManager<IdentityUser> userManager)
        {
			this.userRepository = userRepository;
            this.userManager = userManager;
        }

		[HttpGet]
        public async Task<IActionResult> List()
		{
			var users = await userRepository.GetAll();
			var userViewModel = new UserViewModel();
			userViewModel.Users = new List<User>();

			foreach (var user in users)
			{
				userViewModel.Users.Add(new Models.ViewModels.User
				{
					Id = Guid.Parse(user.Id),
					Username = user.UserName,
					Email = user.Email,
				});
			}

			return View(userViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> List(UserViewModel request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}

			var identityUser = new IdentityUser
			{
				UserName = request.Username,
				Email = request.Email
			};

			var identityResult = await userManager.CreateAsync(identityUser, request.Password);
			if(identityResult is not null)
			{
				if (identityResult.Succeeded)
				{
					var roles = new List<string> { "User" };

					if(request.AdminRoleCheckBox)
					{
						roles.Add("Admin");
					}

					identityResult = await userManager.AddToRolesAsync(identityUser, roles);

					if(identityResult is not null && identityResult.Succeeded)
					{
						return RedirectToAction("List", "AdminUsers");
					}
				}
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser != null && currentUser.Id == id.ToString())
            {
                ViewData["ErrorMessage"] = "You cannot delete yourself.";
                return View("Error");
            }

            var userToDelete = await userManager.FindByIdAsync(id.ToString());

            if (userToDelete == null)
            {
                ViewData["ErrorMessage"] = "User not found.";
                return View("Error");
            }

            
            var isUserToDeleteAdmin = await userManager.IsInRoleAsync(userToDelete, "Admin");

            
            var isCurrentUserSuperAdmin = await userManager.IsInRoleAsync(currentUser, "SuperAdmin");

            if (isUserToDeleteAdmin && !isCurrentUserSuperAdmin)
            {
                ViewData["ErrorMessage"] = "Only superadmins can delete other admins.";
                return View("Error");
            }

            var identityResult = await userManager.DeleteAsync(userToDelete);

            if (identityResult != null && identityResult.Succeeded)
            {
                return RedirectToAction("List", "AdminUsers");
            }

            ViewData["ErrorMessage"] = "User could not be deleted.";
            return View("Error");
        }
	}
}
