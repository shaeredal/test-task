using NUnit.Framework;
using OnlinerNotifier.BLL.Validators;

namespace OnlinerNotifier.BLL_Tests.Validators
{
    [TestFixture]
    public class EmailValidatorTest
    {
        [Test]
        public void IsValid_Email_True()
        {
            var emailValidator = new EmailValidator();
            var email = "OnlinerNotifier@gmail.com";

            var result = emailValidator.IsValid(email);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_Email_False()
        {
            var emailValidator = new EmailValidator();
            var email = "OnlinerNotifier@gmailcom";

            var result = emailValidator.IsValid(email);

            Assert.IsFalse(result);
        }
    }
}
