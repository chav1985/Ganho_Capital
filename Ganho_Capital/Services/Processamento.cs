using Ganho_Capital.Interfaces;
using Ganho_Capital.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ganho_Capital.Services
{
    public class Processamento : IProcessamento
    {
        private List<List<Acao>> lstOperacoes;
        private List<string> lstJsonEntrada;
        private List<List<Taxa>> lstTaxas;
        private List<string> lstTaxasCalc;

        private readonly ICalculo _calculo;

        public Processamento(ICalculo calculo)
        {
            _calculo = calculo;
        }

        public void Iniciar(string[] args)
        {
            lstOperacoes = new List<List<Acao>>();
            lstJsonEntrada = new List<string>();

            string line;

            try
            {
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    lstJsonEntrada.Add(line);
                }

                foreach (var itemEntrada in lstJsonEntrada)
                {
                    lstOperacoes.Add(JsonConvert.DeserializeObject<List<Acao>>(itemEntrada));
                }

                CalcularOperacoes(lstOperacoes);

                ImprimirTaxas(lstTaxas);
            }
            catch (Exception ex)
            {
                Console.Write($"\nErro ao processar o JSON de entrada: {ex.Message}");
            }
        }

        public void CalcularOperacoes(List<List<Acao>> lstOperacoes)
        {
            lstTaxas = new List<List<Taxa>>();
            foreach (var itemOperacao in lstOperacoes)
            {
                lstTaxas.Add(_calculo.CalcOperacao(itemOperacao));
            }
        }

        public void ImprimirTaxas(List<List<Taxa>> lstTaxas)
        {
            lstTaxasCalc = new List<string>();
            foreach (var item in lstTaxas)
            {
                lstTaxasCalc.Add(JsonConvert.SerializeObject(item));
            }

            foreach (var item in lstTaxasCalc)
            {
                Console.Write($"{item}\n");
            }
        }
    }
}