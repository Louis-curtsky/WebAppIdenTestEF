using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIdenTestEF.Models;

namespace WebAppIdenTestEF.Identity
{
    public class AppUserStore:UserStore<ApplicationUser>
    {

        public AppUserStore(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}