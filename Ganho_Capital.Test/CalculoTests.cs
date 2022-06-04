using Ganho_Capital.Models;
using Ganho_Capital.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ganho_Capital.Test
{
    [TestClass]
    public class CalculoTests
    {
        private Calculo calculo;
        private List<Acao> lstAcao;

        [TestInitialize]
        public void Setup()
        {
            calculo = new Calculo();
            lstAcao = new List<Acao>();
        }

        [TestMethod]
        [Description("Calcular uma operação de compra")]
        public void CalcularOperacaoCompra()
        {
            //arrange
            lstAcao.Add(new Acao { Operation = "buy", Quantity = 1000, UnitCost = 10.00m });

            //act
            var taxaCalculada = calculo.CalcOperacao(lstAcao);

            //assert
            Assert.AreEqual(1, taxaCalculada.Count);
        }

        [TestMethod]
        [Description("Calcular duas operações de compra")]
        public void CalcularOperacoesCompra()
        {
            //arrange
            lstAcao.Add(new Acao { Operation = "buy", Quantity = 1000, UnitCost = 10.00m });
            lstAcao.Add(new Acao { Operation = "buy", Quantity = 2000, UnitCost = 15.00m });

            //act
            var taxaCalculada = calculo.CalcOperacao(lstAcao);

            //assert
            Assert.AreEqual(2, taxaCalculada.Count);
        }

        [TestMethod]
        [Description("Calcular operações de compra e venda")]
        public void CalcularOperacoesCompraVenda()
        {
            //arrange
            lstAcao.Add(new Acao { Operation = "buy", Quantity = 1000, UnitCost = 10.00m });
            lstAcao.Add(new Acao { Operation = "sell", Quantity = 1000, UnitCost = 15.00m });

            //act
            var taxaCalculada = calculo.CalcOperacao(lstAcao);

            //assert
            Assert.AreEqual(2, taxaCalculada.Count);
        }

        [TestMethod]
        [Description("Calcular diversas operações de compra e venda")]
        public void CalcularOperacoesDiversasCompraVenda()
        {
            //arrange
            lstAcao.Add(new Acao { Operation = "buy", UnitCost = 10.00m, Quantity = 10000 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 2.00m, Quantity = 5000 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 20.00m, Quantity = 2000 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 20.00m, Quantity = 2000 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 25.00m, Quantity = 1000 });
            lstAcao.Add(new Acao { Operation = "buy", UnitCost = 20.00m, Quantity = 10000 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 15.00m, Quantity = 5000 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 30.00m, Quantity = 4350 });
            lstAcao.Add(new Acao { Operation = "sell", UnitCost = 30.00m, Quantity = 650 });

            //act
            var taxaCalculada = calculo.CalcOperacao(lstAcao);

            //assert
            Assert.AreEqual(9, taxaCalculada.Count);
        }
    }
}