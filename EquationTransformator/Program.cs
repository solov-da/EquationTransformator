using EquationTransformator.Core.Handlers;
using EquationTransformator.Implementation.Handlers;

namespace EquationTransformator;

internal class Program
{
    static void Main(string[] args)
    {
        var handler = new DefaultCanonicalEquationHandler();
        
        if (args.Length == 2)
        {
            if (args[0] != "-file") 
                return;
            
            var fileName = args[1];
            
            if (string.IsNullOrWhiteSpace(fileName))
                return;
            
            var directoryName = Path.GetDirectoryName(args[1]);
            
            if (string.IsNullOrWhiteSpace(directoryName))
                return;

            var outFilePath = Path.Combine(directoryName, $"{Path.GetFileNameWithoutExtension(fileName)}.out");

            using var sr = File.OpenText(fileName);
            using var sw = File.CreateText(outFilePath);
            Console.SetIn(sr);
            Console.SetOut(sw);
            Processing(handler, true);
        }
        else
        {
            Processing(handler, false);
        }
    }
       
    private static void Processing(ICanonicalEquationHandler handler, bool fileMode)
    {
        string? equation;

        Console.WriteLine("Enter equation:");

        while ((equation = Console.ReadLine()) is not null)
        {
            try
            {
                if (fileMode)
                    Console.WriteLine(equation);

                Console.WriteLine();
                var canonicalEquation = handler.Processing(equation);
                Console.WriteLine("Canonical equation:");
                Console.WriteLine(canonicalEquation.ToString());
                Console.WriteLine();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Enter equation:");
        }
    }
}