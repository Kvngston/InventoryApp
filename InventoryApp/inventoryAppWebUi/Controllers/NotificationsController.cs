using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.Services;
using inventoryAppWebUi.Models;
using Microsoft.AspNetCore.Http;

namespace inventoryAppWebUi.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public ActionResult Index()
        {
            var notificationViewModel = new NotificationsPageViewModel
            {
                AllNotifications = _notificationService.GetAllNotifications(),
                NotificationsCount = _notificationService.GetNotificationsCount(NotificationStatus.UN_READ),
                UnreadNotifications = _notificationService.GetAllNonReadNotifications(),
            };
            return View(notificationViewModel);
        }

        public ActionResult GetRecentFive()
        {
            var notifications = _notificationService.GetRecentFive();
            return Json(notifications, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetNotificationById(int id)
        {
            var notification = _notificationService.GetNotificationById(id);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            try
            {
                var result = await _notificationService.MarkAsRead(id);
                
                if (result)
                {
                    return Json(new {status = "200", message = "Marked As Read Successful"}, JsonRequestBehavior.DenyGet);
                }
                return Json(new {status = "400", message = "Failed"}, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> MarkAllAsRead()
        {
            try
            {
                await _notificationService.MarkAllAsRead();
                return Json("Success" , JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllNotifications()
        {
            try
            {
                var notifications = _notificationService.GetAllNotifications();
                return View(notifications);

            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return View(Response.StatusCode);
            }
        }

        public ActionResult ShowAllNotifications()
        {
            return View();
        }

    }
}