using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using cn.ylz1.feihua.UI.View;
using Prism.Commands;
using Prism.Mvvm;

namespace cn.ylz1.feihua.UI.ViewModel
{
    class MainWindowViewModel:BindableBase
    {
        public MainWindowViewModel()
        {
            ClickMenuCommand = new DelegateCommand<object>(ClickMenuExecute);
            ListContent = new ObservableCollection<string>();
            ListContent.Add("词库导入");
            ListContent.Add("浏览词库");
        }

        private ObservableCollection<string> _listContent;
        public ObservableCollection<string> ListContent
        {
            get { return _listContent; }
            set
            {
                _listContent = value;
                this.RaisePropertyChanged("ListContent");
            }
        }

        private int _choosedIndex;
        public int ChoosedIndex
        {
            get { return _choosedIndex; }
            set
            {
                _choosedIndex = value;
                this.RaisePropertyChanged("ChoosedIndex");
            }
        }

        public DelegateCommand<object> ClickMenuCommand { get; set; }

        public void ClickMenuExecute(object parameter)
        {
            if (typeof(Grid) != parameter.GetType())
            {
                return;
            }
            if (ChoosedIndex != 0 && ChoosedIndex != 1) { return; }
            switch (ChoosedIndex)
            {
                case 0:
                    (parameter as Grid).Children.RemoveAt(0);
                    (parameter as Grid).Children.Add(new PageImport());
                    break;
                case 1:
                    (parameter as Grid).Children.RemoveAt(0);
                    (parameter as Grid).Children.Add(new PagePreview());
                    break;
            }

        }
    }
}
