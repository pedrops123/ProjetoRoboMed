using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XPathConsole
{
    public class CommandsBot
    {
        IWebDriver driver;
        public CommandsBot(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Login(DataParametros dados)
        {
            driver.Navigate().GoToUrl("https://pt-br.facebook.com/");
            var hasTitle = false;
            do
            {
                IWebElement email = driver.FindElement(By.XPath("//input[@type='text' or @id='email']"));
                IWebElement pass = driver.FindElement(By.XPath("//input[@type='password' or @id='pass']"));
                IWebElement form = driver.FindElement(By.XPath("//form[@method='post']"));
                email.Click();
                email.Clear();
                email.SendKeys(dados.email);
                pass.Click();
                pass.SendKeys(dados.senha);
                form.Submit();
                Thread.Sleep(new TimeSpan(0, 0, 10));
                hasTitle = driver.Title.Contains("Entrar");
            }
            while (hasTitle == true);



        }

        public void incluirComentarios(DataParametros dados)
        {
            IWebElement buttonPerfil = driver.FindElement(By.XPath("//a[@title='Perfil']"));
            buttonPerfil.Click();
            Thread.Sleep(new TimeSpan(0, 0, 10));
            IWebElement bodyElement = driver.FindElement(By.TagName("body"));

            //for (int i = 1; i <= 10; i++)
            //{
            //    bodyElement.SendKeys(Keys.ArrowDown);
            //}

            Thread.Sleep(new TimeSpan(0, 0, 5));
            IWebElement buttonComment = driver.FindElement(By.XPath("//a[@label='Publicação']"));
            buttonComment.Click();

            foreach (string txt in dados.texto)
            {
                setMessage(txt);
            }
        }

        public void Logout()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(new TimeSpan(0, 0, 10));
            IWebElement botaoOpcoes = driver.FindElement(By.Id("userNavigationLabel"));
            botaoOpcoes.Click();
            Thread.Sleep(new TimeSpan(0, 0, 5));
            IWebElement botaoSair = driver.FindElement(By.XPath("//span[text()='Terminar sessão']"));
            botaoSair.Click();
            Thread.Sleep(new TimeSpan(0, 0, 5));
            driver.Quit();
            Thread.Sleep(new TimeSpan(0, 0, 10));
            Environment.Exit(-1);
        }

        public void getTextoLeroLero(DataParametros dados, int qtd)
        {
            //criarAba();
            driver.Navigate().GoToUrl("https://lerolero.com/");
            IWebElement botaoGerar = driver.FindElement(By.Id("gerar-frase"));
            IWebElement textoAleartorio = driver.FindElement(By.XPath("//div[@class='sentence sentence-exited']"));
            for (int qt = 1; qt <= qtd; qt++)
            {
                botaoGerar.Click();
                string texto = textoAleartorio.GetAttribute("innerHTML");
                dados.texto.Add(texto);
                Thread.Sleep(new TimeSpan(0, 0, 3));
            }

        }

        public void verificaSolicitacaoAmizade()
        {
            try
            {
                //IWebElement notificationAmizade = driver.FindElement(By.XPath("//div/a[@name='requests']/div/span[@class='jewelCount']"));
                IWebElement ButtonNotificacaoAmizade = driver.FindElement(By.XPath("//div/a[@name='requests']"));
                ButtonNotificacaoAmizade.Click();
                Thread.Sleep(new TimeSpan(0, 0, 6));
                List<IWebElement> buttonConfirmar = driver.FindElements(By.XPath("//button[contains(text(),'Confirmar')]")).ToList();
                foreach (IWebElement botao in buttonConfirmar)
                {
                    botao.Click();
                }

            }
            catch (Exception e)
            {

            }


        }

        public void verificaMensagemPrivada()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(new TimeSpan(0, 0, 5));
            IWebElement numeroMensagens = driver.FindElement(By.XPath("//span[@id='mercurymessagesCountValue']"));
            var numeroCount = numeroMensagens.GetAttribute("innerHTML");
            IWebElement botaoMsg = driver.FindElement(By.XPath("//a[@name='mercurymessages']"));
            botaoMsg.Click();
            List<IWebElement> boxMensagens = driver.FindElements(By.XPath("//li[@class='jewelItemNew']")).ToList();
            foreach (IWebElement msg in boxMensagens)
            {
                msg.Click();
                Thread.Sleep(new TimeSpan(0, 0, 8));
                IWebElement CaixaTexto = driver.FindElement(By.XPath("//div[contains(text(),'mensagem')]"));
                CaixaTexto.Click();
                CaixaTexto.SendKeys("Ola sou um bot !");
                CaixaTexto.SendKeys(Keys.Enter);
                Thread.Sleep(new TimeSpan(0, 0, 8));
            }


        }

        public void spamGeral()
        {
            driver.Navigate().GoToUrl("url da pagina de spam");
            //List<IWebElement> ListaCampo =  driver.FindElements(By.XPath("//form[@class='commentable_item']/div/div/div[contains(@class,'clearfix')]/div/div/div/div/div/form/div/div/div")).ToList();
            top:;
            List<IWebElement> listaPublicacoes = driver.FindElements(By.XPath("//div[contains(@class,'userContentWrapper')]")).ToList();
            foreach (IWebElement publicacao in listaPublicacoes)
            {
                //publicacao.
                // Verifica se ja possui comentario
                List<IWebElement> testeVerificaComentariosRobo = publicacao.FindElements(By.XPath("//a[contains(text(),'Robo')]")).ToList();
                if (testeVerificaComentariosRobo.Count() == 0)
                {
                    // Procura o campo para clicar 
                    IWebElement CampoEscrita = publicacao.FindElement(By.XPath("//div[contains(text(),'Escreve')]"));
                    Thread.Sleep(new TimeSpan(0, 0, 6));
                    //CampoEscrita.Click();
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(CampoEscrita);
                    actions.Perform();
                    CampoEscrita.Click();
                    Thread.Sleep(new TimeSpan(0, 0, 6));
                    var campoText = publicacao.FindElement(By.XPath("//div[@role='textbox']"));
                    for (int i = 0; i <= 1; i++)
                    {
                        campoText.Click();
                        //campo.Click();
                        Thread.Sleep(new TimeSpan(0, 0, 5));
                        campoText.SendKeys("texto spam");
                        campoText.SendKeys(Keys.Enter);
                        Thread.Sleep(new TimeSpan(0, 0, 3));
                    }
                }
            }

        }


        public void getFoto()
        {
            // Em desenvolvimento
        }


        #region comandos_genericos

        public void setMessage(string mensagem)
        {
            Thread.Sleep(new TimeSpan(0, 0, 5));
            IWebElement textAreaComment = driver.FindElement(By.XPath("//div[contains(@aria-label,'pensar')]"));
            textAreaComment.Click();
            textAreaComment.SendKeys(mensagem);
            IWebElement buttonSubmit = driver.FindElement(By.XPath("//button[span[text()='Publicar']]"));
            buttonSubmit.Submit();
            Thread.Sleep(new TimeSpan(0, 0, 6));
        }

        public void criarAba()
        {
            var corpoDocumento = driver.FindElement(By.TagName("body"));
            corpoDocumento.SendKeys(Keys.Control + "t");
        }
        #endregion
    }
}
