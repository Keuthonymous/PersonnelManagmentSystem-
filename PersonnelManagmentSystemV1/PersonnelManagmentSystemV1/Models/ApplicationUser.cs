﻿using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManagmentSystemV1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public Department Department { get; set; }

        public virtual ICollection<Department> ManagedDepartments { get; set; }
        public virtual ICollection<CV> CVs { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; }

        public string GetDepartmentName() 
        { 
            if (Department != null)
            {
                return Department.Name;
            }
            return "";
        }

        public int GetMostRecentCVID()
        {
            if (CVs == null || CVs.Count() == 0)
                return 0;
            return CVs.OrderByDescending(c => c.UploadTime).FirstOrDefault().ID;
        }
    }

    
}