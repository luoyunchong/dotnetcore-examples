using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using System.Windows.Threading; 
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ChromiumWebBrowser webBrowser;  
        string html = "主打歌努力工作中....";
        public string HTML { get => html; set => SetProperty(ref html, value); }

        string url = "http://www.wyj55.cn/test/A187.php";
        public string URL { get => url; set => SetProperty(ref url, value); }

        public MainWindow()
        {
            InitializeComponent();

            CefSettings settings = new CefSettings(); 
            Cef.Initialize(settings);
            webBrowser = new ChromiumWebBrowser(URL);
            //webBrowser.Address = "http://www.wyj55.cn/test/A187.php";
            webBrowser.Width = 500;
            webBrowser.Height = 300;
            //隐藏滚动条
            webBrowser.FrameLoadEnd += OnBrowserFrameLoadEnd;

            webBrowser.LoadingStateChanged += OnLoadingStateChanged;

            this.Closing += dede;

             
            //var sp = new StackPanel();
            //sp.Children.Add(webBrowser);
            //sp.Children.Add(t1);
            //Content = sp;

            main.Children.Add(webBrowser);

            DataContext = this;

        }
        private void dede(object sender, object args)
        {
            Cef.Shutdown();
        }
        
        private async void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (!args.IsLoading)
            {
                // 加载完毕,读取

                HTML = await webBrowser.GetSourceAsync();

                //source.Text=HTML;
            }
        }

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
        {
            if (args.Frame.IsMain)
            {
                args
                    .Browser
                    .MainFrame
                    .ExecuteJavaScriptAsync(
                    "document.body.style.overflow = 'hidden'");
            }
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, bool force = false)
        {
            if (!force && EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.Load(URL);
        }
    }
}
