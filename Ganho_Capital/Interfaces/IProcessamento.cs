using Ganho_Capital.Models;
using System.Collections.Generic;

namespace Ganho_Capital.Interfaces
{
    public interface IProcessamento
    {
        void Iniciar(string[] args);
        void CalcularOperacoes(List<List<Acao>> lstOperacoes);
        void ImprimirTaxas(List<List<Taxa>> lstTaxas);
    }
}