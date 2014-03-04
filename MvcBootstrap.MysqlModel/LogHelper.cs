using System;
using System.IO;

public class Log
{
    public static void Append(string info)
    {
        File.AppendAllText("D:\\service.txt", info + Environment.NewLine);
    }
    public static void Append(string fileName, string info)
    {
        File.AppendAllText("D:\\" + fileName + ".txt", info + Environment.NewLine);
    }
}