using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPathConsole
{
    public class ParametrosDocumento
    {
        private string pathCompleto;

        public ParametrosDocumento()
        {
            string nomeArquivo = "ParametroGerais.txt";
            string diretorioRaiz = Directory.GetCurrentDirectory();
            pathCompleto = Path.Combine(diretorioRaiz, nomeArquivo);
        }

        public DataParametros criaVerificaDoc()
        {
            DataParametros dados = new DataParametros();
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
                Console.WriteLine("Arquivo já existe ! \n \n Verificando dados...");
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

            return dados;
        }
    }



    public class DataParametros
    {
        public string email { get; set; }

        public string senha { get; set; }

        public List<string> texto { get; set; }
    }
}
