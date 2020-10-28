using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieWatcher.Models {

    /// <summary>
    /// ミニゲーム「ガーデン」に関する処理を担当するクラス
    /// </summary>

    public class Farmer {

        private IWebDriver driver;

        public Farmer(IWebDriver webDriver) {
            driver = webDriver;
        }
    }
}
