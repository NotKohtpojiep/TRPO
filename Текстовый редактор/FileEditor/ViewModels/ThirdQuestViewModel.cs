using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm;
using FileEditor.Models;
using FileEditor.Services;

namespace FileEditor.ViewModels
{
    public class ThirdQuestViewModel : BindableBase
    {
        public ObservableCollection<MenuItemObject> MenuItems { get; set; }

        public ThirdQuestViewModel()
        {
            Init();
        }

        private void Init()
        {
            MenuItems = new ObservableCollection<MenuItemObject>(new List<MenuItemObject>
            {
                new MenuItemObject {Command = new RelayCommand<string>(_ => ShowFileInfo()), Content = "Информация о файле"}
            });
        }

        private void ShowFileInfo()
        {
            FileOperator fileOperator = new FileOperator();
            string filePath = fileOperator.GetPathByOpen(null);
            FileData fileInfo = fileOperator.GetFileInfo(filePath);
            MessageBox.Show(fileInfo.GetFileInfo());
        }
    }

}
