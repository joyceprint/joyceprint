using System;
using JoycePrint.Configuration;
using JoycePrint.Domain.Enums;

namespace JoycePrint.Domain.Models
{
    public class Notification
    {
        /// <summary>
        /// The name of the view that will render the notification
        /// </summary>
        public string ViewName { get; private set; }

        /// <summary>
        /// The header for the notification
        /// </summary>       
        public string Header { get; private set; }

        /// <summary>
        /// The message for the notification
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public NotificationType Type { get; private set; }

        /// <summary>
        /// Sets the notification properties based on the notification type
        /// </summary>
        /// <param name="type"></param>
        public void SetNotification(NotificationType type)
        {
            ViewName = "Notification";
            Type = type;

            switch (type)
            {
                case NotificationType.Success:
                    Header = Config.NotificationHeaderSuccess;
                    Message = Config.NotificationMessageSuccess;
                    break;
                case NotificationType.Failure:
                    Header = Config.NotificationHeaderError;
                    Message = Config.NotificationMessageError;
                    break;
                case NotificationType.None:
                    Header = Message = string.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }        
    }
}