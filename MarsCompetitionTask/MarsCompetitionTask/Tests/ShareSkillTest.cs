using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using FluentAssertions;
using MarsCompetitionTask.Pages;
using MarsCompetitionTask.Utilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetitionTask.Tests
{
    [TestFixture]
    public class ShareSkillTest : CommonDriver 
    {
        public static ExtentTest test1;

        public static ExtentReports extent1;
        public ShareSkillTest()
        {
            driver = new ChromeDriver();
        }
        [SetUp]
        public void SignIn()
        {
            extent1 = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(@"C:\Chithra - Industry Connect\MVP Studio\Automation_Final\MarsCompetitionTask\MarsCompetitionTask\MarsCompetitionTask\MarsCompetitionTask\ExtentReports\+ DateTime.Now.ToString(""_MMddyyyy_hhmmtt"") + "".html");
            extent1.AttachReporter(htmlReporter);
            // Login Page object initialization and definition

            LoginPage loginpageObj = new LoginPage();
            loginpageObj.loginActions();
        }
        [Test, Order(1)]
        public void AddShareSkill()
        {
            test1 = extent1.CreateTest("Share Skills Test").Info("Add Skill Test Started");
            ShareSkillPage shareSkillPageObj = new ShareSkillPage();
            shareSkillPageObj.AddShareSkillListing(2);
            shareSkillPageObj.shareSkillListingAdded.Should().BeTrue();
            test1.Log(Status.Info, "Skill added Sucessfully to Manage Listings Page");
        }
        [Test, Order(2)]
        public void EditListing()
        {
            test1 = extent1.CreateTest("Edit Skills Test").Info("Edit Skill Test Started");
            // MangeListing Page object initialization and definition
            ManageListingsPage manageListingPageObj = new ManageListingsPage();
            manageListingPageObj.EditListings(3);
            manageListingPageObj.editListing.Should().BeTrue();
            //test1.Log(Status.Info, "Skill added Sucessfully to Manage Listings Page");
            test1.Log(Status.Pass, "Sucessfully Edited a Skill in the Manage Listing Page");

        }
        [Test, Order(3)]
        public void DeleteListing()
        {
            test1 = extent1.CreateTest("Delete Skill Test").Info("Delete Skill Test Started");
            ManageListingsPage manageListingPageObj = new ManageListingsPage();
            manageListingPageObj.DeleteListings(3);
            manageListingPageObj.deleteListing.Should().BeTrue();
            test1.Log(Status.Pass, "Sucessfully Deleted a Skill in the Manage Listing Page");
        }

        [TearDown]
        public void quit()
        {
            extent1.Flush();
            driver.Quit();
            
        }

    }
}
