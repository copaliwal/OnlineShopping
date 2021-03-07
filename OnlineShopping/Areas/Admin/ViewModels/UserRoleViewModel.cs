using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Areas.Admin.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
