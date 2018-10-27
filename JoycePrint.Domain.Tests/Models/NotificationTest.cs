using JoycePrint.Domain.Enums;
using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests.Models
{
    [TestClass]
    public class NotificationTest : BaseTest
    {
        /// <summary>
        /// Test the notification that will be set when the notification type is None
        /// </summary>
        [TestMethod]
        public void SetNotificationTypeNone()
        {
            var expectedViewName = "Notification";
            var expectedHeader = "";
            var expectedMessage = "";
            const NotificationType expectedType = NotificationType.None;

            var notifiation = new Notification();

            notifiation.SetNotification(NotificationType.None);

            AssertAreEqual(expectedViewName, notifiation.ViewName, "ViewName");
            AssertAreEqual(expectedHeader, notifiation.Header, "Header");
            AssertAreEqual(expectedMessage, notifiation.Message, "Message");
            AssertAreEqual(expectedType, notifiation.Type, "Type");
        }

        /// <summary>
        /// Test the notification that will be set when the notification type is Success
        /// </summary>
        [TestMethod]
        public void SetNotificationTypeSuccess()
        {
            var expectedViewName = "Notification";
            var expectedHeader = "We have received your email and will get back to you shortly";
            var expectedMessage = "Thank you for your enquiry";
            const NotificationType expectedType = NotificationType.Success;

            var notifiation = new Notification();

            notifiation.SetNotification(NotificationType.Success);

            AssertAreEqual(expectedViewName, notifiation.ViewName, "ViewName");
            AssertAreEqual(expectedHeader, notifiation.Header, "Header");
            AssertAreEqual(expectedMessage, notifiation.Message, "Message");
            AssertAreEqual(expectedType, notifiation.Type, "Type");
        }

        /// <summary>
        /// Test the notification that will be set when the notification type is FAILURE
        /// </summary>
        [TestMethod]
        public void SetNotificationTypeFailure()
        {
            var expectedViewName = "Notification";
            var expectedHeader = "An unexpected error has occurred";
            var expectedMessage = "As the site is experiencing issues at the moment, please give us a call on +353-94-925-6876";
            const NotificationType expectedType = NotificationType.Failure;

            var notifiation = new Notification();

            notifiation.SetNotification(NotificationType.Failure);

            AssertAreEqual(expectedViewName, notifiation.ViewName, "ViewName");
            AssertAreEqual(expectedHeader, notifiation.Header, "Header");
            AssertAreEqual(expectedMessage, notifiation.Message, "Message");
            AssertAreEqual(expectedType, notifiation.Type, "Type");
        }
    }
}