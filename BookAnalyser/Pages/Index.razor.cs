using BookAnalyser.ViewModels;
using BookAnalyserLib;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookAnalyser.Pages
{
    public partial class Index
    {
        private FileUploadDataItem FileItem { get; set; }

        public List<WordCountResult> WordCountResults { get; set; }

        [Inject]
        IWebHostEnvironment env { get; set; }

        [Inject]
        IBookAnalyser BookAnalyser { get; set; }
        [Inject]
        IJSRuntime js { get; set; }

        private bool ShowToast { get; set; }
        private bool HasError { get; set; }
        private String ToastMessage { get; set; }
        public Index()
        {
      
            FileItem = new();
            WordCountResults = new ();
            FileItem.TopRows = 50;
            FileItem.MinWordLength = 1;
        }

        private async void LoadFile(InputFileChangeEventArgs e)
        {

            FileItem.FileName = $"{env.WebRootPath}\\{e.File.Name}";
            FileStream fs = File.Create(FileItem.FileName);
            await e.File.OpenReadStream(33594080).CopyToAsync(fs);
         
            fs.Close();

            ShowToast = true;
            StateHasChanged();
        }

        private async Task OnProcessBook()
        {
            WordCountResults.Clear();
            await BookAnalyser.UploadFile(FileItem.FileName);
            await BookAnalyser.ProcessBook();
            var topResults = BookAnalyser.GetTopResults(FileItem.TopRows, FileItem.MinWordLength);
            WordCountResults.AddRange(topResults);
            StateHasChanged();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                await js.InvokeAsync<object>("showToast");
                ShowToast = false;
                HasError = false;
            }

        }
    }
}
