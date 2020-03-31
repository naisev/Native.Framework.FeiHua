using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cn.ylz1.feihua.Code
{
    public class ImportInfo: BindableBase
    {
        private Visibility _btnVisibility;
        public Visibility BtnVisibility
        {
            get { return _btnVisibility; }
            set
            {
                _btnVisibility = value;
                this.RaisePropertyChanged("BtnVisibility");
            }
        }

        private Visibility _tipsVisibility;
        public Visibility TipsVisibility
        {
            get { return _tipsVisibility; }
            set
            {
                _tipsVisibility = value;
                this.RaisePropertyChanged("TipsVisibility");
            }
        }

        private string _info;

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                this.RaisePropertyChanged("Info");
            }
        }

    }
}
