using EquationTransformator.Core.Handlers;
using EquationTransformator.Implementation.Handlers;

namespace EquationTransformator;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Processing(new DefaultCanonicalEquationHandler(), false);
        }
        else
        {
            if (args[0] != "-file") return;
             
            using (var sr = File.OpenText(args[1]))
            using (var sw = File.CreateText(Path.Combine(Path.GetDirectoryName(args[1]), 
                       $"{Path.GetFileNameWithoutExtension(args[1])}.out")))
            {
                Console.SetIn(sr);
                Console.SetOut(sw);
                Processing(new DefaultCanonicalEquationHandler(), true);
            }
        }
    }
       
    private static void Processing(ICanonicalEquationHandler handler, bool fileMode)
    {
        string equation;

        Console.WriteLine("Enter equation:");

        while ((equation = Console.ReadLine()) != null)
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