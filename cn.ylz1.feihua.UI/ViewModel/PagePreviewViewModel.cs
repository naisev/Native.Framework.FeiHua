using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using cn.ylz1.feihua.Code;
using System.Windows;

namespace cn.ylz1.feihua.UI.ViewModel
{
    class PagePreviewViewModel:BindableBase
    {
        public PagePreviewViewModel()
        {
            ClickAddCommand = new DelegateCommand(new Action(ClickAddExecute));
            ClickDelCommand = new DelegateCommand<object>(new Action<object>(ClickDelExecute));
            LoadCommand = new DelegateCommand(new Action(LoadExecute));
        }

        private ObservableCollection<PoemData> _datas;
        public ObservableCollection<PoemData> Datas
        {
            get { return _datas; }
            set
            {
                _datas = value;
                this.RaisePropertyChanged("Datas");
            }
        }

        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                this.RaisePropertyChanged("Key");
            }
        }

        private string _verse;
        public string Verse
        {
            get { return _verse; }
            set
            {
                _verse = value;
                this.RaisePropertyChanged("Verse");
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

        public DelegateCommand ClickAddCommand { get; set; }
        public DelegateCommand<object> ClickDelCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }

        public void ClickAddExecute()
        {
            PoemData.AddData(new PoemData
            {
                Key = this.Key,
                Verse = this.Verse,
                Info = this.Info
            });
            Datas = new ObservableCollection<PoemData>(PoemData.pd);
            MessageBox.Show("添加成功！");
        }
        public void ClickDelExecute(object parameter)
        {
            Common.CQLog.Debug("", parameter.GetType().ToString());
            if (parameter.GetType() != typeof(PoemData)) { return; }
            PoemData p = parameter as PoemData;
            PoemData.DelData(p);
            Datas = new ObservableCollection<PoemData>(PoemData.pd);
            MessageBox.Show("删除成功！");
        }
        public void LoadExecute()
        {
            if (PoemData.pd == null) { return; }
            Datas = new ObservableCollection<PoemData>(PoemData.pd);
        }
    }
}
