//Ajay Singh 
//02-22-2020
//Medo.ai coding examination

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Diagnostics;

//need to install following nuget packages
//Install-Package Selenium.WebDriver -Version 3.141.0
//Install-Package NUnit -Version 3.12.0
//Install-Package NUnit3TestAdapter -Version 3.16.1

//When testing please select all tests in test explorer and right click and select debug selected tests. Assertion output will appear in Debug output window.

namespace SimpleUIUXTestingTest
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("D:\\3rdparty\\chrome");
            driver.Url = "https://medo.ai/career/test-challenge/index.html";
        }

        [Test, Order(1)]
        public void TestOne()
        {
            Trace.WriteLine("------------------Test One-----------------------------------");

            driver.Url = "https://medo.ai/career/test-challenge/index.html";

            IWebElement email = driver.FindElement(By.Id("inputEmail"));
            IWebElement pass = driver.FindElement(By.Id("inputPassword"));

            //locate via XPath due to many button without ID's
            IWebElement login = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/button"));

            if (email != null && pass != null && login != null)
            {
                email.SendKeys("mail@mail.com");
                pass.SendKeys("pass");

                login.Click();
            }

            Trace.WriteLine("------------------Test One-----------------------------------");
        }

        [Test, Order(2)]
        public void TestTwo()
        {
            Trace.WriteLine("------------------Test Two-----------------------------------");

            driver.Url = "https://medo.ai/career/test-challenge/index.html";

            IWebElement listGroup = driver.FindElement(By.ClassName("list-group"));

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            if (listGroup.FindElements(By.ClassName("list-group-item")).Count() == 3)
            {
                Trace.WriteLine("Control 'list-group' contains 3 list-group items...continuing tests");

                IWebElement listItemTwo = driver.FindElement(By.XPath("/html/body/div/div[2]/div/ul/li[2]"));

                if (listItemTwo.Text.Trim().Contains("List Item 2"))
                {

                    Trace.WriteLine("Found second list-group-item, item value is set to 'List Item 2'...continuing tests ");

                    IWebElement listItemTwoBadge = driver.FindElement(By.XPath("/html/body/div/div[2]/div/ul/li[2]/span"));

                    if (listItemTwoBadge.Text == "6")
                    {
                        Trace.WriteLine("Found second list-group-item span, item badge is set to '6'...all tests complete");
                    }
                    else
                    {
                        Trace.WriteLine("Could not find second list-group-item span....aborting final test ");
                    }
                }
                else
                {
                    Trace.WriteLine("Could not find second list-group-item....aborting further tests ");
                }
            }
            else
            {
                Trace.WriteLine("Control 'list-group' does not contain 3 list-group items....aborting further tests");
            }

            Trace.WriteLine("------------------Test Two-----------------------------------");

        }

        [Test, Order(3)]
        public void TestThree()
        {
            Trace.WriteLine("------------------Test Three-----------------------------------");


            driver.Url = "https://medo.ai/career/test-challenge/index.html";

            IWebElement dropDownButton = driver.FindElement(By.Id("dropdownMenuButton"));
            IWebElement newSelection = driver.FindElement(By.XPath("/html/body/div/div[3]/div/div/div/a[3]"));

            if (dropDownButton.Text.Trim() == "Option 1")
            {
                Trace.WriteLine("Control dropdownMenuButton has a default value of Option 1");

                dropDownButton.Click();
                newSelection.Click();
            }
            else
            {
                Trace.WriteLine("Control dropdownMenuButton does not have a default value of Option 1");
            }

            Trace.WriteLine("------------------Test Three-----------------------------------");

        }

        [Test, Order(4)]
        public void TestFour()
        {
            Trace.WriteLine("------------------Test Four-----------------------------------");

            driver.Url = "https://medo.ai/career/test-challenge/index.html";

            IWebElement enabledButton = driver.FindElement(By.XPath("/html/body/div/div[4]/div/button[1]"));
            IWebElement disabledButton = driver.FindElement(By.XPath("/html/body/div/div[4]/div/button[2]"));

            if (enabledButton.GetAttribute("disabled") != null)
            {
                Trace.WriteLine("Button 1 is disabled");
            }
            else
            {
                Trace.WriteLine("Button 1 is enabled");
            }

            if (disabledButton.GetAttribute("disabled") != null)
            {
                Trace.WriteLine("Button 2 is disabled");
            }
            else
            {
                Trace.WriteLine("Button 2 is enabled");
            }

            Trace.WriteLine("------------------Test Four-----------------------------------");

        }

        [Test, Order(5)]
        public void TestFive()
        {
            Trace.WriteLine("------------------Test Five-----------------------------------");

            driver.Url = "https://medo.ai/career/test-challenge/index.html";

            //wait for button to appear before trying to find it
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            IWebElement button = driver.FindElement(By.Id("test5-button"));
            button.Click();

            IWebElement alert = driver.FindElement(By.Id("test5-alert"));

            if (alert != null)
            {
                Trace.WriteLine("Test5 Alert is visible");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            button = driver.FindElement(By.Id("test5-button"));

            if (button.GetAttribute("disabled") != null)
            {
                Trace.WriteLine("Button is disabled");
            }
            else
            {
                Trace.WriteLine("Button is not disabled");
            }

            Trace.WriteLine("------------------Test Five-----------------------------------");
        }


        [Test, Order(6)]
        public void TestSix()
        {
            Trace.WriteLine("------------------Test Six-----------------------------------");

            driver.Url = "https://medo.ai/career/test-challenge/index.html";

            FindCell(2, 2);

            Trace.WriteLine("------------------Test Six-----------------------------------");
        }


        public void FindCell(int x, int y)
        {

            var table = driver.FindElement(By.TagName("table"));
            var rows = table.FindElements(By.TagName("tr"));

            for (int row = 0; row <= y; row++)
            {
                var rowTds = rows[row].FindElements(By.TagName("td"));

                for (int cell = 0; cell <= x; cell++)
                {
                    if (row == y && cell == x)
                    {
                        Trace.WriteLine("Cell value is: " + rowTds[cell].Text);
                    }
                }
            }
        }


        [OneTimeTearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}
