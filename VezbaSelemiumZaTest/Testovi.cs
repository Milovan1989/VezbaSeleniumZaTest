using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace VezbaSeleniumZaTest
{
    class Cas34 : SeleniumBaseClass
    {
        [Test]
        [Category("Cas34")]
        public void Test_Ivana()
        {
            this.NavigateTo("https://www.emmi.rs/");
            this.DoWait(1);
            IWebElement monitori = this.FindElement(By.XPath("//a[@title='Monitori']"));
            monitori.Click();
            this.DoWait(1);
            IWebElement brend = this.FindElement(By.Name("brandId"));
            var select = new SelectElement(brend);
            select.SelectByText("HP");
            this.DoWait(1);
            IWebElement tip = this.FindElement(By.Name("tip"));
            select = new SelectElement(tip);
            select.SelectByText("TN");
            this.DoWait(1);
            IWebElement trazi = this.FindElement(By.XPath("//input[@value='traži']"));
            trazi.Click();
            this.DoWait(1);
            IWebElement omen = this.FindElement(By.XPath("//a[contains(text(), 'OMEN')]"));
            omen.Click();
            this.DoWait(1);
            IWebElement price = this.FindElement(By.XPath("//div[@class='price']"));
            var cena = price.Text.Trim();
            Assert.AreEqual("29.990", cena);
            this.DoWait(2);
        }

        [Test]
        [Category("Cas34")]
        public void Test2_Ivana()
        {
            this.NavigateTo("http://shop.qa.rs/");
            IWebElement kolicina = this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[1]//select"));
            var select = new SelectElement(kolicina);
            select.SelectByValue("6");
            int ocekivanaCena = Convert.ToInt32(this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[2]")).Text.Substring(1));
            this.DoWait(1);
            IWebElement order = this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[1]//input[@type='submit']"));
            order.Click();
            int qty = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[2]")).Text);
            int price = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[3]")).Text.Substring(1));
            int subtotal = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[4]")).Text.Substring(1));
            Assert.AreEqual(ocekivanaCena, price);
            Assert.AreEqual(subtotal, qty * price);
            this.DoWait(1);
        }

        [Test]
        [Category("Cas34")]
        public void Test_Filip()
        {
            this.NavigateTo("https://www.emmi.rs/naslovna_stranica.1.html");
            DoWait(1);
            this.FindElement(By.LinkText("Monitori"))?.Click();
            DoWait(1);
            IWebElement brend = this.FindElement(By.Name("brandId"));
            var selection = new SelectElement(brend);
            selection.SelectByValue("448");
            DoWait(1);
            IWebElement tip = this.FindElement(By.Name("tip"));
            var selection2 = new SelectElement(tip);
            selection2.SelectByValue("34878");
            this.FindElement(By.XPath("//div[@id='searchBottom']/input[@type='submit']"))?.Click();
            DoWait(1);
            this.FindElement(By.PartialLinkText("HP OMEN Z7Y57AA TN"))?.Click();
            DoWait(1);
            IWebElement price = this.FindElement(By.XPath("//div[@class='price']"));
            string cena = price.Text.Trim();
            Assert.AreEqual("29.990", cena);
        }

        [Test]
        [Category("Cas34")]
        public void Test2_Filip()
        {
            this.NavigateTo("http://shop.qa.rs/");
            DoWait(2);
            string pricePerItem = this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[2]")).Text;

            IWebElement small = this.FindElement(By.XPath("(//select[@name='quantity'])[2]"));
            var selection = new SelectElement(small);
            selection.SelectByValue("6");
            DoWait(1);
            this.FindElement(By.XPath("(//input[@type='submit' and @value='ORDER NOW'])[2]"))?.Click();

            string kolicina = this.FindElement(By.XPath("(//table//td)[2]")).Text;
            string cena = this.FindElement(By.XPath("(//table//td)[3]")).Text.Substring(1);

            int expected = Convert.ToInt32(kolicina) * Convert.ToInt32(pricePerItem.Substring(1));
            int actual = Convert.ToInt32(kolicina) * Convert.ToInt32(cena);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Category("Cas34")]
        public void Test_Marina()
        {
            this.NavigateTo("https://www.emmi.rs/naslovna_stranica.1.html");
            IWebElement elements1 = this.FindElement(By.XPath("//a[@title = 'Monitori']"));
            elements1.Click();
            this.DoWait(1);
            string selectByText = "HP";
            IWebElement el = this.FindElement(By.Name("brandId"));
            SelectElement select = new SelectElement(el);
            select.SelectByText(selectByText);
            string selectByText1 = "TN";
            IWebElement el1 = this.FindElement(By.Name("tip"));
            SelectElement select1 = new SelectElement(el1);
            select1.SelectByText(selectByText1);
            this.FindElement(By.XPath("//input[@value = 'traži']"))?.Click();
            this.FindElement(By.XPath("//a[contains(text(), 'OMEN')]"))?.Click();
            this.DoWait(1);
            IWebElement price = this.FindElement(By.XPath("//div[@class='price']"));
            string selectedItem = price.Text.Trim();
            string expectedItem = "29.990";
            Assert.AreEqual(expectedItem, selectedItem);
        }

        [Test]
        [Category("Cas34")]
        public void Test2_Marina()
        {
            this.NavigateTo("http://shop.qa.rs/");
            int ocekivanaCenaPoKomadu = Convert.ToInt32(this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[2]")).Text.Substring(1));
            IWebElement el = this.FindElement(By.XPath("//div[@class='panel panel-warning']//div[@class='panel-body text-justify']//select[@name='quantity']"));
            var broj = new SelectElement(el);
            broj.SelectByValue("6");
            this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[1]//input[@type='submit']"))?.Click();
            string cenaPoKomadu = this.FindElement(By.XPath("(//table//td)[3]")).Text.Substring(1);

            Assert.AreEqual(ocekivanaCenaPoKomadu, Convert.ToInt32(cenaPoKomadu));
        }

        [SetUp]
        public void SetUpTests()
        {
            this.Driver = new FirefoxDriver();
            //this.Driver = new ChromeDriver();
            this.Driver.Manage().Cookies.DeleteAllCookies();
            this.Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDownTests()
        {
            this.Close();
        }
    }
}