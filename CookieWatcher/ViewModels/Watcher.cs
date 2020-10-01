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

        public List<string> Crops {
            #region
            get => crops;
            set => SetProperty(ref crops, value);
        }

        private List<string> crops = new List<string>();
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

        public void updateGarden() {
            if(driver.FindElements(By.Id("gardenField")).Count == 0) {
                return;
            }

            var cropElements = driver.FindElements(By.ClassName("gardenTile"));
            List<string> cropList = new List<string>();
            foreach(var crElement in cropElements) {
                var att = crElement.GetAttribute("style");
                string s = (att.Contains("display: block;")) ? "planted" : "";

                cropList.Add(s);
            }

            Crops = cropList;
        }
    }
}
