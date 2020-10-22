using OpenQA.Selenium;
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

        public int MP {
            #region
            get => mp;
            set {
                SetProperty(ref mp, value);
                RaisePropertyChanged(nameof(MPText));
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
    }
}
