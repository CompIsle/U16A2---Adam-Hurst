using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class CSVProcessor
{
    static void Main()
    {
        string inputFilePath = @"..\..\..\Taskdata.csv";
        string outputFilePath = @"..\..\..\TaskdataUPDATED.csv";

        string[] fileLines = File.ReadAllLines(inputFilePath);

        StringBuilder outputContent = new StringBuilder();

        foreach (string line in fileLines)
        {
            string[] columns = line.Split(',');
            string hashValue = ComputeMD5Hash(columns);
            outputContent.AppendLine($"{line},{hashValue}");
        }

        File.WriteAllText(outputFilePath, outputContent.ToString());
        Console.WriteLine("CSV file processed successfully!");
    }

    static string ComputeMD5Hash(string[] values)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            StringBuilder hashBuilder = new StringBuilder();
            string concatenatedValues = string.Join("", values);
            byte[] dataBytes = Encoding.UTF8.GetBytes(concatenatedValues);
            byte[] hashBytes = md5Hash.ComputeHash(dataBytes);

            foreach (byte b in hashBytes)
            {
                hashBuilder.Append(b.ToString("X2"));
            }

            return hashBuilder.ToString().Substring(0, 8);
        }
    }
}