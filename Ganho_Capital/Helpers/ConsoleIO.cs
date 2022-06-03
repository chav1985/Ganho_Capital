using Ganho_Capital.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Ganho_Capital.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ConsoleIO : IConsoleIO
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string texto)
        {
            Console.Write(texto);
        }
    }
}
