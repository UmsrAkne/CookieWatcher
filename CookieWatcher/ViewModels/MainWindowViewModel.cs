using CookieWatcher.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Timers;

namespace CookieWatcher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        Timer timer = new Timer(10000);
        IWebDriver driver;
        
        public DateTime LastUpdateDate {
            #region 
            get => lastUpdateDate;
            set => SetProperty(ref lastUpdateDate, value);
        }

        private DateTime lastUpdateDate = DateTime.Now;
        #endregion

        public Watcher Watcher {
            #region
            get => watcher;
            set => watcher = value;
        }

        public Watcher watcher;
        #endregion

        public CookieController CookieController {
            #region
            get => cookieController;
            set => cookieController = value;
        }

        private CookieController cookieController;
        #endregion;

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

            watcher = new Watcher(driver);
            cookieController = new CookieController(driver);

            timer.Elapsed += intervalProcess;
            timer.Start();

        }

        private void intervalProcess(object sender, ElapsedEventArgs e) {
            if(driver.FindElements(By.Id("cookies")).Count > 0) {
                watcher.CookieCount = driver.FindElement(By.Id("cookies")).Text;
            }

            if (CookieController.EnableGCAutoClick) {
                CookieController.ClickGCCommand.Execute();
            }

            Watcher.updateBuffs();

            LastUpdateDate = DateTime.Now;
        }

        public DelegateCommand CloseDriverCommand {
            #region 
            get => closeDriverCommand ?? (closeDriverCommand = new DelegateCommand(
                () => {
                    driver.Close();
                }
            ));
        }

        private DelegateCommand closeDriverCommand;
        #endregion
    }
}
