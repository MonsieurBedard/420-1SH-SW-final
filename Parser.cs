namespace Brainfuck;

public class Parser
{
    public Stmt FirstNode { get; set; }

    public Parser(List<Token> tokens)
    {
        FirstNode = new StmtEOF(new Token(TokenType.EOF, 0, 0, ""));
        bool first = true;

        Stmt currentNode = new StmtEOF(new Token(TokenType.EOF, 0, 0, ""));
        Stmt previousNode = new StmtEOF(new Token(TokenType.EOF, 0, 0, ""));

        Stack<StmtLoopStart> loopStartStack = new Stack<StmtLoopStart>();

        foreach (Token token in tokens)
        {
            previousNode = currentNode;

            switch (token.Type)
            {
                case TokenType.GREATER_THAN:
                    currentNode = new StmtGreaterThan(token);
                    break;
                case TokenType.LESS_THAN:
                    currentNode = new StmtLessThan(token);
                    break;
                case TokenType.PLUS:
                    currentNode = new StmtPlus(token);
                    break;
                case TokenType.MINUS:
                    currentNode = new StmtMinus(token);
                    break;
                case TokenType.FULL_STOP:
                    currentNode = new StmtFullStop(token);
                    break;
                case TokenType.COMMA:
                    currentNode = new StmtComma(token);
                    break;
                case TokenType.LEFT_BRACKET:
                    var node = new StmtLoopStart(token);
                    currentNode = node;
                    loopStartStack.Push(node);
                    break;
                case TokenType.RIGHT_BRACKET:
                    var loopStart = loopStartStack.Pop();
                    currentNode = new StmtLoopEnd(token, loopStart);
                    loopStart.Jump = currentNode;
                    break;
                default:
                    currentNode = new StmtEOF(token);
                    break;
            }

            if (first) {
                FirstNode = currentNode;
                first = false;
            }

            previousNode.Next = currentNode;
        }
    }
}