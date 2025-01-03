using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominio.Entidades;

namespace Test.Domain.Entidades
{
  [TestClass]
  public class VeiculoTest
  {
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
      var veiculo = new Veiculo();

      veiculo.Id = 1;
      veiculo.Nome = "Onix";
      veiculo.Marca = "Chevrolet";
      veiculo.Ano = 2010;

      Assert.AreEqual(1, veiculo.Id);
      Assert.AreEqual("Onix", veiculo.Nome);
      Assert.AreEqual("Chevrolet", veiculo.Marca);
      Assert.AreEqual(2010, veiculo.Ano);
    }
  }
}