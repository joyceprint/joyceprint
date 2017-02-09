using JoycePrint.Domain.Enums;

namespace JoycePrint.Domain.Models
{
    public class Notification
    {
        /// <summary>
        /// The name of the view that will render the notification
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// The header for the notification
        /// </summary>       
        public string Header { get; set; }

        /// <summary>
        /// The message for the notification
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Sets the notification properties based on the notification type
        /// </summary>
        /// <param name="type"></param>
        public void SetNotification(NotificationType type)
        {
            ViewName = "Notification";

            switch (type)
            {
                case NotificationType.SUCCESS:
                    Header = "Success";
                    Message = "We have recieved your enquiry and will get back to you shortly";
                    break;
                case NotificationType.FAILURE:
                    Header = "Error";
                    Message = "Sorry, your request was not completed.";
                    break;
                case NotificationType.NONE:
                    Header = Message = string.Empty;
                    break;
            }
        }        
    }
}