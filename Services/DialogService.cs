using System;
using Ookii.Dialogs.Wpf;

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
    }
}
