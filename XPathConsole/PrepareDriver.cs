using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XPathConsole
{
   public class PrepareDriver
    {
        public CommandsBot PrepareDriverChrome()
        {
            ChromeOptions options = new ChromeOptions();
            // Começa navegador maximizado , remove notificações de permissão do navegador
            options.AddArguments(new string[] { "start-maximized", "--disable-notifications" });

            //Inicializador do Web driver Chrome
            IWebDriver  driver = new ChromeDriver(
               Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
               options
               );
            CommandsBot comandosRobo = new CommandsBot(driver);
            return comandosRobo;
        }
    }
}
