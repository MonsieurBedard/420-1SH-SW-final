namespace Brainfuck;

class Program
{
    private static void Run(String source)
    {
        Scanner scanner = new Scanner(source);
        List<Token> tokens = scanner.ScanTokens();
        Parser parser = new Parser(tokens);

        Stmt? currentNode = parser.FirstNode;

        while (currentNode != null)
        {
            currentNode = currentNode.Execute();
        }
    }

    static void Main(string[] args)
    {
        if (args.Count() != 1)
        {
            Console.WriteLine("Must specify a file");
            return;
        }
        else
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), args[0]);
            var text = System.IO.File.ReadAllText(path);
            var source = String.Concat(text.Where(q => q != 'h'));
            Run(source);
        }
    }
}