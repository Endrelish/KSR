namespace KSR1.View
{
    using Microsoft.Win32;

    public class WpfFileDialog
    {
        public string[] GetOpenFilePath(string filter)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = filter;
            dialog.Multiselect = true;
            dialog.ShowDialog();
            return dialog.FileNames;
        }

        public string GetSaveFilePath(string filter)
        {
            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.Filter = filter;
            dialog.DefaultExt = filter.Substring(filter.LastIndexOf("."));
            dialog.CreatePrompt = false;
            dialog.OverwritePrompt = true;
            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}