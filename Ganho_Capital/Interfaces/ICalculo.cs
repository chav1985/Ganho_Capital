using Ganho_Capital.Models;
using System.Collections.Generic;

namespace Ganho_Capital.Interfaces
{
    public interface ICalculo
    {
        List<Taxa> CalcOperacao(List<Acao> listaAcoes);
    }
}