using Ganho_Capital.Interfaces;
using Ganho_Capital.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Ganho_Capital
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvide = new ServiceCollection()
                .AddSingleton<IProcessamento, Processamento>()
                .AddTransient<ICalculo, Calculo>()
                .BuildServiceProvider();

            var processamento = serviceProvide.GetService<IProcessamento>();
            processamento.Iniciar(args);
        }
    }
}