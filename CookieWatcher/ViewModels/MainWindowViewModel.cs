using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Prism.Mvvm;
using System;
using System.Timers;

namespace CookieWatcher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        Timer timer = new Timer(10000);
        IWebDriver driver;

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            // ブラウザの情報の方のクッキーを使用するために引数を入力する必要がある。
            // 引数のパスに関しては、今回は下に書いてある環境変数にて取得。
            ChromeOptions option = new ChromeOptions();
            var path = Environment.GetEnvironmentVariable("GoogleChromeUserDirectoryPath"
                ,EnvironmentVariableTarget.User );
            option.AddArgument("user-data-dir=" + path);

            var ds = ChromeDriverService.CreateDefaultService();
            ds.HideCommandPromptWindow = true;
            driver = new ChromeDriver(ds,option);

            driver.Navigate().GoToUrl(@"https://orteil.dashnet.org/cookieclicker/");

            timer.Elapsed += intervalProcess;
            timer.Start();
        }

        private void intervalProcess(object sender, ElapsedEventArgs e) {
            string pageSource = driver.PageSource;
            System.Diagnostics.Debug.WriteLine(
                driver.FindElement(By.Id("cookies")).Text
            );
        }
    }
}
