using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.ReadBrowser.Databases;
using WM.ReadBrowser.Messages;
using WM.ReadBrowser.Models;
using WM.ReadBrowser.Views;

namespace WM.ReadBrowser.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        string url;

        [ObservableProperty]
        UrlWebViewSource source;

        private readonly MyDatabase _db;

        public MainViewModel(MyDatabase db)
        {
            Source = new UrlWebViewSource()
            {
                Url = "https://cn.bing.com/"
            };
            _db = db;
            WeakReferenceMessenger.Default.Register<GoToUrlMessage>(this, (r, m) =>
            {
                Source = new UrlWebViewSource()
                {
                    Url = m.Value
                };
            });
        }

        partial void OnSourceChanged(UrlWebViewSource value)
        {
            Url = Source.Url;
        }

        [RelayCommand]
        void GoTo(Entry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Text))
            {
                string text = "地址不正确";
                var toast = Toast.Make(text);

                toast.Show();
                return;
            }
            Source = new UrlWebViewSource()
            {
                Url = entry.Text
            };
        }
        [RelayCommand]
        void More()
        {
            var popup =Ioc.Default.GetRequiredService<ToolPopup>();
            popup.Size = new Size(300, 500);
            App.Current.MainPage.ShowPopup(popup);
        }
        [RelayCommand]
        async void Collect()
        {
            string name = await App.Current.MainPage
                 .DisplayPromptAsync("收藏网页", "网页名称?默认显示为地址。",
                 accept: "收藏", cancel: "取消", initialValue: Url);
            if (string.IsNullOrWhiteSpace(name))
            {
                var toast = Toast.Make("名称不能为空！");
                await toast.Show();
                return;
            }

            var has = await _db.Database.Table<WebCollection>()
                 .Where(e => e.Url == Url || e.Name == name).FirstOrDefaultAsync();
            if (has != null)
            {
                var toast = Toast.Make("名称或地址已经在收藏夹中！");
                await toast.Show();
                return;
            }

            await _db.Database.InsertAsync(new WebCollection()
            {
                Name = name,
                Url = Url
            });
            await App.Current.MainPage.DisplayAlert("好消息", "已经添加至收藏夹！", "了解");


        }


    }
}
