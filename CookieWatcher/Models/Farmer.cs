using OpenQA.Selenium;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CookieWatcher.Models {

    /// <summary>
    /// ミニゲーム「ガーデン」に関する処理を担当するクラス
    /// </summary>

    public class Farmer : BindableBase{

        private IWebDriver driver;

        public Farmer(IWebDriver webDriver) {
            driver = webDriver;
        }

        public List<GardenTile> Crops {
            #region
            get => crops;
            set => SetProperty(ref crops, value);
        }

        private List<GardenTile> crops = new List<GardenTile>();
        #endregion

        private string NotificationSoundFilePath = @"C:\Windows\Media\chord.wav";

        public Dictionary<string,GardenTile> CropDictionary {
            #region
            get => cropDictionary;
            set => SetProperty(ref cropDictionary, value);
        }

        private Dictionary<string, GardenTile> cropDictionary = new Dictionary<string, GardenTile>();
        #endregion

        public void updateGarden() {
            if(driver.FindElements(By.Id("gardenField")).Count == 0) {
                return;
            }

            var cropElements = driver.FindElements(By.ClassName("gardenTileIcon"));
            List<GardenTile> cropList = new List<GardenTile>();
            var cropMaturing = false;

            foreach(var crElement in cropElements) {
                var att = crElement.GetAttribute("style");

                // gardeTileオブジェクトは、都度生成ではなく、辞書から既存のオブジェクトを取り出す
                var elementID = crElement.GetAttribute("id");
                if (!CropDictionary.ContainsKey(elementID)) {
                    GardenTile tile = new GardenTile();
                    CropDictionary[elementID] = tile;
                    tile.CropIDName = elementID;
                }

                GardenTile gc = CropDictionary[elementID];

                if(!att.Contains("display: block")) {
                    // 作物が植えられていない場合に突入するブロック 実行後に即リターン
                    gc.Level = 0;
                    gc.CropName = "";
                    cropList.Add(gc);
                    continue;
                }

                // toolTip経由で直接取得するのが一番早いが、技術的に無理そう。
                // なので、style属性に表記されている background-position の座標から間接的に成長レベルを取得する。
                // タイルアイコン１枚あたり 48x48 でレベルが上がる毎に x座標が -48 ずつ増える。
                Regex r = new Regex("background-position: (?<n1>-*[0-9]*)px (?<n2>-*[0-9]*)");
                Match m = r.Match(att);

                int tileIconSize = 48;

                // X座標は成長レベル Y座標は作物の種類を表す。
                Point bgPos = new Point(int.Parse(m.Groups["n1"].Value), int.Parse(m.Groups["n2"].Value));
                var growLevel = Math.Abs(bgPos.X / tileIconSize);
                gc.Level = growLevel;
                gc.setCropName((int)Math.Abs(bgPos.Y / tileIconSize));

                if (gc.Maturing) {
                    cropMaturing = true;
                }

                cropList.Add(gc);
            }

            if(cropMaturing) {
                new System.Media.SoundPlayer(NotificationSoundFilePath).Play();
            }

            Crops = cropList;
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
