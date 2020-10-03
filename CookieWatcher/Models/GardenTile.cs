using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieWatcher.Models
{

    /// <summary>
    /// ガーデンの１マスを表します。
    /// </summary>

    public class GardenTile : BindableBase
    {

        /// <summary>
        /// ID が番号ではなく文字列になっているのは、HTML上の ID 属性が文字列なため
        /// </summary>
        public string CropIDName {
            #region
            get => cropIDName;
            set => SetProperty(ref cropIDName, value);
        }

        private string cropIDName;
        #endregion

        public string CropName {
            #region
            get => cropName;
            set => SetProperty(ref cropName, value);
        }

        public string cropName;
        #endregion

        public int Level {
            #region
            get => level;
            set => SetProperty(ref level, value);
        }
        private int level = 1;
        #endregion

        public Point Point { get; set; }
    }
}
