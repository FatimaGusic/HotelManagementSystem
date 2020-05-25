using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSM.Models
{
    public class RoleAssignment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool isChecked { get; set; }

    }

    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            this.UserRoles = new List<RoleAssignment>();

        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleAssignment> UserRoles {get; set;}

    }


}