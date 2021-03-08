﻿using System;
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
        private ApplicationDbContext _dbContext;
        public IOrderService OrderService { get; }

        public ReportService(IOrderService orderService)
        {
            OrderService = orderService;
            _dbContext = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }

        public string GenerateSalesTable(List<DrugCartItem> cartItems)
        {
            var sb = new StringBuilder();
            var table = @"
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
                string row = $@"<tbody>
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

        public Report CreateReport(TimeFrame timeFrame)
        {
            Report report;
            switch (timeFrame)
            {
                case TimeFrame.DAILY:
                {
                    Func<Report, bool> dailyFunc = report1 => report1.CreatedAt.Date == DateTime.Now.Date;
                    report =
                        _dbContext.Reports.FirstOrDefault(dailyFunc);
                    if (report == null)
                    {
                        report = new Report();
                    }

                    report.Orders = OrderService.GetOrdersForTheDay();
                    report.TimeFrame = timeFrame;
                    report.TotalRevenueForReport = OrderService.GetOrdersForTheDay().Select(order => order.Price).Sum();

                    var drugItem = new List<DrugCartItem>();
                    var orders = OrderService.GetOrdersForTheDay();
                    foreach (var order in orders)
                    {
                        foreach (var drugCartItem in order.OrderItems)
                        {
                            drugItem.Add(_dbContext.DrugCartItems.Include(item => item.Drug).Include(item => item.DrugCart).FirstOrDefault(item => item.Id == drugCartItem.Id));
                        }
                    }

                    report.DrugSales = GenerateSalesTable(drugItem);


                    if (_dbContext.Reports.Any(dailyFunc))
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
                        report1.CreatedAt.Year.Equals(beginningOfWeek.Year) && report1.CreatedAt >= beginningOfWeek &&
                        report1.CreatedAt <= lastDayOfWeek;

                    report = _dbContext.Reports.FirstOrDefault(weeklyFunc);

                    if (report == null)
                    {
                        report = new Report();
                    }

                    report.Orders = OrderService.GetOrdersForTheWeek();
                    report.TimeFrame = timeFrame;
                    report.TotalRevenueForReport =
                        OrderService.GetOrdersForTheWeek().Select(order => order.Price).Sum();

                    var drugItem = new List<DrugCartItem>();
                    var orders = OrderService.GetOrdersForTheWeek();
                    foreach (var order in orders)
                    {
                        foreach (var drugCartItem in order.OrderItems)
                        {
                            drugItem.Add(_dbContext.DrugCartItems.Include(item => item.Drug).Include(item => item.DrugCart).FirstOrDefault(item => item.Id == drugCartItem.Id));
                        }
                    }

                    report.DrugSales = GenerateSalesTable(drugItem);

                    if (_dbContext.Reports.Any(weeklyFunc))
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
                        report1.CreatedAt.Year.Equals(DateTime.Now.Year);
                    
                    report = _dbContext.Reports.FirstOrDefault(monthlyFunc);
                    if (report == null)
                    {
                        report = new Report();
                    }

                    report.Orders = OrderService.GetOrdersForTheMonth();
                    report.TimeFrame = timeFrame;
                    report.TotalRevenueForReport =
                        OrderService.GetOrdersForTheMonth().Select(order => order.Price).Sum();

                    var drugItem = new List<DrugCartItem>();
                    var orders = OrderService.GetOrdersForTheMonth();
                    foreach (var order in orders)
                    {
                        foreach (var drugCartItem in order.OrderItems)
                        {
                            drugItem.Add(_dbContext.DrugCartItems.Include(item => item.Drug).Include(item => item.DrugCart).FirstOrDefault(item => item.Id == drugCartItem.Id));
                        }
                    }

                    report.DrugSales = GenerateSalesTable(drugItem);

                    if (_dbContext.Reports.Any(monthlyFunc))
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