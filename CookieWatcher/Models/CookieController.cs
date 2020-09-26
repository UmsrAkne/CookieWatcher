using OpenQA.Selenium;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieWatcher.Models {
    public class CookieController {

        private IWebDriver driver;

        public CookieController(IWebDriver dirver) {
            this.driver = dirver;
        }

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


private bool existID(string id) => driver.FindElements(By.Id(id)).Count > 0;
    }
}
