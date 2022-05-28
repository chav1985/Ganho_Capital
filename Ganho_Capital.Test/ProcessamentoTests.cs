using Ganho_Capital.Interfaces;
using Ganho_Capital.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Ganho_Capital.Test
{
    [TestClass]
    public class ProcessamentoTests
    {
        [TestMethod]
        [Description("Operações realizadas com sucesso")]
        public void ProcessamentoRealizado()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]";
            IProcessamento processamento = new Processamento(new Calculo());

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var stringReader = new StringReader(jsonEntrada);
            Console.SetIn(stringReader);

            //act
            processamento.Iniciar(null);

            //assert
            var output = stringWriter.ToString();
            string vlrEsperado = "[{\"tax\":0.00},{\"tax\":10000.00}]\n";
            Assert.AreEqual(vlrEsperado, output);
        }

        [TestMethod]
        [Description("Erro no arquivo de entrada")]
        public void ProcessamentoErroArquivoEntrada()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}";
            IProcessamento processamento = new Processamento(new Calculo());

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var stringReader = new StringReader(jsonEntrada);
            Console.SetIn(stringReader);

            //act
            processamento.Iniciar(null);

            //assert
            var output = stringWriter.ToString();
            string vlrEsperado = "\nErro ao processar o JSON de entrada: Unexpected end when deserializing array. Path '[1]', line 1, position 116.";
            Assert.AreEqual(vlrEsperado, output);
        }

        [TestMethod]
        [Description("Processando duas listas na entrada")]
        public void ProcessamentoArquivoDuasListas()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]\n" +
                "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]";
            IProcessamento processamento = new Processamento(new Calculo());

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var stringReader = new StringReader(jsonEntrada);
            Console.SetIn(stringReader);

            //act
            processamento.Iniciar(null);

            //assert
            var output = stringWriter.ToString();
            string vlrEsperado = "[{\"tax\":0.00},{\"tax\":10000.00}]\n[{\"tax\":0.00},{\"tax\":10000.00}]\n";
            Assert.AreEqual(vlrEsperado, output);
        }

        [TestMethod]
        [Description("Processando varias operações")]
        public void ProcessamentoVariasOperacoes()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\",\"unit-cost\":10.00,\"quantity\":10000},{\"operation\":\"sell\",\"unit-cost\":2.00,\"quantity\":5000}," +
                "{\"operation\":\"sell\",\"unit-cost\":20.00,\"quantity\":2000},{\"operation\":\"sell\",\"unit-cost\":20.00,\"quantity\":2000}," +
                "{\"operation\":\"sell\",\"unit-cost\":25.00,\"quantity\":1000},{\"operation\":\"buy\",\"unit-cost\":20.00,\"quantity\":10000}," +
                "{\"operation\":\"sell\",\"unit-cost\":15.00,\"quantity\":5000},{\"operation\":\"sell\",\"unit-cost\":30.00,\"quantity\":4350}," +
                "{\"operation\":\"sell\",\"unit-cost\":30.00,\"quantity\":650}]";
            IProcessamento processamento = new Processamento(new Calculo());

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var stringReader = new StringReader(jsonEntrada);
            Console.SetIn(stringReader);

            //act
            processamento.Iniciar(null);

            //assert
            var output = stringWriter.ToString();
            string vlrEsperado = "[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3000.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3700.00},{\"tax\":0.00}]\n";
            Assert.AreEqual(vlrEsperado, output);
        }

        [TestMethod]
        [Description("Arquivo de entrada com duas listas na mesma linha")]
        public void ProcessamentoErroArquivoEntradaDuasListas()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]" +
                "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}";
            IProcessamento processamento = new Processamento(new Calculo());

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var stringReader = new StringReader(jsonEntrada);
            Console.SetIn(stringReader);

            //act
            processamento.Iniciar(null);

            //assert
            var output = stringWriter.ToString();
            string vlrEsperado = "\nErro ao processar o JSON de entrada: Additional text encountered after finished reading JSON content: [. Path '', line 1, position 117.";
            Assert.AreEqual(vlrEsperado, output);
        }
    }
}