using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using Soccer.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace Soccer.Service.Implementation
{
    public class CookiesService : ICookiesService
    {
        readonly ICookiesRepository _cookiesRepository;
        readonly IConfiguration _configuration;

        public CookiesService(ICookiesRepository cookiesRepository, IConfiguration configuration)
        {
            _cookiesRepository = cookiesRepository;
            _configuration = configuration;
        }

        private List<CookiesModel> GetCookiesFromSBOWebsite()
        {
            List<CookiesModel> cookiesModels = new List<CookiesModel>();
            string url = _configuration["URL:SBO"];
            string sportUrl = _configuration["URL:SBOSport"];
            string resultUrl = _configuration["URL:SBOSport-results-more"];
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            IWebElement inputAccount = driver.FindElement(By.CssSelector(_configuration["Selenium:UserNameElement"]));
            inputAccount.Clear();
            inputAccount.SendKeys(_configuration["Account:SBO:UserName"]);

            IWebElement inputPassword = driver.FindElement(By.CssSelector(_configuration["Selenium:PasswordElement"]));
            inputPassword.Clear();
            inputPassword.SendKeys(_configuration["Account:SBO:Password"]);

            IWebElement submitButton = driver.FindElement(By.ClassName(_configuration["Selenium:LoginButtonElement"]));
            submitButton.Click();
            Thread.Sleep(2000);

            driver.Navigate().GoToUrl(sportUrl);
            Thread.Sleep(2000);

            driver.Navigate().GoToUrl(resultUrl);

            var cookies = driver.Manage().Cookies.AllCookies;
            foreach (var cookie in cookies)
            {
                CookiesModel cookiesModel = new CookiesModel();
                cookiesModel.Website = _configuration["Cookies:Website"];
                cookiesModel.Name = cookie.Name;
                cookiesModel.Value = cookie.Value;
                cookiesModel.Path = cookie.Path;
                cookiesModel.Domain = cookie.Domain;
                cookiesModels.Add(cookiesModel);
            }
            driver.Quit();

            return cookiesModels;
        }

        public void UpdateSBOCookies()
        {
            _cookiesRepository.UpdateCookies(GetCookiesFromSBOWebsite());
        }

        public CookiesModel GetCookesByCondition(string website, string name)
        {
            return _cookiesRepository.GetCookiesByCondition(website, name);
        }
    }
}
