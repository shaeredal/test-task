using System;

namespace OnlinerNotifier.BLL.Models.NotificationModels
{
    public class NotificationEmailModel
    {
        public string EmailAddress { get; set; }

        public string ReceiverName { get; set; }

        public string EmailBody { get; set; }

        public DateTime NotificationTime { get; set; }
    }
}
