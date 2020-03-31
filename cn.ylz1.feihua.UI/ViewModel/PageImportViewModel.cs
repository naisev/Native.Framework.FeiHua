using cn.ylz1.feihua.Code;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace cn.ylz1.feihua.UI.ViewModel
{
    class PageImportViewModel : BindableBase
    {
        public PageImportViewModel()
        {
            ClickImportCommand = new DelegateCommand(new Action(ClickImportExecute));
            Info = new ImportInfo();
            Info.BtnVisibility = Visibility.Visible;
            Info.TipsVisibility = Visibility.Hidden;
            Info.Info = string.Empty;
        }

        private ImportInfo _info;
        public ImportInfo Info
        {
            get { return _info; }
            set
            {
                _info = value;
                this.RaisePropertyChanged("Info");
            }
        }

        public DelegateCommand ClickImportCommand { get; set; }

        public void ClickImportExecute()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "词库(*.db)|*.db";
            string file = string.Empty;
            if (dialog.ShowDialog() == true)
            {
                file = dialog.FileName;
            }
            if (file == string.Empty)
            {
                MessageBox.Show("未选择文件！");
                return;
            }

            try
            {
                Info.BtnVisibility = Visibility.Hidden;
                Info.TipsVisibility = Visibility.Visible;
                Thread tImport = new Thread(new ThreadStart(() =>
                {
                    SQLiteConnection conn = new SQLiteConnection($"data source={file};");
                    conn.Open();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand("Select * from Poem where 1=1", conn));
                    DataSet set = new DataSet();
                    adapter.Fill(set, "Poem");
                    int affected = PoemData.ImportData(set, Info);
                    set.Dispose();
                    adapter.Dispose();
                    conn.Dispose();
                    Info.BtnVisibility = Visibility.Visible;
                    Info.TipsVisibility = Visibility.Hidden;
                    MessageBox.Show($"导入成功！共导入{affected}条诗句！");
                }));
                tImport.Start();
            }
            catch
            {
                MessageBox.Show("导入错误！不是正确词库！");
                Info.BtnVisibility = Visibility.Visible;
                Info.TipsVisibility = Visibility.Hidden;
            }
        }

    }
    
}
