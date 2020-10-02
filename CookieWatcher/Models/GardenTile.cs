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
        public int CropID {
            #region
            get => cropID;
            set => SetProperty(ref cropID, value);
        }

        private int cropID;
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
