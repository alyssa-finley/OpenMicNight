using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenMicNight.Logic;
using OpenMicNight.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using OpenMicNight.UI;
internal class Program
{
    private static void Main(string[] args)
    {
        var services = CreateServiceCollection();

        var signUpLogic = services.GetService<ISignUpLogic>();

        UI.WelcomeBanner();

        UI.MainMenu(signUpLogic);
    }

    static IServiceProvider CreateServiceCollection()
    {
        return new ServiceCollection()
            .AddTransient<ISignUpLogic, SignUpLogic>()
            .AddTransient<IPerformerRepository, PerformerRepository>()
            .BuildServiceProvider();
    }

}