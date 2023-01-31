using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using WM.ReadBrowser.Databases;
using WM.ReadBrowser.Models;

namespace WM.ReadBrowser.ViewModels
{
    public partial class ToolViewModel : ObservableObject
    {
        private readonly MyDatabase _db;

        [ObservableProperty]
        List<WebCollection> webCollections;

        public ToolViewModel(MyDatabase db)
        {
            _db = db;

            Task.Run(() => InitData()).Wait();
           
        }

        public async Task InitData()
        {
            try
            {
                WebCollections = await _db.Database.Table<WebCollection>().ToListAsync();

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("异常InitData", ex.Message+"|"+ex.ToString(), "了解");
            }
        }

        [RelayCommand]
        async void Delete(int Id)
        {

            try
            {
                //_db.Database.DeleteAsync(object) 非泛型接口在 release版本中报错，在debug中有效 我直接好家伙。。。。
                await _db.Database.DeleteAsync<WebCollection>(primaryKey: Id);
                await InitData();
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert($"异常Delete", message: ex.Message + "|" + ex.ToString(), "了解");
            }
            
        }
    }
}
