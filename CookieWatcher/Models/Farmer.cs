using OpenQA.Selenium;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CookieWatcher.Models {

    /// <summary>
    /// ミニゲーム「ガーデン」に関する処理を担当するクラス
    /// </summary>

    public class Farmer {

        private IWebDriver driver;

        public Farmer(IWebDriver webDriver) {
            driver = webDriver;
        }

        public DelegateCommand<ListViewItem> HarvestCommand {
            #region
            get => harvestCommand ?? (harvestCommand = new DelegateCommand<ListViewItem>((ListViewItem param) => {
                var gardenTile = param.Content as GardenTile;
                if (gardenTile != null && driver.FindElement(By.Id(gardenTile.CropIDName)).Displayed) {
                    driver.FindElement(By.Id(gardenTile.CropIDName)).Click();
                }
            }));
        }

        private DelegateCommand<ListViewItem> harvestCommand;
        #endregion

    }
}
