using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic;
using Service;
using DataAccess;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationSenderService notificationSender;

        
        public NotificationController(INotificationSenderService notificationSender)
        {
            this.notificationSender = notificationSender;
        }

        [HttpGet("notify/attendance")]
        public string NotifyAllAboutAttendance()
        {
            var notification = notificationSender.NotifyAllAboutAttendance();
            var result = JsonConvert.SerializeObject(notification, Formatting.Indented);
            return result;
        }

        [HttpGet("notify/progress")]
        public string NotifyAllAboutProgress()
        {
            var notification = notificationSender.NotifyAllAboutProgress();
            var result = JsonConvert.SerializeObject(notification, Formatting.Indented);
            return result;
        }
    }
}
