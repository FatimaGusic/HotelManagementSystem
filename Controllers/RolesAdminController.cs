using HSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data;

namespace HSM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesAdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: RolesAdmin
        public ActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View();
        }
        public ActionResult ListUsers()
        {
            var users = context.Users.OrderBy(u => u.UserName).ToList();
            return View(users);
        }
        //GET:


        public ActionResult Create()
        {
            return View();
        }

        //POST: RolesAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            IdentityResult ir = new IdentityResult();
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ir = rm.Create(new IdentityRole(collection["RoleName"]));

            if (ir.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = ir.Errors.ToString();
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult ManageUserRoles(FormCollection form)
        {
            UserRoleViewModel vm = new UserRoleViewModel();
            var userName = form["UserName"];

            var user = context.Users.Where(u => u.UserName == userName.Trim()).FirstOrDefault();
            if (user != null)
            {
                var roles = context.Roles.ToList();
                vm.UserName = userName;
                vm.UserId = user.Id;


                foreach (var item in roles)
                {
                    RoleAssignment roleAssigned = new RoleAssignment();
                    roleAssigned.Name = item.Name;
                    roleAssigned.Id = item.Id;
                    roleAssigned.isChecked = false;

                    if (user.Roles != null)
                    {
                        var roleIds = user.Roles.Select(r => r.RoleId).ToList();
                        if (roleIds.Contains(item.Id))
                        {
                            roleAssigned.isChecked = true;
                        }
                        else
                        {
                            roleAssigned.isChecked = false;
                        }
                    }
                    vm.UserRoles.Add(roleAssigned);
                }
            }
            else
            {
                vm = null;
            }

            return View(vm);

        }

        public ActionResult UpdateRoles(UserRoleViewModel updateRoles)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userId = updateRoles.UserId;


            foreach (var item in updateRoles.UserRoles)
            {
              

                if (item.isChecked)
                {
                    if(!userManager.IsInRole(userId, item.Id))
                    {
                        userManager.AddToRole(userId, item.Name);
                    }
                }
                else if (userManager.IsInRole(userId, "Name"))
                {
                    userManager.RemoveFromRoles(userId, item.Name);
                }
            }
            return RedirectToAction("Index");
        }
        
     
        

    }

}