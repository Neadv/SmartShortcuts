using System;
using Ookii.Dialogs.Wpf;
using System.Windows;

namespace SmartShortcuts.Services
{
    public static class DialogService
    {
        private static VistaOpenFileDialog fileDialog = new VistaOpenFileDialog();
        private static VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog();

        public static string ShowFileDialog()
        {
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            var result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value)
                return fileDialog.FileName;
            return null;
        }
        public static string ShowFolderDialog()
        {
            var result = folderDialog.ShowDialog();
            if (result.HasValue && result.Value)
                return folderDialog.SelectedPath;
            return null;
        }

        public static bool WarningDialog(string message)
        {
            if (MessageBox.Show(message, "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                return true;
            return false;
        }
    }
}
