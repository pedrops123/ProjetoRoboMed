
using System;
using XPathConsole;

namespace XpathConsole
{
    public class XPathMain
    {
        static void Main(string[] args)
        {
            try
            {
                topoSistema:;
                // Classe para verificar se o documento txt foi criado e adicionar os parametros gerais
                ParametrosDocumento doc = new ParametrosDocumento();
                var dados = doc.criaVerificaDoc();

                if (dados.email.Trim() == "" || dados.senha.Trim() == "")
                {
                    Console.WriteLine("Não foi informado nenhum usuario ou senha , favor incluir no arquivo de configuração \n para que possamos prosseguir.");
                    Console.WriteLine("Aperte qualquer tecla para voltar");
                    Console.ReadKey();
                    goto topoSistema;
                }

                PrepareDriver ClasseDrivers = new PrepareDriver();
                var comandos = ClasseDrivers.PrepareDriverChrome();
                comandos.Login(dados);
                comandos.incluirComentarios(dados);
                Console.WriteLine("Finalizei comentarios...");
                Console.WriteLine("Deslogando ...");
                comandos.Logout();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}