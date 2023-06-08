using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SeleniumExample
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.Port = 5001;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(service, options);
            //some actions may not work on different resolution            
            driver.Manage().Window.Size = new System.Drawing.Size(1366, 768);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            driver.Navigate().GoToUrl("https://edition.cnn.com/sport");

            By cookieButtonSelector = By.Id("onetrust-accept-btn-handler");            
            wait.Until(d => d.FindElement(cookieButtonSelector).Displayed);
            IWebElement cookieButton = driver.FindElement(cookieButtonSelector);
            cookieButton.Click();

            driver.Navigate().Refresh();    //evaiding bug when searchfield disappear
            IWebElement searchIcon = driver.FindElement(By.CssSelector("#headerSearchIcon > svg"));
            searchIcon.Click();

            IWebElement searchField = driver.FindElement(By.XPath("//*[@id='pageHeader']/div/div/div[2]/div/div[1]/form/input"));
            searchField.SendKeys("Manchester United");
            searchField.Submit();

            IWebElement CnnLogo = driver.FindElement(By.XPath("//*[@title='CNN logo']"));
            CnnLogo.Click();
            IWebElement LiveTvButton = driver.FindElement(By.XPath("//*[@data-zjs-component_text='Live TV']"));
            LiveTvButton.Click();

            try
            {
                //product restricted from Lithuanian ip
                By backButtonLocator = By.XPath("//*[@class='user-account-restricted-shared__button user-account-restricted__button']");
                IWebElement backButton = wait.Until(driver => driver.FindElement(backButtonLocator));
                backButton.Click();                
            }
            catch(WebDriverTimeoutException)
            {
                Console.WriteLine("A error has ocured: there is no such button. Back to previous page");
                driver.Navigate().Back();
            }

            driver.Navigate().GoToUrl("https://edition.cnn.com/sport");

            IWebElement headerButton = driver.FindElement(By.ClassName("header__nav-item-link"));
            headerButton.Click();           
            driver.Navigate().Back();

            IWebElement tennisButton = driver.FindElement(By.LinkText("Tennis"));
            tennisButton.Click();
            driver.Navigate().Back();

            tennisButton = driver.FindElement(By.PartialLinkText("Tenn"));
            tennisButton.Click();

            IWebElement sportsButton = driver.FindElement(By.XPath("/html/body/header/div/div[3]/div/div/nav/div/div/div[1]/div[1]/div/a[2]"));
            driver.Navigate().Back(); 

            Console.ReadLine();
            driver.Quit();
        }
    }
}
