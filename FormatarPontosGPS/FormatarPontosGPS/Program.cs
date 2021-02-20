using FormatarPontosGPS.Entities;
using System;
using System.IO;

namespace FormatarPontosGPS
{
    class Program
    {
        static void Main(string[] args) {
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
            Console.Write(" **** Digite o caminho do arquivo completo: ");
            string sourceFilePath = Console.ReadLine();
            string path = Path.GetDirectoryName(sourceFilePath);
            string targetDirectoryPath = path + @"\Pontos_Formatados";
            string fileName = @"\Pontos_Formatados.txt";
            string targetFilePath = targetDirectoryPath + fileName;

            try {
                Directory.CreateDirectory(targetDirectoryPath);
                FileInfo info = new FileInfo(targetDirectoryPath + fileName);               
                if (info.Exists) {
                    Console.WriteLine();
                    Console.WriteLine("Arquivo existente! Caminho: " + targetFilePath);
                    Console.WriteLine("Deseja sobrescrever o arquivo? Digite (1) - Sim ou (2) - Não ");
                    int opc = int.Parse(Console.ReadLine());                    
                    if (opc > 2 || opc < 1) {
                        Console.WriteLine("Opção Inválida! Digite (1) - Sim ou (2) - Não");
                        opc = int.Parse(Console.ReadLine());
                    } else if (opc == 2) {
                        Console.WriteLine();
                        Console.WriteLine("Operação não realizada.");
                        Console.WriteLine("Pressione qualquer tecla para finalizar ...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }                    
                }
                string[] lines = File.ReadAllLines(sourceFilePath);
                using (StreamWriter sw = File.CreateText(targetFilePath)) {
                    foreach (string line in lines) {
                        string line1 = line.Trim('"');
                        string[] item = line1.Split(' ');
                        int ponto = int.Parse(item[0]);
                        double x = double.Parse(item[1]);
                        double y = double.Parse(item[2]);
                        double z = double.Parse(item[3]);
                        string descricao = item[4].TrimStart('"').TrimEnd('"');
                        if (item.Length > 4) {
                            for (int i = 5; i < item.Length; i++) {
                                descricao += " " + item[i].TrimStart('"').TrimEnd('"');
                            }
                        }
                        PontoGps pontoGps = new PontoGps(ponto, x, y, z, descricao);
                        sw.WriteLine(                            
                            pontoGps.Ponto + " " + "\t" +
                            pontoGps.X.ToString("F4") + " " + "\t" +
                            pontoGps.Y.ToString("F4") + " " + "\t" +
                            pontoGps.Z.ToString("F4") + " " + "\t" +
                            pontoGps.Descricao + " "
                            );                     
                    }
                    Console.WriteLine("Operação Realizada com Sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Caminho do arquivo gerado: " + targetFilePath.ToString().ToUpper());
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
