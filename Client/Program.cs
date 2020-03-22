using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorSignalRApp.Client.Services;
using Blazored.LocalStorage;

namespace BlazorSignalRApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<QuestionService>();
            builder.Services.AddSingleton<QuizProgressionService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<UserService>();
            await builder.Build().RunAsync();
        }
    }
}
