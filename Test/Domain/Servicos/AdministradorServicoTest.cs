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
  public class AdministradorServicoTest
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
    public void TestandoSalvarAdministrador()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

      var adm = new Administrador();

      adm.Email = "teste@teste.com";
      adm.Senha = "teste";
      adm.Perfil = "Adm";

      var administradorServico = new AdministradorServico(context);

      administradorServico.Incluir(adm);

      Assert.AreEqual(1, administradorServico.Todos(1).Count());
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

      var adm = new Administrador();

      adm.Email = "teste@teste.com";
      adm.Senha = "teste";
      adm.Perfil = "Adm";

      var administradorServico = new AdministradorServico(context);

      administradorServico.Incluir(adm);
      var admDobanco = administradorServico.BuscarPorId(adm.Id);

      Assert.AreEqual(1, admDobanco?.Id);
    }

    [TestMethod]
    public void TestandoLogin()
    {
      var context = CriarContextoDeTeste();
      context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

      var adm = new Administrador();

      adm.Email = "teste@teste.com";
      adm.Senha = "teste";
      adm.Perfil = "Adm";

      context.administradores.Add(adm);
      context.SaveChanges();

      var admLogin = new LoginDTO();

      admLogin.Email = "teste@teste.com";
      admLogin.Senha = "teste";

      var administradorServico = new AdministradorServico(context);

      var admLogando = administradorServico.Login(admLogin);

      Assert.AreEqual(adm.Email, admLogando?.Email);
      Assert.AreEqual(adm.Senha, admLogando?.Senha);
    }
  }
}