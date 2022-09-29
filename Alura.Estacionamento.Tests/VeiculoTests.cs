using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Alura.Estacionamento.Tests
{
    public class VeiculoTests : IDisposable
    {
        private Veiculo _veiculo;
        public ITestOutputHelper _testOutputHelper;

        public VeiculoTests(ITestOutputHelper testOutputHelper)
        {
            _veiculo = new();
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("Setup executed.");
        }

        [Fact(DisplayName ="Acelerar")]
        [Trait("UnitTest", "Acelerar")]
        public void Veiculo_Acelerar_VelocidadeAtualMustBe100()
        {
            //Arrange
            //Veiculo veiculo = new();
            
            //Act
            _veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, _veiculo.VelocidadeAtual);
        }
        [Fact(DisplayName ="Frear")]
        [Trait("UnitTest","Frear")]
        public void Veiculo_Frear_VelocidadeAtualMustBe150Negative()
        {
            //Arrange
            //Veiculo veiculo = new();

            //Act
            _veiculo.Frear(10);
            
            //Assert
            Assert.Equal(-150, _veiculo.VelocidadeAtual);
        }
        [Fact(DisplayName ="Definição de tipo de motocicleta")]
        [Trait("UnitTest","Definição de tipo de motocicleta")]
        public void Veiculo_DefineTipoVeiculo_TipoMustBeMotocicleta()
        {
            //Arrange
            //Veiculo veiculo = new();

            //Act
            _veiculo.Tipo = TipoVeiculo.Motocicleta;

            //Assert
            Assert.Equal(TipoVeiculo.Motocicleta, _veiculo.Tipo);
        }

        [Fact(DisplayName ="Definição de tipo de veículo DEFAULT")]
        [Trait("UnitTest", "Definição de tipo de DEFAULT")]
        public void Veiculo_DefineTipoVeiculo_TipoMustBeAutomovel()
        {
            //Arrange
            //Veiculo veiculo = new();

            //Act
            _veiculo.Tipo = 0;

            //Assert
            Assert.Equal(TipoVeiculo.Automovel, _veiculo.Tipo);
        }

        [Theory(DisplayName = "Testa aceleração")]
        [ClassData(typeof(Veiculo))]
        public void Veiculo_TestaAcelerar_VelocidadeDeveSerDez(Veiculo modelo)
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            _veiculo.Acelerar(10);
            modelo.Acelerar(10);

            //Assert
            Assert.Equal(modelo.VelocidadeAtual, _veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Busca Dados Automóvel")]
        public void Veiculo_BuscaDadosAutomovel_ResultadoDeveConterAutomovel()
        {
            //Arrange
            //var carro = new Veiculo();
            _veiculo.Proprietario = "Carlos Silva";
            _veiculo.Placa = "ZAP-7419";
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Variante";

            //Act
            string dados = _veiculo.ToString();

            //Assert
            Assert.Contains("Tipo do Veículo: Automovel", dados);
        }

        [Fact(DisplayName ="Exceção nome com menos de 3 caracteres")]
        public void Veiculo_NomeProprietarioComMenosDeTresCaracteres_DeveResultarEmExcecao()
        {
            //Arrange
            string nomeProprietario = "Ab";

            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
                );
        }

        [Fact(DisplayName ="Exceção de quarto caracter não \"-\"")]
        public void Veiculo_VerificaExcecaoQuartoCaracterDaPlaca_DeveResultarEmExcecao()
        {
            //Arrange
            string placa = "ABCD1234";

            //Act
            var mensagem = Assert.Throws<System.FormatException>(
                () => new Veiculo().Placa = placa
                );

            //Assert
            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Cleanup evoked");
        }
    }
}