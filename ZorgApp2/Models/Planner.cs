using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZorgApp2.Models;

namespace ZorgApp2.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ZorgApp2User class
    public class Planner : IdentityUser
    {
        public List<Bezoek> Bezoeken { get; set; }
    }
}
