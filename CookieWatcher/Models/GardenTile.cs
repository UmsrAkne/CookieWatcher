using Prism.Commands;
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
            get => cropIDName.Replace("Icon", "");
            set => SetProperty(ref cropIDName, value);
        }

        private string cropIDName;
        #endregion

        public string CropName {
            #region
            get => cropName;
            set => SetProperty(ref cropName, value);
        }

        private string cropName = "";
        #endregion

        public int Level {
            #region
            get => level;
            set {
                if(level == 3 && value == 4){ // level 3 は完熟一歩手前, level 4 は完熟状態
                    Maturing = true;
                }
                else {
                    Maturing = false;
                }

                SetProperty(ref level, value);
            }
        }
        private int level = 1;
        #endregion

        public Point Point { get; set; }

        /// <summary>
        /// 作物が完熟状態になった直後に true になり、その後、Level の set が参照されたタイミングで false に戻ります。 
        /// </summary>
        public bool Maturing { get; private set; } = false;

        /// <summary>
        /// このマスに作物が植えられているかを取得します。
        /// </summary>
        public bool Exist { get => CropName != ""; }

        /// <summary>
        /// インデックスから作物の名前を設定します。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void setCropName(int index) {
            CropName = cropNames[index];
        }

        private String[] cropNames = {
            "Bakers wheat",
            "Thumbcorn",
            "Cronerice",
            "planted",
            "planted",

            "planted",
            "planted",
            "planted",
            "BakeBerry",
            "planted",

            "planted",
            "planted",
            "planted",
            "planted",
            "planted",

            "planted",
            "planted",
            "planted",
            "planted",
            "planted",

            "planted",
            "planted",
            "planted",
            "planted",
            "planted",

            "planted",
            "planted",
            "planted",
            "planted",
            "planted",

            "planted",
            "planted",
        };

    }

}
