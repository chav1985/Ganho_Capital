using Ganho_Capital.Interfaces;
using Ganho_Capital.Models;
using System;
using System.Collections.Generic;

namespace Ganho_Capital.Services
{
    public class Calculo : ICalculo
    {
        private List<Taxa> lstTaxas;
        private int qtdAcoesCompra;
        private decimal mediaPond;
        private decimal resultadoOperacao;
        private int qtdCompras;
        private decimal resultadoFinal;

        public List<Taxa> CalcOperacao(List<Acao> listaAcoes)
        {

            lstTaxas = new List<Taxa>();
            qtdAcoesCompra = 0;
            mediaPond = 0;
            resultadoOperacao = 0;
            qtdCompras = 0;
            resultadoFinal = 0;

            foreach (var acao in listaAcoes)
            {
                if (acao.Operation == "buy")
                {
                    OperacaoCompra(acao);
                }
                else if (acao.Operation == "sell")
                {
                    OperacaoVenda(acao);
                }
            }

            return lstTaxas;
        }



        private void OperacaoCompra(Acao acao)
        {
            if (qtdCompras == 0)
            {
                qtdAcoesCompra = acao.Quantity;
                acao.Imposto = DecimalRound(0.00m);
                lstTaxas.Add(new Taxa { TaxaOperacao = acao.Imposto });
                mediaPond = DecimalRound(acao.UnitCost);
                qtdCompras += 1;
            }
            else
            {
                acao.Imposto = DecimalRound(0.00m);
                lstTaxas.Add(new Taxa { TaxaOperacao = acao.Imposto });
                mediaPond = DecimalRound(((qtdAcoesCompra * mediaPond) + (acao.Quantity * acao.UnitCost)) / (qtdAcoesCompra + acao.Quantity));

                if (qtdAcoesCompra == 0)
                {
                    resultadoFinal = 0;
                }

                qtdAcoesCompra += acao.Quantity;
            }
        }

        private void OperacaoVenda(Acao acao)
        {
            decimal valorOperacao = acao.UnitCost * acao.Quantity;
            resultadoOperacao = valorOperacao - (mediaPond * acao.Quantity);

            qtdAcoesCompra -= acao.Quantity;

            if (valorOperacao >= 20000)
            {
                if (resultadoOperacao < 0)
                {
                    acao.Imposto = DecimalRound(0.00m);
                    lstTaxas.Add(new Taxa { TaxaOperacao = acao.Imposto });
                    resultadoFinal += resultadoOperacao;
                }
                else
                {
                    resultadoFinal += resultadoOperacao;

                    if (resultadoFinal > 0)
                    {
                        acao.Imposto = DecimalRound(resultadoFinal * 20 / 100);
                        lstTaxas.Add(new Taxa { TaxaOperacao = acao.Imposto });
                    }
                    else
                    {
                        acao.Imposto = DecimalRound(0.00m);
                        lstTaxas.Add(new Taxa { TaxaOperacao = acao.Imposto });
                    }
                }
            }
            else
            {
                acao.Imposto = DecimalRound(0.00m);
                lstTaxas.Add(new Taxa { TaxaOperacao = acao.Imposto });
                resultadoFinal += resultadoOperacao;
            }
        }

        private static decimal DecimalRound(decimal value)
        {
            return decimal.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}