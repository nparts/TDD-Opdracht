using System;

public class Prorgam
{
    public static void Main(string[] args)
    {
        justAFunction(true, true, true);
    }
    private static void justAFunction(bool a, bool b, bool c)
    {
        if (a & b & c)
        {
            Console.WriteLine("Hello, World!");
        }
        else
        {
            Console.WriteLine("Goodbye, World!");
        }
    }
}