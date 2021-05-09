using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using inventoryAppDomain.Entities;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Services;
using Microsoft.AspNet.Identity.Owin;

namespace inventoryAppDomain.Repository
{
	public class DrugService : IDrugService
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly INotificationService _notificationService;

		public DrugService(INotificationService notificationService)
		{
			_notificationService = notificationService;
			_dbContext = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
		}

		public List<Drug> GetAllDrugs() => _dbContext.Drugs.Include(d => d.DrugCategory).ToList();

		public List<Drug> GetAvailableDrugs()
		{
			var drugs = GetAllDrugs();
			var availableDrugs = new List<Drug>();
			drugs.ForEach(drug =>
		   {
			   if (drug.Quantity > 0)
			   {
				   if (drug.ExpiryDate.CompareTo(DateTime.Now) == 1)
				   {
					   availableDrugs.Add(drug);
				   }
			   }
		   }
			);
			return availableDrugs;
		}
		public Drug GetAvailableDrugsById(int id)
		{
			var drug = GetAvailableDrugs().Find(d => d.Id == id);

			return drug ?? null;
		}
		public List<Drug> GetAvailableDrugFilter(string searchQuery)
		{
			var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower();
			if (queries == null)
			{
				return GetAvailableDrugs();
			}
			return GetAvailableDrugs().Where(item => queries.Any(query => (item.DrugName.ToLower().Contains(query)))).ToList();
		}

		public List<Drug> GetAllExpiringDrugs(TimeFrame timeFrame)
		{
			var drugs = GetAllDrugs();
			var expiringDrugs = new List<Drug>();
			var today = DateTime.Now;
			switch (timeFrame)
			{
				case TimeFrame.MONTHLY:
					{
						drugs.ForEach(drug =>
						{
							if (today.AddMonths(1).Month.Equals(drug.ExpiryDate.Month))
							{
								expiringDrugs.Add(drug);
							}
						});
						break;
					}
				case TimeFrame.WEEKLY:
					{
						drugs.ForEach(drug =>
						{
							if (today.AddDays(7).Day.Equals(drug.ExpiryDate.Day))
							{
								expiringDrugs.Add(drug);
							}
						});
						break;
					}
				default:
					{
						throw new Exception("An Error Occurred");
					}
			}

			return expiringDrugs;
		}

		public List<Drug> GetAllExpiredDrugs()
		{
			var drugs = GetAllDrugs();
			var expiredDrugs = new List<Drug>();
			drugs.ForEach(drug =>
			{
				if (drug.ExpiryDate.CompareTo(DateTime.Today) == 1)
				{
					expiredDrugs.Add(drug);
				}
			});
			return expiredDrugs;
		}

		public List<Drug> GetDrugsOutOfStock()
		{
			var drugs = GetAllDrugs();
			var drugsRunningOutOfStock = new List<Drug>();
			drugs.ForEach(drug =>
			{
				if (drug.Quantity <= 20)
				{
					drugsRunningOutOfStock.Add(drug);
				}
			});
			return drugsRunningOutOfStock;
		}

		public Drug GetDrugById(int id)
		{
			var result = _dbContext.Drugs.FirstOrDefault(drug => drug.Id == id);

			if (result == null)
				return null;

			return result;
		}

		public List<DrugCategory> AllCategories() => _dbContext.DrugCategories.ToList();


		public void AddDrug(Drug drug)
		{
			_dbContext.Drugs.Add(drug);
			_dbContext.SaveChanges();
		}
		public void UpdateDrug(Drug drug)
		{
			var update = _dbContext.Drugs.Add(drug);
			_dbContext.Entry(update).State = EntityState.Modified;

			_dbContext.SaveChanges();
		}

		public bool RemoveDrug(int id)
		{
			var drug = GetDrugById(id);
			if (drug == null)
				return false;
			else
			{
				_dbContext.Drugs.Remove(_dbContext.Drugs.Single(d => d.Id == id));
				_dbContext.SaveChanges();
				return true;
			}

		}

		public Drug EditDrug(int id)
		{
			var drug = GetDrugById(id);
			if (drug == null)
				return null;

			return _dbContext.Drugs.SingleOrDefault(d => d.Id == id);
		}

		public int DateComparison(DateTime FirstDate, DateTime SecondDate) =>
			DateTime.Compare(FirstDate, SecondDate);


		public void AddDrugCategory(DrugCategory category)
		{
			_dbContext.DrugCategories.Add(category);
			_dbContext.SaveChanges();
		}

		public bool RemoveDrugCategory(int id)
		{
			var removeCategory = _dbContext.DrugCategories.SingleOrDefault(category => category.Id == id);

			if (removeCategory == null)
				return false;

			_dbContext.DrugCategories.Remove(_dbContext.DrugCategories.Single(c => c.Id == id));
			_dbContext.SaveChanges();
			return true;
		}

		public DrugCategory EditDrugCategory(int id)
		{

			var result = _dbContext.DrugCategories.SingleOrDefault(d => d.Id == id);

			return result ?? null;

			//if (result == null)
			//    return null;

			//return result;
		}

		public void UpdateDrugCategory(DrugCategory category)
		{

			var update = _dbContext.DrugCategories.Add(category);
			_dbContext.Entry(update).State = EntityState.Modified;

			_dbContext.SaveChanges();
		}

		public async Task NotifyDrugExpirationAsync()
		{
			var drugs = GetAvailableDrugs();
			foreach (Drug drug in drugs)
			{
				var days = (drug.ExpiryDate.Date - DateTime.Now).Days;
				if (days <= 10)
				{
					 await _notificationService.CreateNotification(drug.DrugName + " About to Expire",
					 drug.DrugName + " is remaining " + days + "days to expire",
					 NotificationType.REOCCURRING, NotificationCategory.EXPIRATION);
				}
			}


		}

	}
}