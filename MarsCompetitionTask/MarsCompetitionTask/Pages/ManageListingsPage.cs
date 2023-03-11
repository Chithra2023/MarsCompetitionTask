using MarsCompetitionTask.Utilities;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace MarsCompetitionTask.Pages
{
    public class ManageListingsPage : CommonDriver
    {
        //Manage Listings
        private IWebElement manageListingsTab => driver.FindElement(By.XPath("//a[@href='/Home/ListingManagement']"));
        //View button
        private IWebElement firstRowEditButton => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr/td[8]/div/button[2]/i"));
        private IWebElement titleEditTextBox => driver.FindElement(By.Name("title"));
        private IWebElement descriptionEditTextBox => driver.FindElement(By.Name("description"));
        private IWebElement categoryEditDropDown => driver.FindElement(By.Name("categoryId"));
        private IWebElement subCategoryEditDropDown => driver.FindElement(By.Name("subcategoryId"));
        private IList<IWebElement> addedTags => driver.FindElements(By.XPath("//form[@class='ui form']/div[4]/div[2]/div/div/div/span/a"));
        private IWebElement Tags => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[4]/div[2]/div/div/div/div/input"));
        private IWebElement oneOffServiceRadio => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[2]/div/input"));
        private IWebElement hourlyServiceRadio => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[1]/div/input"));
        private IWebElement onsiteRadioButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[1]/div/input"));
        private IWebElement onlineRadioButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[2]/div/input"));
        private IWebElement skillExchangeRadioButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[2]/div/div[1]/div/input"));
        private IWebElement creditRadioButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input"));
        private IWebElement SkillExchangeTag => driver.FindElement(By.XPath("//div[@class='form-wrapper']//input[@type='text']"));
        private IList<IWebElement> addedSkillExchangeTags => driver.FindElements(By.XPath("//form[@class='ui form']/div[8]/div[4]/div/div/div/div/span/a"));
        private IWebElement creditTextBox => driver.FindElement(By.XPath("//input[@placeholder='Amount']"));
        private IWebElement activeRadioButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[1]/div/input"));
        private IWebElement hiddenRadioButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[2]/div/input"));
        private IWebElement saveEditButton => driver.FindElement(By.XPath("//input[@value='Save']"));
        private IWebElement manageListingTable => driver.FindElement(By.XPath("/*[@id=\"listing - management - section\"]/div[2]/div[1]/div[1]/table"));
        private IWebElement yesDeleteButton => driver.FindElement(By.XPath("//div[@class='actions']/button[contains(text(), 'Yes')]"));
        private IWebElement noDeleteButton => driver.FindElement(By.XPath("//div[@class='actions']/button[contains(text(), 'No')]"));
        public bool editListing = false;
        public bool deleteListing = false;



        //Navigate to Manage Listing

        internal void GoToManageListingsTab()
        {
            //Wait for Language Tab and click 
            Wait.WaitForElementToBeClickable(driver, "XPath", "//a[@href='/Home/ListingManagement']", 5);
            manageListingsTab.Click();
        }

        public void EditListings(int rowNumber)
        {
            //Click on ManageListing
            GoToManageListingsTab();
            Wait.WaitForElementToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr/td[8]/div/button[2]/i", 5);
            firstRowEditButton.Click();
            //Populate the Excel Sheet
            ExcelUtil.PopulateInCollection(@"C:\Chithra - Industry Connect\MVP Studio\Automation_Final\CompetitionTask_ProjectMars\ShareSkill_TestData.xlsx");

            //Edit Title
            titleEditTextBox.Clear();
            titleEditTextBox.SendKeys(ExcelUtil.ReadData(rowNumber, "Title"));

            //Edit Description
            descriptionEditTextBox.Clear();
            descriptionEditTextBox.SendKeys(ExcelUtil.ReadData(rowNumber, "Description"));

            //Edit category
            SelectElement selectCategory = new SelectElement(categoryEditDropDown);
            selectCategory.SelectByText(ExcelUtil.ReadData(rowNumber, "Category"));

            //Edit Subcategory
            SelectElement selectSubCategory = new SelectElement(subCategoryEditDropDown);
            selectSubCategory.SelectByText(ExcelUtil.ReadData(rowNumber, "Subcategory"));

            //Edit Tags
            //Delete tags
            int tagCount = addedTags.Count();
            for (int i = 0; i < tagCount; i++)
            {
                if (tagCount > 0)

                    addedTags[i].Click();
            }
            //Update by adding new tag
            Tags.Click();
            Tags.SendKeys(ExcelUtil.ReadData(rowNumber, "Tags"));
            Tags.SendKeys(Keys.Enter);

            //Update Service Type
            string ServiceTypeRadioLabel = ExcelUtil.ReadData(rowNumber, "ServiceType");

            if (ServiceTypeRadioLabel.Equals("One-off service"))
            {
                oneOffServiceRadio.Click();
            }

            if (ServiceTypeRadioLabel.Equals("Hourly basis service"))
            {
                hourlyServiceRadio.Click();
            }

            //Update Location Type
            string LocationTypeRadioLabel = ExcelUtil.ReadData(rowNumber, "LocationType");
            if (LocationTypeRadioLabel.Equals("On-site"))
            {
                onsiteRadioButton.Click();
            }

            if (ServiceTypeRadioLabel.Equals("Online"))
            {
                onlineRadioButton.Click();
            }

            //Update Skill Trade
            string skillTradeRadioLabel = ExcelUtil.ReadData(rowNumber, "SkillTradeOption");
            if (skillTradeRadioLabel.Equals("Skill-exchange"))
            {
                skillExchangeRadioButton.Click();
                //Delete tags
                int skillExchangeTagCount = addedSkillExchangeTags.Count();
                for (int i = 0; i < skillExchangeTagCount; i++)
                {
                    if (skillExchangeTagCount > 0)

                        addedSkillExchangeTags[i].Click();
                }


                SkillExchangeTag.SendKeys(ExcelUtil.ReadData(rowNumber, "SkillExchangeTag"));
                SkillExchangeTag.SendKeys(Keys.Enter);
            }
            if (skillTradeRadioLabel.Equals("Credit"))
            {
                creditRadioButton.Click();
                descriptionEditTextBox.Clear();
                creditTextBox.SendKeys(ExcelUtil.ReadData(rowNumber, "CreditAmount"));
            }

            //Update IsActive
            string activeTypeRadioLabel = ExcelUtil.ReadData(rowNumber, "ActiveOption");
            if (activeTypeRadioLabel.Equals("Active"))
            {
                activeRadioButton.Click();
            }

            if (activeTypeRadioLabel.Equals("Hidden"))
            {
                hiddenRadioButton.Click();
            }
            saveEditButton.Click();
            editListing = true;
        }

        public void DeleteListings(int rowNumber)
        {
            GoToManageListingsTab();
            //Populate the Excel Sheet
            ExcelUtil.PopulateInCollection(@"C:\Chithra - Industry Connect\MVP Studio\Automation_Final\CompetitionTask_ProjectMars\ShareSkill_TestData.xlsx");

            // Find the XPath of Delete Button with Title read from excel
            string titletoDelete = ExcelUtil.ReadData(rowNumber, "Title");
            string delButtonXpath = "//div[@id='listing-management-section']//tbody/tr[contains(.,'" + titletoDelete + "')]/td[8]/div/button[3]";
            Wait.WaitForElementToExist(driver, "XPath", delButtonXpath, 5);
            IWebElement deleteButton = driver.FindElement(By.XPath(delButtonXpath));
            Wait.WaitForElementToBeClickable(driver, "XPath", delButtonXpath, 5);
            deleteButton.Click();

            //delete Confirmation Yes or No
            string forDelete = ExcelUtil.ReadData(rowNumber, "ForDelete");
            //Click Yes
            if (forDelete.Equals("Yes"))
            {
                yesDeleteButton.Click();

            }
            if (forDelete.Equals("No"))
            {
                //Click No
                noDeleteButton.Click();
            }
            deleteListing = true;
        }

    }
}

