using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAnalyser.Pages
{
    public partial class Toast
    {
        [Parameter]
        public bool IsError { get; set; }
        [Parameter]
        public String ToastMessage { get; set; }
        [Parameter]
        public String Title { get; set; }


        public Toast()
        {
            IsError = false;
        }


    }
}
