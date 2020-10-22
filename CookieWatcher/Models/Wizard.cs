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
    public class Wizard : BindableBase{

        public int MP {
            #region
            get => mp;
            set => SetProperty(ref mp, value);
        }

        private int mp;
        #endregion


    }
}
