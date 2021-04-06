using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using inventoryAppDomain.Entities;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.ExtensionMethods;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Services;
using Microsoft.AspNet.Identity.Owin;

namespace inventoryAppDomain.Repository
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IOrderService _orderService;
        private readonly IDrugCartService _drugCartService;
        private readonly IDrugService _drugService;

        public ReportService(IOrderService orderService, IDrugCartService drugCartService, IDrugService drugService)
        {
            _orderService = orderService;
            _drugCartService = drugCartService;
            _drugService = drugService;
            _dbContext = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }

        private static string GenerateSalesTable(List<DrugCartItem> cartItems)
        {
            var sb = new StringBuilder();
            const string table = @"
                                <table class= "" table table-hover table-bordered text-left "">
                                <thead>
                                    <tr class= ""table-success "">
                                    <th>Drug Name</th>
                                    <th>Quantity</th>
                                    <th>Price</th>
                                    </tr>
                                </thead>";
            sb.Append(table);
            foreach (var item in cartItems)
            {
                var row = $@"<tbody>
                                <tr class=""info"" style="" cursor: pointer"">
                                <td class=""font-weight-bold"">{item.Drug.DrugName}</td>
                                <td class=""font-weight-bold"">{item.Amount}</td>
                                <td class=""font-weight-bold"">{item.Drug.Price}</td>
                         </tr>
                         </tbody>";
                sb.Append(row);
            }

            sb.Append("</Table>");
            return sb.ToString();
        }

        public Report GetReportByFunc(Func<Report, bool> func)
        {
            return _dbContext.Reports.FirstOrDefault(func);
        }

        public bool GetReportBoolByFunc(Func<Report, bool> func)
        {
            return _dbContext.Reports.Any(func);
        }

        public Report CreateReport(TimeFrame timeFrame)
        {
            Report report;
            switch (timeFrame)
            {
                case TimeFrame.DAILY:
                {
                    Func<Report, bool> dailyFunc = report1 => report1.CreatedAt.Date == DateTime.Now.Date && report1.TimeFrame == timeFrame;
                    report = GetReportByFunc(dailyFunc) ?? new Report();
                    var orders = _orderService.GetOrdersForTheDay();
                    report.Orders = orders;
                    report.TimeFrame = timeFrame;
                    report.TotalRevenueForReport = orders.Select(order => order.Price).Sum();

                    var drugItem = new List<DrugCartItem>();
                    var drugs = new List<Drug>();
                    
                    foreach (var order in orders)
                    {
                        foreach (var drugCartItem in order.OrderItems)
                        {
                            drugItem.Add(_drugCartService.GetDrugCartItemById(drugCartItem.Id));
                            drugs.Add(_drugService.GetDrugById(drugCartItem.DrugId));
                        }
                    }

                    report.DrugSales = GenerateSalesTable(drugItem);
                    report.ReportDrugs = drugs;

                    if (GetReportBoolByFunc(dailyFunc))
                    {
                        _dbContext.Entry(report).State = EntityState.Modified;
                    }
                    else
                    {
                        _dbContext.Reports.Add(report);
                    }

                    _dbContext.SaveChanges();
                    return report;
                }
                case TimeFrame.WEEKLY:
                {
                    var beginningOfWeek = DateTime.Now.FirstDayOfWeek();
                    var lastDayOfWeek = DateTime.Now.LastDayOfWeek();
                    Func<Report, bool> weeklyFunc = report1 =>
                        report1.CreatedAt.Month.Equals(beginningOfWeek.Month) &&
                        report1.CreatedAt.Year.Equals(beginningOfWeek.Year) && (report1.CreatedAt >= beginningOfWeek &&
                        report1.CreatedAt <= lastDayOfWeek && report1.TimeFrame == timeFrame);

                    
                    report = GetReportByFunc(weeklyFunc) ?? new Report();

                    var orders = _orderService.GetOrdersForTheWeek();
                    report.Orders = orders;
                    report.TimeFrame = timeFrame;
                    report.TotalRevenueForReport = orders.Select(order => order.Price).Sum();

                    var drugItem = new List<DrugCartItem>();
                    var drugs = new List<Drug>();
                    
                    foreach (var order in orders)
                    {
                        foreach (var drugCartItem in order.OrderItems)
                        {
                            drugItem.Add(_drugCartService.GetDrugCartItemById(drugCartItem.Id));
                            drugs.Add(_drugService.GetDrugById(drugCartItem.DrugId));
                        }
                    }

                    report.ReportDrugs = drugs;
                    report.DrugSales = GenerateSalesTable(drugItem);

                    if (GetReportBoolByFunc(weeklyFunc))
                    {
                        _dbContext.Entry(report).State = EntityState.Modified;
                    }
                    else
                    {
                        _dbContext.Reports.Add(report);
                    }

                    _dbContext.SaveChanges();
                    return report;
                }
                case TimeFrame.MONTHLY:
                {
                    Func<Report, bool> monthlyFunc = report1 =>
                        report1.CreatedAt.Month.Equals(DateTime.Now.Month) &&
                        report1.CreatedAt.Year.Equals(DateTime.Now.Year) && report1.TimeFrame == timeFrame;
                    
                    report = GetReportByFunc(monthlyFunc);
                    if (report == null)
                    {
                        report = new Report();
                    }

                    var orders = _orderService.GetOrdersForTheMonth();
                    report.Orders = _orderService.GetOrdersForTheMonth();
                    report.TimeFrame = timeFrame;
                    report.TotalRevenueForReport = orders.Select(order => order.Price).Sum();

                    var drugItem = new List<DrugCartItem>();
                    var drugs = new List<Drug>();
                    
                    foreach (var order in orders)
                    {
                        foreach (var drugCartItem in order.OrderItems)
                        {
                            drugItem.Add(_drugCartService.GetDrugCartItemById(drugCartItem.Id));
                            drugs.Add(_drugService.GetDrugById(drugCartItem.DrugId));
                        }
                    }

                    report.DrugSales = GenerateSalesTable(drugItem);
                    report.ReportDrugs = drugs;
                    if (GetReportBoolByFunc(monthlyFunc))
                    {
                        _dbContext.Entry(report).State = EntityState.Modified;
                    }
                    else
                    {
                        _dbContext.Reports.Add(report);
                    }

                    _dbContext.SaveChanges();
                    return report;
                }
                default: return null;
            }
        }
    }
}