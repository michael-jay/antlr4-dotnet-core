using System;
using Antlr4.Runtime;
using Calculator.Parsing;

namespace Calculator
{
    class Program
    {
        private string GetInput()
        {
            Console.Write("Enter a value to evaluate: ");
            return Console.ReadLine();
        }

        private int EvaluateInput(string input)
        {
            CalculatorLexer lexer = new CalculatorLexer(new AntlrInputStream(input));

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            CalculatorParser parser = new CalculatorParser(new CommonTokenStream(lexer));

            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowingErrorListener<IToken>());

            return new CalculatorVisitor().Visit(parser.expression());
        }

        private void DisplayResult(int result)
        {
            Console.WriteLine($"Result: {result}");
        }

        private void DisplayError(Exception ex)
        {
            Console.WriteLine("Something didn't go as expected:");
            Console.WriteLine(ex.Message);
        }

        static void Main()
        {
            Program program = new Program();

            try
            {
                string input = program.GetInput();
                int result = program.EvaluateInput(input);

                program.DisplayResult(result);
            }
            catch(Exception ex)
            {
                program.DisplayError(ex);
            }

            Console.Write($"{Environment.NewLine}Press ENTER to exit: ");
            Console.ReadKey();
        }
    }
}
