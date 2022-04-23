using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace BlazorClientApp.Pages;
public class Collapsible_razorBase:ComponentBase
{
    
    [Inject]
    public IJSRuntime JS { get; set; }
    public bool Show { get; set; }
    public string Msg { get; set; } 
    [Parameter] public string Title { get; set; } = "Delete";
    [Parameter] public string Class { get; set; } = "btn btn-danger";
    [Parameter] public string Message { get; set; } = "Are you sure?";
    [Parameter] public EventCallback<bool> ConfirmedChanged { get; set; }

    public async Task Confirmation(bool value)
    {
        Show = false;
        Msg = "message works";
        await ConfirmedChanged.InvokeAsync(value);
        await JS.InvokeVoidAsync("AutoCloseAlert");
    }

    protected override async void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Show = false;
            Msg = "message works";
            await JS.InvokeVoidAsync("AutoCloseAlert");
        }
    }

    public void ShowPop()
    {
        Show = true;
    }
}