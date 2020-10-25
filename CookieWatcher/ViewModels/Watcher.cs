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
    }
}
