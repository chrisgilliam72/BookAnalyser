using System;
using System.Collections.Generic;
using System.Text;
using BookAnalyserLib;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AnalyseBookFunction.Startup))]

namespace AnalyseBookFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IBookAnalyser, FileBookAnalyser>();
        }
    }
}
