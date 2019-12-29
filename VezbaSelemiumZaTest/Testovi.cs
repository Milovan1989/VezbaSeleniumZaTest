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
    class VezbaSeleniumZaTest : SeleniumBaseClass
    {
        [Test]
        public void Vezba1()
        {
            this.NavigateTo("https://www.google.rs/");
            this.DoWait(2);

            IWebElement gmail = this.FindElement(By.XPath("//a[contains(@class, 'gb_g') and contains(., 'Gmail')]"));
            gmail.Click();
            DoWait(2);
        }


        [Test]
        public void ToolsqaAutomationPractice()
        {
            this.NavigateTo("https://www.toolsqa.com/automation-practice-form");

            DoWait(3);
            DismissCookieWarning();

            IWebElement firstName = this.FindElement(By.XPath("//input[@name='firstname']"));
            this.SendKeys("Milovan", false, firstName);
            IWebElement lastName = this.FindElement(By.Id("lastname"));
            this.SendKeys("Lazarevic", false, lastName);
            DoWait(1);
            IWebElement gender = this.FindElement(By.Id("sex-0"));
            gender.Click();
            DoWait(1);
            IWebElement experience = this.FindElement(By.Id("exp-1"));
            experience.Click();
            DoWait(1);
            IWebElement datepick = this.FindElement(By.Id("datepicker"));
            this.SendKeys(DateTime.Today.ToString(), false, datepick);

            // Catches ad popup
            DoWait(12);
            DismissAdPopup();

            DoWait(1);
            IWebElement profession = this.FindElement(By.XPath("//input[@name='profession' and @value='Automation Tester']"));
            profession.Click();
            DoWait(1);
            IWebElement tool = this.FindElement(By.Id("tool-2"));
            tool.Click();
            DoWait(1);
            IWebElement continent = this.FindElement(By.Id("continents"));
            var selection = new SelectElement(continent);
            selection.SelectByText("Europe");
            DoWait(2);

            IWebElement success = this.FindElement(By.XPath("//div[@class='alert alert-success' and @role='alert']"));
            if (success == null)
            {
                Assert.Pass("Korisnik je uspesno registrovan i test je prosao");
            }
            else
            {
                Assert.Fail("Korisnik nije mogao da bude registrovan i test nije prosao");
            }
        }
    

        private void DismissCookieWarning()
        {
            // Dismisses cookie info message
            IWebElement cookie = this.FindElement(By.Id("cookie-law-info-bar"));
            cookie.FindElement(By.LinkText("ACCEPT"))?.Click();
        }

        private void DismissAdPopup()
        {
            this.FindElement(By.XPath("//img[@class='lazyloading']"))?.Click();
        }

        [Test]
        public void TestDragAndDrop()
        {
            // Doesn't work
            this.NavigateTo("https://formy-project.herokuapp.com/dragdrop");
            IWebElement target = this.FindElement(By.Id("box"));
            IWebElement drop1 = this.FindElement(By.Id("image"));
            var action = new Actions(this.Driver);
            action.DragAndDrop(drop1, target).Perform();
            action.ClickAndHold(drop1);
            action.MoveToElement(target);
            action.MoveByOffset(-150, 0);
            action.Release();
            action.Build();
            action.Perform();
            DoWait(5);
        }

        [Test]
        public void OtvaranjeDvaProzora()
        {
            this.NavigateTo("https://www.seleniumeasy.com/test/window-popup-modal-demo.html");
            IWebElement twitter = this.FindElement(By.XPath("//a[contains(@class, 'followeasy') and contains(., 'Twitter')]"));
            twitter.Click();
            DoWait(1);
            var popup = this.Driver.WindowHandles[1];
            this.Driver.SwitchTo().Window(popup);
            // Executes in popup window
            DoWait(1);
            IWebElement username = this.FindElement(By.Id("username_or_email"));
            username.SendKeys("nekimail@negde.com");
            DoWait(1);
            IWebElement password = this.FindElement(By.Id("password"));
            password.SendKeys("nekasifra");
            DoWait(3);
            this.Driver.Close(); // Closes popup window

            DoWait(2);
            this.Driver.SwitchTo().Window(this.Driver.WindowHandles[0]);

            IWebElement facebook = this.FindElement(By.XPath("//a[contains(@class, 'followeasy') and contains(., 'Facebook')]"));
            facebook.Click();
            DoWait(1);
            var popup1 = this.Driver.WindowHandles[1];
            this.Driver.SwitchTo().Window(popup1);
            // Executes in popup window
            DoWait(1);
            IWebElement username2 = this.FindElement(By.CssSelector("#email"));
            username2.SendKeys("mail2@gmail.com");
            DoWait(1);
            IWebElement password2 = this.FindElement(By.CssSelector("#pass"));
            password2.SendKeys("sifra2");
            DoWait(1);
            this.Driver.Close(); // Closes popup window

            DoWait(2);
            this.Driver.SwitchTo().Window(this.Driver.WindowHandles[0]);
            this.Driver.Close();
        }

        [Test]
        public void OtvaranjeDvaProzora2()
        {
            this.NavigateTo("https://www.seleniumeasy.com/test/window-popup-modal-demo.html");
            IWebElement button1 = this.FindElement(By.XPath("//div[@class='two-windows']/a"));
            //IWebElement button1 = this.FindElement(By.XPath("//a[contains(text(),'Follow Twitter & Facebook')]"));
            button1.Click();
            DoWait(1);
            var mainWindow = this.Driver.CurrentWindowHandle;

            foreach (var windowHandle in this.Driver.WindowHandles)
            {
                this.Driver.SwitchTo().Window(windowHandle);
                string title = this.Driver.Title;
                if (title.Contains("Twitter"))
                {
                    IWebElement username1 = this.FindElement(By.Id("username_or_email"));
                    username1.SendKeys("mail1@gmail.com");
                    DoWait(1);
                    IWebElement password1 = this.FindElement(By.XPath("//input[@id='password']"));
                    password1.SendKeys("sifra1");
                    DoWait(1);
                    this.Driver.Close();
                }
                if (title.Contains("Facebook"))
                {
                    IWebElement username2 = this.FindElement(By.CssSelector("#email"));
                    username2.SendKeys("mail2@gmail.com");
                    DoWait(1);
                    IWebElement password2 = this.FindElement(By.CssSelector("#pass"));
                    password2.SendKeys("sifra2");
                    DoWait(1);
                    this.Driver.Close();
                }
            }
            this.Driver.SwitchTo().Window(mainWindow);
        }


        [Test]
        public void EmmiKupovina()
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
        public void KupovinaSaPoredjenjemCena()
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
        public void SeleniumEasy_InputForms1()
        {
            //this.log.Store("*** Single Input Field***");
            this.NavigateTo("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            IWebElement inputField = this.FindElement(By.XPath("//input[@id='user-message']"));
            this.SendKeys("Kako napraviti SS sa Se?", false);
            this.FindElement(By.XPath("//button[contains(text(),'Show Message')]"))?.SendKeys(Keys.Enter);
            IWebElement msg = this.FindElement(By.XPath("//span[@id='display']"));
            Assert.AreEqual(inputField.GetAttribute("value"), msg.Text);
            this.DoWait(4);
        }

        [Test]
        public void SeleniumEasy_InputForms2()
        {
            this.NavigateTo("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            string xPath = "//input[@id='user-message']";
            IWebElement textPolje = this.Driver.FindElement(By.XPath(xPath));
            string pojamZaPretragu = "Selenium";
            textPolje.SendKeys(pojamZaPretragu);
            IWebElement dugme = this.Driver.FindElement(By.XPath("//button[contains(text(),'Show Message')]"));
            dugme.Click();
            IWebElement expected = this.Driver.FindElement(By.XPath("//span[@id='display']"));
            Assert.AreEqual(pojamZaPretragu, expected.Text);
            this.DoWait(4);
        }

        [Test]
        public void SeleniumEasy_InputForms3()
        {
            this.NavigateTo("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            IWebElement tekstUnos = this.FindElement(By.Id("user-message"));
            this.SendKeys("neki tekst", false, tekstUnos);
            DoWait(2);
            IWebElement showMessage = this.FindElement(By.XPath("//button[@onclick='showInput();']"));
            showMessage.Click();
            IWebElement actualMessage = this.FindElement(By.Id("display"));
            DoWait(4);
            Assert.AreEqual(actualMessage.Text, "neki tekst");
        }

        [Test]
        public void RegistrationQAFinalTest()
        {
            this.NavigateTo("http://test.qa.rs/");
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(By.XPath("//div[@id='registerLinkPlaceholder']/a")));
            this.DoWait(1);
            IWebElement registerNew = this.FindElement(By.XPath("//div[@id='registerLinkPlaceholder']/a"));
            registerNew.Click();
            this.DoWait(1);
            IWebElement ime = this.FindElement(By.Name("ime"));
            this.SendKeys("Milovan", false, ime);
            this.DoWait(1);
            IWebElement prezime = this.FindElement(By.Name("prezime"));
            this.SendKeys("Lazarevic", false, prezime);
            this.DoWait(1);
            IWebElement korisnicko = this.FindElement(By.Name("korisnicko"));
            this.SendKeys("MilLaz", false, korisnicko);
            this.DoWait(1);
            IWebElement email = this.FindElement(By.Name("email"));
            this.SendKeys("milovan@lazarevic.com", false, email);
            this.DoWait(1);
            IWebElement tel = this.FindElement(By.Name("telefon"));
            this.SendKeys("012/345-678", false, tel);
            this.DoWait(1);
            IWebElement drzava = this.FindElement(By.Name("zemlja"));
            var select = new SelectElement(drzava);
            select.SelectByValue("srb");
            this.DoWait(1);
            wait.Until(c => c.FindElement(By.Id("grad")));
            this.DoWait(1);
            IWebElement grad = this.FindElement(By.Id("grad"));
            select = new SelectElement(grad);
            select.SelectByIndex(9);
            this.DoWait(1);
            IWebElement ulica = this.FindElement(By.XPath("(//div[@id='address']/div)[2]/input"));
            this.SendKeys("Bulevar Lazarevica", false, ulica);
            this.DoWait(1);
            IWebElement pol = this.FindElement(By.Id("pol_m"));
            pol.Click();
            this.DoWait(1);
            IWebElement vesti = this.FindElement(By.Id("newsletter"));
            vesti.Click();
            this.DoWait(2);
            IWebElement registruj = this.FindElement(By.Name("register"));
            registruj.Click();
            this.DoWait(3);
            IWebElement uspeh = this.FindElement(By.XPath("//div[@class='alert alert-success']"));
            Assert.True(uspeh.Displayed);
            this.DoWait(3);
        }
    
        [Test]
        public void ShoppingCart()
        {
            this.NavigateTo("http://shop.qa.rs/");
            IWebElement kolicina = this.FindElement(By.XPath("//h3[contains(text(),'PRO')]/parent::div/following-sibling::div[1]//select"));
            var select = new SelectElement(kolicina);
            select.SelectByValue("10");
            this.DoWait(2);
            IWebElement order = this.FindElement(By.XPath("//h3[contains(text(),'PRO')]/parent::div/following-sibling::div[1]//input[@type='submit']"));
            order.Click();

            int qty = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[2]")).Text);
            int price = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[3]")).Text.Substring(1));
            int subtotal = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[4]")).Text.Substring(1));
            this.DoWait(3);

            Assert.AreEqual(qty * price, subtotal);

            this.DoWait(3);
        }

        [SetUp]
        public void SetUpTests()
        {
            //this.Driver = new FirefoxDriver();
            this.Driver = new ChromeDriver();
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