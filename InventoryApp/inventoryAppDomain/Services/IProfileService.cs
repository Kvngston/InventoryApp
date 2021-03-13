using System;
using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;

namespace inventoryAppDomain.Services
{
    public interface IProfileService
    {
        void EditProfile(ApplicationUser user, Pharmacist pharmacist = null, StoreManager storeManager = null);
    }
}