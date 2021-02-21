using FormatarPontosGPS.Entities;
using System;
using System.IO;

namespace FormatarPontosGPS
{
    class Program
    {
        static void Main(string[] args) {
            Console.Title = "*** FormatarPontosGPS *** - Desenvolvido por Renata Rodrigues";
            Console.WriteLine("******************** Formatação de Arquivos de Pontos GPS ********************");
            Console.WriteLine();
            Console.WriteLine("Para que o processamento ocorra corretamente, o arquivo de origem ");
            Console.WriteLine("deve conter a seguinte formatação para cada linha: ");
            Console.WriteLine();
            Console.WriteLine("Exemplo: ");
            Console.WriteLine(@"""72 7442110,7005 328560,1692 886,6528 """"AL DIVISA""""""");
            Console.WriteLine();
            Console.WriteLine("Após a formatação, o novo arquivo apresentará cada linha no seguinte formato: ");
            Console.WriteLine("72 	7442110,7005 	328560,1692 	886,6528 	AL DIVISA");
            Console.WriteLine();            
            string sourceFilePath;
            bool result;
            do {
                Console.Write(" **** Digite o caminho do arquivo completo: ");
                sourceFilePath = Console.ReadLine();
                result = String.IsNullOrEmpty(sourceFilePath) ? true : false;
                if (result) {
                    Console.WriteLine("Caminho do arquivo em branco!");
                }
            } while (result);
           
            string path = Path.GetDirectoryName(sourceFilePath);
            string targetDirectoryPath = path + @"\Pontos_Formatados";
            string fileName = @"\Pontos_Formatados.txt";
            string targetFilePath = targetDirectoryPath + fileName;
            int opc;

            try {
                Directory.CreateDirectory(targetDirectoryPath);
                FileInfo info = new FileInfo(targetDirectoryPath + fileName);
                if (info.Exists) {
                    Console.WriteLine();
                    Console.WriteLine("Arquivo existente! Caminho: " + targetFilePath);
                    Console.WriteLine("Deseja sobrescrever o arquivo? Digite (1) - Sim ou (2) - Não ");
                    opc = int.Parse(Console.ReadLine());
                    Services.ReadChoice(opc);
                }
                Services.ReadAndFormatFile(sourceFilePath, targetFilePath);               
                opc = 0;
                Console.WriteLine("**** Deseja somar a ondulação geoidal ao arquivo gerado?");
                Console.WriteLine("Digite (1) - Sim ou (2) - Não ");
                opc = int.Parse(Console.ReadLine());
                Services.ReadChoice(opc);
                string sourceFilePath2;
                do {
                    Console.WriteLine("Digite o caminho do arquivo completo, contendo os dados da ondulação geoidal: ");
                    Console.WriteLine();
                    Console.WriteLine("** IMPORTANTE: Os arquivos devem ter a mesma quantidade de linhas.");
                    sourceFilePath2 = Console.ReadLine();
                    result = String.IsNullOrEmpty(sourceFilePath2) ? true : false;
                    if (result) {
                        Console.WriteLine("Caminho do arquivo em branco!");
                    }
                } while (result);
                fileName = @"\Pontos_somados_ondulacao_geoidal.txt";
                targetFilePath = targetDirectoryPath + fileName;
                
                if (String.IsNullOrEmpty(sourceFilePath) ||
                    String.IsNullOrEmpty(sourceFilePath2) ||
                    String.IsNullOrEmpty(targetFilePath)) {
                    Console.WriteLine("Operação encerrada! Caminho dos arquivos não identificados");
                    Console.WriteLine("Pressione qualquer tecla para finalizar ...");
                    Console.ReadKey();
                    Environment.Exit(0);
                } else {
                    Services.ReadAndAddFile(sourceFilePath, sourceFilePath2, targetFilePath);
                    Console.WriteLine("Pressione qualquer tecla para finalizar ...");
                    Console.ReadKey();
                }
               
            }
            catch (IOException e) {
                Console.WriteLine("Ocorreu o seguinte erro: ");
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para finalizar ...");
                Console.ReadKey();
            }
        }
    }
}


