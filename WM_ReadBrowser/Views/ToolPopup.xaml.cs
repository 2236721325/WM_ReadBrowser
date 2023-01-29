using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui;
using WM_ReadBrowser.Databases;
using WM_ReadBrowser.Messages;
using WM_ReadBrowser.ViewModels;

namespace WM_ReadBrowser.Views;

public partial class ToolPopup : Popup 
{
	private readonly ToolViewModel _vm;

	private readonly MyDatabase _db;
    public ToolPopup(ToolViewModel vm, MyDatabase db)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
        _db = db;
        
        
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		var url = _vm.WebCollections[e.ItemIndex].Url;
		WeakReferenceMessenger.Default.Send(new GoToUrlMessage(url));
        Close();
    }


}