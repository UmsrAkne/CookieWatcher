using OpenQA.Selenium;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ウィザードタワー（グリモア）を管理する機能を持つクラスです。
/// </summary>

namespace CookieWatcher.Models
{
    public class Wizard : BindableBase {

        public Wizard(IWebDriver driver) {
            Driver = driver;
        }


        public IWebDriver Driver { private get; set; }

        public int MP {
            #region
            get => mp;
            set {
                SetProperty(ref mp, value);
                RaisePropertyChanged(nameof(MPText));
                SummonCookieCommand.RaiseCanExecuteChanged();
            }
        }

        private int mp;
        #endregion

        public int MaxMP {
            #region
            get => maxMP;
            set {
                SetProperty(ref maxMP, value);
                RaisePropertyChanged(nameof(MPText));
            }
        }
        private int maxMP;
        #endregion

        public string MPText {
            get => $"{MP} / {MaxMP}";
        }

        /// <summary>
        /// グリモア関連の情報をアップデートします。通常、10秒に一回、タイマー内で呼び出しを行います。
        /// </summary>
        /// <param name="driver"></param>
        public void update(IWebDriver driver) {

            // xx/yy の形式で記された２つの数値を変換してプロパティに格納。
            string[] mpText = driver.FindElement(By.Id("grimoireBarText")).Text.Split('/','(');
            MP = int.Parse(mpText[0]);
            MaxMP = int.Parse(mpText[1]);
        }

        public DelegateCommand SummonCookieCommand {
            #region
            get => summonCookieCommand ?? (summonCookieCommand = new DelegateCommand(
                () => {
                    const string spellID = "grimoireSpell1";
                    if(Driver.FindElements(By.Id(spellID)).Count > 0) {
                        Driver.FindElement(By.Id(spellID)).Click();
                    }
                },
                () => {
                    const string spellPriceID = "grimoirePrice1";
                    if (Driver.FindElements(By.Id(spellPriceID)).Count > 0) {
                        return int.Parse(Driver.FindElement(By.Id(spellPriceID)).Text) <= MP;
                    }
                    else {
                        return false;
                    }
                }
            ));
        }

        private DelegateCommand summonCookieCommand;
        #endregion
    }
}
