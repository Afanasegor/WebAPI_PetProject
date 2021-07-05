using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface INotificationSenderService
    {
        public List<EmailNotificationModel> NotifyAllAboutAttendance();
        public List<SmsNotificationModel> NotifyAllAboutProgress();
    }
}
