using System;
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
        /// <summary>
        /// 國碼
        /// </summary>
        public string alpha3 { get; set; }
        /// <summary>
        /// 語系
        /// </summary>
        public string lang { get; set; }
        /// <summary>
        /// 假刪除
        /// </summary>
        public int DelFlag { get; set; }
    }
    //最高管理者  Admin 1
    //代理商     Agent 2
    //經銷商     Dealer 3
    //車廠管理者 ShopManager 4
    //車廠技師 ShopTechnician 5
    //車廠員工 ShopStaff 6
    //會員 User 7
    //一般 General 8
    //測試人員1 Test1 16
    //測試人員2 Test2 17

    //new Claim(JwtClaimTypes.Role, "Admin"),
    //new Claim(JwtClaimTypes.Role, "Agent"),
    //new Claim(JwtClaimTypes.Role, "Dealer"),
    //new Claim(JwtClaimTypes.Role, "ShopManager"),
    //new Claim(JwtClaimTypes.Role, "ShopTechnician"),
    //new Claim(JwtClaimTypes.Role, "ShopStaff"),
    //new Claim(JwtClaimTypes.Role, "User")
    //new Claim(JwtClaimTypes.Role, "General")
    //new Claim(JwtClaimTypes.Role, "Test1")
    //new Claim(JwtClaimTypes.Role, "Test2")
}
