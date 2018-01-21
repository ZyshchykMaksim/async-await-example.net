using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SynchronizationContext_Exemple_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClick_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var syncContext = SynchronizationContext.Current;

                Task.Factory.StartNew(() =>
                    {
                        var isThreadInfo = Thread.CurrentThread.IsThreadPoolThread;

                        TbText.Text = (2 + 2).ToString();

                        ////выполняем маршалинг
                        syncContext.Post(state =>
                        {
                            TbText.Text = (2 + 2).ToString();
                        }, null);
                    }
                , new CancellationToken()
                , TaskCreationOptions.None
                , TaskScheduler.Default);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
