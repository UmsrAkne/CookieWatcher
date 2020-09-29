using OpenQA.Selenium;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieWatcher.Models {
    public class CookieController : BindableBase{

        private IWebDriver driver;

        public CookieController(IWebDriver dirver) {
            this.driver = dirver;
        }

        /// <summary>
        /// true になっている場合、MainWindowViewModel のタイマー内で ClickGCCommand が自動実行されます
        /// </summary>
        public bool EnableGCAutoClick {
            #region
            get => enableGCAutoClick;
            set => SetProperty(ref enableGCAutoClick, value);
        }
        private bool enableGCAutoClick = false;
        #endregion

        /// <summary>
        /// 大クッキーを自動操作によって１クリックします。
        /// </summary>
        public DelegateCommand ClickCookieCommand {
            #region
            get => clickCookieCommand ?? (clickCookieCommand = new DelegateCommand(
                () => {
                    string bigCookieButtonID = "bigCookie";
                    if (existID(bigCookieButtonID)) {
                        driver.FindElement(By.Id(bigCookieButtonID)).Click();
                    }
                }
            ));
        }

        private DelegateCommand clickCookieCommand;
        #endregion

        /// <summary>
        /// 自動操作によってゴールデンクッキーをクリックします。
        /// </summary>
        public DelegateCommand ClickGCCommand {
            #region
            get => clickGCCommand ?? (clickGCCommand = new DelegateCommand(
                () => {
                    string gcClassName = "shimmer";
                    string gcID = "shimmers";

                    if(existID(gcID) && existClass(gcClassName)) {
                        driver.FindElement(By.ClassName(gcClassName)).Click();
                    }
                }
            ));
        }

        private DelegateCommand clickGCCommand;
        #endregion

        private bool existID(string id) => driver.FindElements(By.Id(id)).Count > 0;
        private bool existClass(string className) => driver.FindElements(By.ClassName(className)).Count > 0;


    }
}
