using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using FileEditor.Models;
using FileEditor.Services;

namespace FileEditor.ViewModels
{
    public class SecondQuestViewModel : BindableBase
    {
        public string Text { get; set; }
        public ObservableCollection<MenuItemObject> MenuItems { get; set; }

        public SecondQuestViewModel()
        {
            Init();
        }

        private void Init()
        {
            MenuItems = new ObservableCollection<MenuItemObject>(new List<MenuItemObject>
            {
                new MenuItemObject {Command = new RelayCommand<string>(_ =>  ReadFromFile()), Content = "Показать таблицу в блокноте"},
                new MenuItemObject {Command = new RelayCommand<string>(_ => SaveHtmlFileTable(Text)), Content = "Отобразить таблицу в браузере"},
                new MenuItemObject {Command = new RelayCommand<object>(_ => CloseProgram()), Content = "Выход"}
            });
        }

        private void CloseProgram()
        {
            Process.GetCurrentProcess().Kill();
        }

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        private void ReadFromFile()
        {
            //Process.Start("C:\\Windows\\System32\\notepad.exe", Text);
            try
            {
                Process notepad = Process.Start("notepad.exe");
                if (notepad == null)
                {
                    throw new Exception("Process is null");
                }
                notepad.WaitForInputIdle();
                Clipboard.SetText(Text);
                SendMessage(FindWindowEx(notepad.MainWindowHandle, new IntPtr(0), "Edit", null), 0x000C, 0, Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Smth went wrong");
            }
        }

        private void SaveHtmlFileTable(string content)
        {
            string filePath = new FileOperator().SaveAsHtmlFile(content);
            var proc = Process.Start(@"cmd.exe ", @"/c " + filePath);
            try
            {
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Smth went wrong");
            }
        }
    }
}
