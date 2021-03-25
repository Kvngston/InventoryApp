using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Repository;

namespace inventoryAppDomain.Services
{
    public interface IProfileService
    {
        void EditProfile(ApplicationUser user, Pharmacist pharmacist = null, StoreManager storeManager = null);
        List<ApplicationUser> GetAllUsers();

        Task<ApplicationUser> ValidateUser(string userId);

        Task<ApplicationUser> ChangeUserRole(MockViewModel updateUserRoleViewModel);

        // void ChangeProfile(ApplicationUser user, Pharmacist pharmacist = null, StoreManager storeManager = null);

        Task RemoveUser(string userId);
    }
}