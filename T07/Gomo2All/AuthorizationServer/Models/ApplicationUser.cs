﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthorizationServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public string DataEventRecordsRole { get; set; }
        public string SecuredFilesRole { get; set; }
    }
    //new Claim(JwtClaimTypes.Role, "admin"),
    //new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"),
    //new Claim(JwtClaimTypes.Role, "dataEventRecords.user"),
    //new Claim(JwtClaimTypes.Role, "dataEventRecords"),
    //new Claim(JwtClaimTypes.Role, "securedFiles.user"),
    //new Claim(JwtClaimTypes.Role, "securedFiles.admin"),
    //new Claim(JwtClaimTypes.Role, "securedFiles")
}