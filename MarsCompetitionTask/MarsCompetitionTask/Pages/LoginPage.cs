using MarsCompetitionTask.Utilities;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Pages
{
    public class LoginPage : CommonDriver
    {
        private IWebElement signInButton => driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
        private IWebElement userNameTextBox => driver.FindElement(By.Name("email"));
        private IWebElement passwordTextbox => driver.FindElement(By.Name("password"));
        private IWebElement loginButton => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));
        public void loginActions()
        {
            //Launch portal
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://192.168.0.107:5000/");
            //Click on SignIn
            signInButton.Click();

            ExcelUtil.PopulateInCollection(@"C:\Chithra - Industry Connect\MVP Studio\Automation_Final\MarsCompetitionTask\MarsCompetitionTask\Test Data\Login Details.xlsx");

            //Enter Username
            userNameTextBox.SendKeys(ExcelUtil.ReadData(2, "Username"));

            //Enter password
            passwordTextbox.SendKeys(ExcelUtil.ReadData(2, "Password"));

            //Click Login Button
            loginButton.Click();
        }
    }
}
