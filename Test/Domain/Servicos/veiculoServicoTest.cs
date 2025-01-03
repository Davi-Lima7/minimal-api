using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Servicos
{
  [TestClass]
  public class veiculoServicoTest
  {
    private DbContexto CriarContextoDeTeste()
    {
      var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));
      var builder = new ConfigurationBuilder()
      .SetBasePath(path ?? Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();

      var configuration = builder.Build();

      return new DbContexto(configuration);
    }

    [TestMethod]
    public void TestandoRemover()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

      var veiculo = new Veiculo();

      veiculo.Id = 1;
      veiculo.Nome = "Fusca";
      veiculo.Marca = "Volkswagen";
      veiculo.Ano = 1975;

      context.Veiculos.Add(veiculo);
      context.SaveChanges();

      var veiculoServico = new VeiculoServico(context);

      veiculoServico.Apagar(veiculo);
      var veiculoRemovido = context.Veiculos.Find(veiculo.Id);
      Assert.IsNull(veiculoRemovido);
    }

    [TestMethod]
    public void TestandoAtualizar()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

      var veiculo = new Veiculo();

      veiculo.Id = 1;
      veiculo.Nome = "Fusca";
      veiculo.Marca = "Volkswagen";
      veiculo.Ano = 1975;

      context.Veiculos.Add(veiculo);
      context.SaveChanges();

      var veiculoServico = new VeiculoServico(context);

      veiculo.Nome = "Fusca 2";
      veiculoServico.Atualizar(veiculo);

      var veiculoAtualizado = context.Veiculos.Find(veiculo.Id);
      Assert.AreEqual("Fusca 2", veiculoAtualizado.Nome);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

      var veiculo = new Veiculo();

      veiculo.Id = 1;
      veiculo.Nome = "Fusca";
      veiculo.Marca = "Volkswagen";
      veiculo.Ano = 1975;

      context.Veiculos.Add(veiculo);
      context.SaveChanges();

      var veiculoServico = new VeiculoServico(context);

      var veiculoBuscado = veiculoServico.BuscaPorId(1);
      Assert.IsNotNull(veiculoBuscado);
      Assert.AreEqual(1, veiculoBuscado.Id);
      Assert.AreEqual("Fusca", veiculoBuscado.Nome);
    }

    [TestMethod]
    public void Incluir()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

      var veiculo = new Veiculo();

      veiculo.Id = 1;
      veiculo.Nome = "Fusca";
      veiculo.Marca = "Volkswagen";
      veiculo.Ano = 1975;

      var veiculoServico = new VeiculoServico(context);

      veiculoServico.Incluir(veiculo);

      var veiculoAdicionado = context.Veiculos.Find(veiculo.Id);
      Assert.IsNotNull(veiculoAdicionado);
      Assert.AreEqual("Fusca", veiculoAdicionado.Nome);
    }

    [TestMethod]
    public void Todos()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

      var veiculo = new Veiculo();
      var veiculo2 = new Veiculo();

      veiculo.Id = 1;
      veiculo.Nome = "Fusca";
      veiculo.Marca = "Volkswagen";
      veiculo.Ano = 1975;
      
      veiculo2.Id = 2;
      veiculo2.Nome = "Civic";
      veiculo2.Marca = "Honda";
      veiculo2.Ano = 2020;

      context.Veiculos.Add(veiculo);
      context.Veiculos.Add(veiculo2);
      context.SaveChanges();

      var veiculoServico = new VeiculoServico(context);

      var todosVeiculos = veiculoServico.Todos();
      Assert.AreEqual(2, todosVeiculos.Count);
      Assert.IsTrue(todosVeiculos.Any(v => v.Nome == "Fusca"));
      Assert.IsTrue(todosVeiculos.Any(v => v.Nome == "Civic"));
    }
  }
}