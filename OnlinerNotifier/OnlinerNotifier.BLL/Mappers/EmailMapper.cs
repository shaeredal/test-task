using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class EmailMapper
    {
        private ITimeCalculationService timeCalculationService;

        public EmailMapper(ITimeCalculationService timeCalculationService)
        {
            this.timeCalculationService = timeCalculationService;
        }

        public NotificationEmailModel ToModel(User user, string emailBody, string subject)
        {
            return new NotificationEmailModel()
            {
                UserId = user.Id,
                EmailAddress = user.Email,
                ReceiverName = $"{user.FirstName} {user.LastName}",
                EmailBody = emailBody,
                Subject = subject,
                NotificationTime = timeCalculationService.CalculateNotificationTime(user.NotificationTime)
            };
        }
    }
}
