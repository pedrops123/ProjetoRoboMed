
using System;
using System.Collections.Generic;
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
                if (dados.texto == null)
                {
                    dados.texto = new List<string>();
                }


                if (dados.email.Trim() == "" || dados.senha.Trim() == "")
                {
                    Console.WriteLine("Não foi informado nenhum usuario ou senha , favor incluir no arquivo de configuração \n para que possamos prosseguir.");
                    Console.WriteLine("Aperte qualquer tecla para voltar");
                    Console.ReadKey();
                    goto topoSistema;
                }

                PrepareDriver ClasseDrivers = new PrepareDriver();
                var comandos = ClasseDrivers.PrepareDriverChrome();
               

                //if (dados.texto.Count == 0)
                //{
                //    Random rd = new Random();
                //    int qtd = rd.Next(1,10);
                //    comandos.getTextoLeroLero(dados,qtd);
                //}

                //Random numeroAi = new Random();
                //int qtdValida = numeroAi.Next(0,1000);
                //var validaMod = qtdValida % 2;
                //if(validaMod == 0)
                //{
                //    comandos.getFoto();
                //}

                comandos.Login(dados);
                //comandos.verificaSolicitacaoAmizade();
                //comandos.verificaMensagemPrivada();
                //comandos.incluirComentarios(dados);
                //comandos.spamGeral();
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