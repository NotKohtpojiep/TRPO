using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using FileEditor.Models;
using FileEditor.Services;

namespace FileEditor.ViewModels
{
    public class FirstQuestViewModel : BindableBase
    {
        public string Text { get; set; }
        public ObservableCollection<MenuItemObject> MenuItems { get; set; }

        public FirstQuestViewModel()
        {
            Init();
        }

        private void Init()
        {
            MenuItems = new ObservableCollection<MenuItemObject>(new List<MenuItemObject>
            {
                new MenuItemObject {Command = new RelayCommand<string>(_ => Text = ReadFromFile()), Content = "Открыть"},
                new MenuItemObject {Command = new RelayCommand<object>(_ => SaveFile(Text)), Content = "Сохранить как"},
                new MenuItemObject {Command = new RelayCommand<object>(_ => CloseProgram()), Content = "Выход"}
            });
        }

        private void CloseProgram()
        {
            Process.GetCurrentProcess().Kill();
        }

        private string ReadFromFile()
        {
            string result = null;
            try
            {
                result = new FileOperator().GetTextFileContent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Smth went wrong");
            }
            return result;
        }

        private void SaveFile(string content)
        {
            try
            {
                new FileOperator().SaveAsTxtFile(content);
            }
            catch (Exception e)
            {
                MessageBox.Show("Smth went wrong");
            }
        }
    }
}
