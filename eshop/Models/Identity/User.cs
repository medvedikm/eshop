using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public virtual string Name {get; set;}
        public virtual string LastName {get; set;}
        public double Sleva { get; set; }
        public bool IsStudent { get; set; }
    }
}
