using Ganho_Capital.Interfaces;
using Ganho_Capital.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;

namespace Ganho_Capital.Test
{
    [TestClass]
    public class ProcessamentoTests
    {
        private AutoMocker mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMocker();

            mocker.Use(new Mock<ICalculo>());
            mocker.Use(new Mock<IConsoleIO>());
        }

        [TestMethod]
        [Description("Operações realizadas com sucesso")]
        public void ProcessamentoRealizado()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]";

            mocker.GetMock<IConsoleIO>()
                .SetupSequence(x => x.ReadLine())
                .Returns(jsonEntrada)
                .Returns("\n");

            var processamento = mocker.CreateInstance<Processamento>();

            //act
            processamento.Iniciar(null);

            //assert
            mocker.GetMock<IConsoleIO>().Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestMethod]
        [Description("Erro no arquivo de entrada")]
        public void ProcessamentoErroArquivoEntrada()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}";

            mocker.GetMock<IConsoleIO>()
                .SetupSequence(x => x.ReadLine())
                .Returns(jsonEntrada)
                .Returns("\n");

            var processamento = mocker.CreateInstance<Processamento>();

            //act
            processamento.Iniciar(null);

            //assert
            mocker.GetMock<IConsoleIO>().Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        [Description("Processando duas listas na entrada")]
        public void ProcessamentoArquivoDuasListas()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]\n" +
                "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]";

            mocker.GetMock<IConsoleIO>()
                .SetupSequence(x => x.ReadLine())
                .Returns(jsonEntrada)
                .Returns("\n");

            var processamento = mocker.CreateInstance<Processamento>();

            //act
            processamento.Iniciar(null);

            //assert
            mocker.GetMock<IConsoleIO>().Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
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

            mocker.GetMock<IConsoleIO>()
                .SetupSequence(x => x.ReadLine())
                .Returns(jsonEntrada)
                .Returns("\n");

            var processamento = mocker.CreateInstance<Processamento>();

            //act
            processamento.Iniciar(null);

            //assert
            mocker.GetMock<IConsoleIO>().Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestMethod]
        [Description("Arquivo de entrada com duas listas na mesma linha")]
        public void ProcessamentoErroArquivoEntradaDuasListas()
        {
            //arrange
            string jsonEntrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]" +
                "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}";
            
            mocker.GetMock<IConsoleIO>()
                .SetupSequence(x => x.ReadLine())
                .Returns(jsonEntrada)
                .Returns("\n");

            var processamento = mocker.CreateInstance<Processamento>();

            //act
            processamento.Iniciar(null);

            //assert
            mocker.GetMock<IConsoleIO>().Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
        }
    }
}