using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAnalyserLib;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
[assembly: FunctionsStartup(typeof(ProcessBookFunction.Startup))]
namespace ProcessBookFunction
{
    public class Startup : FunctionsStartup
    {
        public Startup()
        {

        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IBookAnalyser, FileBookAnalyser>();
        }
    }
}
