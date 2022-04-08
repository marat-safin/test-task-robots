using System;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        Application app = new Application();
        string[] output = app.Execute(args);
        foreach (string line in output)
        {
            Console.WriteLine(line);
        }
    }
}