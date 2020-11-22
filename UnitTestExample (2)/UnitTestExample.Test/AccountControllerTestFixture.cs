using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
         ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();


            var actualResult = accountController.ValidateEmail(email);


            Assert.AreEqual(expectedResult, actualResult);
        }
        [
            Test,
            TestCase("Abcd", false),
            TestCase("ABCD123", false),
            TestCase("abcd1234", false),
            TestCase("Abcd123", false),
            TestCase("Abcd1234", true)
         ]
        public void Register (string password, bool expectedResult)
        {

        }
    }
}