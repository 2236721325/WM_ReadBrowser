using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using WM.ReadBrowser.Databases;
using WM.ReadBrowser.Messages;
using WM.ReadBrowser.ViewModels;

namespace WM.ReadBrowser;

public partial class MainPage : ContentPage
{

    private readonly MainViewModel _vm;

    private readonly MyDatabase _db;
    public MainPage(MainViewModel vm, MyDatabase db)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
        _db = db;
    }
    System.Timers.Timer timer;

    private string script = @"
var imgs= document.getElementsByTagName(""img"");
var videos=document.getElementsByTagName(""video"");
var iframes=document.getElementsByTagName(""iframe"");
var divs=document.getElementsByTagName(""div"");
for (let index = imgs.length-1; index >= 0; index--) {
    imgs[index].remove();
}
for (let index = videos.length-1; index >= 0; index--) {
    videos[index].remove();
}
for (let index = iframes.length-1; index >= 0; index--) {
    iframes[index].remove();
}

for (let index = divs.length-1; index >= 0; index--) {
    if(divs[index].style.background.includes(""url"")){
        divs[index].parentNode.remove();
    }
}";
    //在js定时执行的话 页面切换 后就失效了  所以在C#中定时执行。
    protected override void OnAppearing()
    {



        timer = new System.Timers.Timer();

        timer.Interval = 30;


        timer.Elapsed += delegate
        {
            Dispatcher.Dispatch(() =>
            {
                try
                {
                    //这个接口参数不能有换行符，要有；
                    webView.EvaluateJavaScriptAsync(script.Replace("\r\n", ""));

                }
                catch (Exception _e)
                {
                    string text = _e.Message;
                    var toast = Toast.Make(text);

                    toast.Show();
                }
            });
        };

        timer.Start();
        base.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        timer.Dispose();

        base.OnDisappearing();
    }
    DateTime? lastPressedTime;

    //连按两次退出
    protected override bool OnBackButtonPressed()
    {
        if (webView.CanGoBack)
        {
            webView.GoBack();
            return true;
        }
        var current = DateTime.Now;
        if (lastPressedTime != null && (current - lastPressedTime.Value) < TimeSpan.FromSeconds(2))
        {
            return base.OnBackButtonPressed();
        }

        lastPressedTime = current;

        string text = "再按一次退出";
        var toast = Toast.Make(text);

        toast.Show();
        return true;


    }

    private void goForword(object sender, EventArgs e)
    {
        if (webView.CanGoForward)
        {
            webView.GoForward();
        }
    }


    private void goBack(object sender, EventArgs e)
    {
        if (webView.CanGoBack)
        {
            webView.GoBack();
        }
    }



    private void webView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        _vm.Url = e.Url;
        entry_url.CursorPosition = 0;

        webView.EvaluateJavaScriptAsync(script.Replace("\r\n", ""));


    }

    private void webView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        webView.EvaluateJavaScriptAsync(script.Replace("\r\n", ""));

    }
}

