using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class PatioTests : IDisposable
    {
        private Veiculo _veiculo;
        public ITestOutputHelper _testOutputHelper;
        public PatioTests(ITestOutputHelper testOutputHelper)
        {
            _veiculo = new();
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("Setup executed.");
        }

        [Fact(DisplayName = "Faturamento")]
        [Trait("UnitTest", "Faturamento")]
        public void Patio_ValidaFaturamento_FaturamentoMustBe2()
        {
            //Arrrange
            Patio estacionamento = new();
            //Veiculo veiculo = new();

            _veiculo.Proprietario = "Robson";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Azul";
            _veiculo.Modelo = "Fusca";
            _veiculo.Placa = "abc-1234";

            estacionamento.OperadorPatio = new Operador { Nome = "Operador" };

            estacionamento.RegistrarEntradaVeiculo(_veiculo);
            estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);


            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }


        [Theory(DisplayName = "Faturamento múltiplos veículos")]
        [Trait("UnitTest", "Faturamento")]
        [InlineData("João", TipoVeiculo.Automovel, "Verde", "Chevette", "abc-5234", 2)]
        [InlineData("Robson", TipoVeiculo.Automovel, "Azul", "Fusca", "abc-1234", 2)]
        public void Patio_ValidaFaturamentoMultiplo_FaturamentoMustBeResultado(string proprietario,
            TipoVeiculo tipoVeiculo,
            string cor,
            string modelo,
            string placa,
            double resultado)
        {
            //Arrange
            Patio estacionamento = new();
            //Veiculo veiculo = new();

            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = tipoVeiculo;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            estacionamento.OperadorPatio = new Operador { Nome = "Operador" };

            estacionamento.RegistrarEntradaVeiculo(_veiculo);
            estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);


            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(resultado, faturamento);
        }

        [Theory(DisplayName = "Consultar por placa")]
        [Trait("UnitTest", "Consulta por placa")]
        [InlineData("Robson", TipoVeiculo.Automovel, "Azul", "Fusca", "abc-1234")]
        public void Patio_LocalizaVeiculo_ResultadoDeveSerPlaca(string proprietario, TipoVeiculo tipoVeiculo, string cor, string modelo, string placa)
        {
            //Arrange
            Patio estacionamento = new();
            //Veiculo veiculo = new();

            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = tipoVeiculo;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            estacionamento.OperadorPatio = new Operador { Nome = "Operador" };

            estacionamento.RegistrarEntradaVeiculo(_veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculoPorPlaca(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Fact(DisplayName = "Alteração de Dados Veículo")]
        public void AlterarDadosVeiculo()
        {
            //Arrange
            Patio estacionamento = new();
            //Veiculo veiculo = new();

            _veiculo.Proprietario = "Proprietario Alterado";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Corsa";
            _veiculo.Placa = "EVD-7888";

            estacionamento.OperadorPatio = new Operador { Nome = "Operador" };
            estacionamento.RegistrarEntradaVeiculo(_veiculo);

            Veiculo veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = _veiculo.Proprietario;
            veiculoAlterado.Placa = _veiculo.Placa;
            veiculoAlterado.Modelo = _veiculo.Modelo;
            veiculoAlterado.Cor = "Cinza";//propriedade que estava com erro

            //Act
            Veiculo consultadoAlterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(veiculoAlterado.Cor, consultadoAlterado.Cor);
        }

        [Theory(DisplayName ="Busca por Id Ticket")]
        [InlineData("Robson", TipoVeiculo.Automovel, "Azul", "Fusca", "abc-1234")]
        public void Patio_LocalizaPorIdTicket_ResultadoDeveSerVerdadeiro(string proprietario, TipoVeiculo tipoVeiculo, string cor, string modelo, string placa)
        {
            //Arrange
            Patio estacionamento = new();
            //Veiculo veiculo = new();

            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = tipoVeiculo;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            estacionamento.OperadorPatio = new Operador { Nome = "Operador" };

            estacionamento.RegistrarEntradaVeiculo(_veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculoPorTicket("00000");

            //Assert
            Assert.Equal(_veiculo.Placa, consultado.Placa);
        }


        public void Dispose()
        {
            _testOutputHelper.WriteLine("Cleanup evoked");
        }
    }
}