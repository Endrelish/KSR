namespace KSR1
{
    using System.Collections.Generic;
    using System.Windows;

    using KSR1.Model;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerator<string> enumerator;

        private IEnumerable<string> list;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Button.Content = "NEXT";
            if (this.list == null)
            {
                this.list = Reader.ReadDataString("D:\\Downloads\\reuters21578\\reut2-001.sgm");
                this.enumerator = this.list.GetEnumerator();
            }

            this.TextBlock.Text = this.enumerator.Current;
            this.enumerator.MoveNext();
        }
    }
}