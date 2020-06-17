using OpenQA.Selenium;
using System;
using Xunit;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace XpathConsole
{
    public class XPathMain
    {
        static void Main(string[] args)
        {
            // Declara variaveis base para criação de arquivo txt
            #region param_base
            string nomeArquivo = "ParametroGerais.txt";
            string diretorioRaiz = Directory.GetCurrentDirectory();
            string pathCompleto = Path.Combine(diretorioRaiz, nomeArquivo);
            #endregion

            // objeto parametro para agrupar dados do txt
            DataParametros dados = new DataParametros();

            try
            {
                // Classe para verificar se o documento txt foi criado e adicionar os parametros gerais
                ParametrosDocumento doc = new ParametrosDocumento();
                doc.criaVerificaDoc(pathCompleto, dados);

                Console.WriteLine("Iniciar navegador");
                //Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
                ChromeOptions options = new ChromeOptions();
                // Começa navegador maximizado , remove notificações de permissão do navegador
                options.AddArguments(new string[] { "start-maximized", "--disable-notifications" });

                //Inicializador do Web driver Chrome

                IWebDriver driver = new ChromeDriver(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    options
                    );

                //Inicia no navegador a url do Facebook
                driver.Navigate().GoToUrl("https://pt-br.facebook.com/");

                IWebElement email = driver.FindElement(By.XPath("//input[@type='text' or @id='email']"));
                IWebElement pass = driver.FindElement(By.XPath("//input[@type='password' or @id='pass']"));
                IWebElement form = driver.FindElement(By.XPath("//form[@method='post']"));
                //IWebElement email = driver.FindElement(By.Id("email"));
                //IWebElement pass = driver.FindElement(By.Id("pass"));
                //IWebElement form = driver.FindElement(By.Id("login_form"));


                email.Click();
                //email.SendKeys("pedro.furlan1304@hotmail.com");
                email.SendKeys(dados.email);
                //email.SendKeys("pedro.vinicius1304@gmail.com");
                pass.Click();
                //pass.SendKeys("pedro72473541");
                pass.SendKeys(dados.senha);
                //pass.SendKeys("teste@01");
                form.Submit();

                Thread.Sleep(new TimeSpan(0, 0, 10));


                IWebElement buttonPerfil = driver.FindElement(By.XPath("//a[@title='Perfil']"));
                buttonPerfil.Click();

                Thread.Sleep(new TimeSpan(0, 0, 5));
                IWebElement buttonComment = driver.FindElement(By.XPath("//a[@label='Publicação']"));
                buttonComment.Click();
                // Campo pesquisa 
                //IWebElement pesquisa = driver.FindElement(By.ClassName("_1frb"));


                foreach (string txt in dados.texto)
                {
                    setMessage(driver, txt);                   
                }

                Console.WriteLine("Finalizei comentarios...");
                Console.WriteLine("Deslogando ...");

                driver.Navigate().Refresh();

                IWebElement botaoOpcoes = driver.FindElement(By.Id("userNavigationLabel"));
                botaoOpcoes.Click();
                Thread.Sleep(new TimeSpan(0, 0, 5));
                IWebElement botaoSair = driver.FindElement(By.XPath("//span[text()='Terminar sessão']"));
                botaoSair.Click();
                Thread.Sleep(new TimeSpan(0,0,5));
                driver.Quit();
                Thread.Sleep(new TimeSpan(0, 0, 10));
                Environment.Exit(-1); 
            }
            catch (Exception e)
            {
                throw e;
                //Environment.Exit(-1);*
            }


        }


        public static void setMessage(IWebDriver driver, string mensagem)
        {
            Thread.Sleep(new TimeSpan(0, 0, 5));
            IWebElement textAreaComment = driver.FindElement(By.XPath("//div[contains(@aria-label,'pensar')]"));
            textAreaComment.Click();
            textAreaComment.SendKeys(mensagem);
            IWebElement buttonSubmit = driver.FindElement(By.XPath("//button[span[text()='Publicar']]"));
            buttonSubmit.Submit();
        }



    }

    public class ParametrosDocumento
    {
        public void criaVerificaDoc(string pathCompleto, DataParametros dados)
        {
            if (!File.Exists(pathCompleto))
            {
                File.Create(pathCompleto).Dispose();

                Console.WriteLine("Arquivo de parametro criado !");
                Console.WriteLine("Digite o E-mail a ser incluido automático !");
                var emailConsole = Console.ReadLine();
                Console.WriteLine("Digite a Senha a ser incluido automáticamente !");
                var senhaConsole = Console.ReadLine();
                Console.WriteLine("Digite alguns textos separado por ';' !");
                var textos = Console.ReadLine();
                using (StreamWriter stream = new StreamWriter(pathCompleto))
                {
                    stream.WriteLine("----------------------------------- PARAMETROS GERAIS ----------------------------------------------");
                    stream.WriteLine(" ");
                    stream.WriteLine(String.Format("Email={0};", emailConsole));
                    stream.WriteLine(" ");
                    stream.WriteLine(String.Format("Senha={0};", senhaConsole));
                    stream.WriteLine("---------------------------------------- TEXTOS GERAIS ------------------------------------------------------------ ");
                    stream.WriteLine(textos);
                }

                Console.WriteLine("Informações cadastradas !");
                dados.email = emailConsole.Trim();
                dados.senha = senhaConsole.Trim();
                var arrayTextos = textos.Split(';');
                dados.texto = new List<string>(arrayTextos);
            }
            else
            {
                using (StreamReader reader = new StreamReader(pathCompleto))
                {
                    string linha;
                    bool readEmail = false;
                    bool readSenha = false;
                    bool AreaTexto = false;

                    while ((linha = reader.ReadLine()) != null)
                    {

                        if (linha != null)
                        {
                            if (linha.Trim().Contains("Email"))
                            {
                                var splitEmail = linha.Split('=');
                                dados.email = splitEmail[1].Replace(';', ' ');
                                readEmail = true;
                            }

                            if (linha.Trim().Contains("Senha"))
                            {
                                var splitSenha = linha.Split('=');
                                dados.senha = splitSenha[1].Replace(';', ' ');
                                readSenha = true;
                                goto prox;
                            }

                            if (linha.Trim().Contains(";") && (readEmail == true && readSenha == true) || (AreaTexto == true))
                            {
                                dados.texto = new List<string>(linha.Split(';'));
                            }

                            if (linha.Trim().Contains("TEXTOS GERAIS"))
                            {
                                AreaTexto = true;
                            }

                            prox:;
                        }
                    }
                }
            }

        }
    }


    public class DataParametros
    {
        public string email { get; set; }

        public string senha { get; set; }

        public List<string> texto { get; set; }
    }





}