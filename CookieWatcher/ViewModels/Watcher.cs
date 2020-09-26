using OpenQA.Selenium;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieWatcher.ViewModels
{
    public class Watcher :BindableBase{

        IWebDriver driver;

        public string CookieCount {
            #region
            get => cookieCount;
            set => SetProperty(ref cookieCount, value);
        }

        private string cookieCount = "0";
        #endregion

        public Watcher(IWebDriver driver) {
            this.driver = driver;
        }

    }
}
