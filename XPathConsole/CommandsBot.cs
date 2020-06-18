using OpenQA.Selenium;
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
                Thread.Sleep(new TimeSpan(0,0,10));
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

            for (int i = 1; i <= 10; i++)
            {
                bodyElement.SendKeys(Keys.ArrowDown);
            }

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

    }
}
