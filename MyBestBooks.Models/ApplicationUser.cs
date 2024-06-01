using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;

namespace MyBestBooks.Models
{
    public class ApplicationUser : IdentityUser // that's if we need to modify some informations for the user (for example)
                                                // in the identity tables. like adding some information that don't exist.
                                                // to do that, we need to create that new model
    {
        [Required]
        public int Name { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
   
}
//  now we have to add a mapper for that to can add that to migration. (in the ApplicationDBContext)
