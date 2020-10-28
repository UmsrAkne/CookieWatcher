using CookieWatcher.Models;
using OpenQA.Selenium;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CookieWatcher.ViewModels
{
    public class Watcher :BindableBase{

        IWebDriver driver;

        public string CookieCount {
            #region
            get => cookieCount.Replace("\r", " ").Replace("\n", " ").Replace("cookies  per second", " cps");
            set => SetProperty(ref cookieCount, value);
        }

        private string cookieCount = "0";
        #endregion

        public List<string> Buffs {
            #region
            get => buffs;
            private set => SetProperty(ref buffs, value);
        }

        private List<string> buffs = new List<string>();
        #endregion



        public Watcher(IWebDriver driver) {
            this.driver = driver;
        }

        /// <summary>
        /// バフの情報をアップデートします。
        /// </summary>
        public void updateBuffs() {
            var buffsElementList = driver.FindElements(By.CssSelector(".crate.enabled.buff"));
            var buffsList = new List<string>();

            if(buffsElementList.Count > 0) {
                foreach(var b in buffsElementList) {
                    buffsList.Add(b.Text + b.GetAttribute("style"));
                }
            }

            Buffs = buffsList;
        }
    }
}
