using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using WM_ReadBrowser.Databases;
using WM_ReadBrowser.Models;

namespace WM_ReadBrowser.ViewModels
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
            WebCollections = await _db.Database.Table<WebCollection>().ToListAsync();
        }

        [RelayCommand]
        async void Delete(WebCollection item)
        {

            await _db.Database.DeleteAsync(item);
            await InitData();
        }
    }
}
