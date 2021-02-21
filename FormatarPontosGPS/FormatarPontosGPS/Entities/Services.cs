using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FormatarPontosGPS.Entities
{
    public static class Services
    {
        /// <summary>
        /// Método que faz a leitura e formatação do arquivo informado
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        public static void ReadAndFormatFile(string sourceFilePath, string targetFilePath) {
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
                Console.Clear();
                Console.WriteLine("Operação Realizada com Sucesso!");
                Console.WriteLine("Caminho do arquivo gerado: " + targetFilePath.ToString().ToUpper());
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Método que faz a leitura do arquivo gerado anteriormente e soma
        /// a altura geoidal contida em outro arquivo
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceFilePath2"></param>
        /// <param name="targetFilePath"></param>
        public static void ReadAndAddFile(string sourceFilePath, string sourceFilePath2, string targetFilePath) {
            string[] lines1 = File.ReadAllLines(sourceFilePath);
            string[] lines2 = File.ReadAllLines(sourceFilePath2);

            if (lines1.Length == lines2.Length) {
                using (StreamWriter sw = File.CreateText(targetFilePath)) {
                    int count = 0;
                    foreach (string line in lines1) {                        
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
                        double ondulacao = double.Parse(lines2[count].Replace(".", ","));
                        z += ondulacao;        
                        PontoGps pontoGps = new PontoGps(ponto, x, y, z, descricao);
                        sw.WriteLine(
                            pontoGps.Ponto + " " + "\t" +
                            pontoGps.X.ToString("F4") + " " + "\t" +
                            pontoGps.Y.ToString("F4") + " " + "\t" +
                            pontoGps.Z.ToString("F4") + " " + "\t" +
                            pontoGps.Descricao + " "
                        );
                        count++;
                    }
                    Console.Clear();
                    Console.WriteLine("Operação Realizada com Sucesso!");
                    Console.WriteLine("Caminho do arquivo gerado: " + targetFilePath.ToString().ToUpper());
                    Console.WriteLine();
                }
            } else {
                Console.WriteLine();
                Console.WriteLine("Operação não realizada!");
                Console.WriteLine("Erro: Arquivo com quantidade de linhas diferentes.");
            }
        }

        /// <summary>
        /// Método que verifica a opção informada
        /// </summary>
        /// <param name="opc"></param>
        public static void ReadChoice(int opc) {
            while (opc > 2 || opc < 1) {
                Console.WriteLine();
                Console.WriteLine("Opção Inválida! Digite (1) - Sim ou (2) - Não");
                opc = int.Parse(Console.ReadLine());
                if (opc == 2) {
                    Console.WriteLine();
                    Console.WriteLine("Operação não realizada.");
                    Console.WriteLine("Pressione qualquer tecla para finalizar ...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
        }      
    }
}
