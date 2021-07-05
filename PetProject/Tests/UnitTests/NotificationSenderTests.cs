using BusinessLogic;
using CustomExceptions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NotificationSenderTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("something")]
        [TestCase("zxc_@.ru")]
        public void SendEmailTest_InvalidEmails_InvalidEmailException(string email)
        {
            // arrange
            string message = "Sending test...";

            // act & assert
            Assert.Throws<InvalidEmailException>(() => NotificationSender.SendEmail(email, message));
        }

        [TestCase("afanasegor3@mail.ru")]
        [TestCase("egor.afanasyev@epam.com")]
        [TestCase("ivan.ivanov.lecturer@epam.com")]
        public void SendEmailTest_ValidEmails_CorrectResult(string email)
        {
            // arrange
            string message = "Sending test...";

            // act
            var actual = NotificationSender.SendEmail(email, message);

            // assert
            Assert.AreEqual(true, actual);
        }




        [TestCase(null)]
        [TestCase("")]
        [TestCase("1243")]
        [TestCase("asmkdl")]
        [TestCase("892746909375165161")]
        [TestCase("8(927)469-09-37")] // incorrect form in this project        
        public void SendSmsTest_InvalidPhoneNumbers_InvalidPhoneNumberException(string phoneNumber)
        {
            // arrange
            string message = "Sending test...";

            // act & assert
            Assert.Throws<InvalidPhoneNumberException>(() => NotificationSender.SendSms(phoneNumber, message));
        }

        [TestCase("89274690937")]
        [TestCase("+79274690937")]
        public void SendSmsTest_ValidPhoneNumbers_CorrectResult(string phoneNumber)
        {
            // arrange
            string message = "Sending test...";

            // act
            var actual = NotificationSender.SendSms(phoneNumber, message);

            // assert
            Assert.AreEqual(true, actual);
        }
    }
}
