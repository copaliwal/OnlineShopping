using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {

        }
        public RoleViewModel(string id, string name)
        {
            this.RoleId = id;
            this.Name = name;
        }
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
}
