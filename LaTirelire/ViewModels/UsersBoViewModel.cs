using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaTirelire.ViewModels
{
    public class UsersBoViewModel
    {
        [Key]
        public string id { get; set; }

        public string email { get; set; }
        public bool responsable { get; set; }
        public bool assistant { get; set; }
        public bool moderateur { get; set; }
        public bool admin { get; set; }


    }
}