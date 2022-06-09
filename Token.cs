namespace Brainfuck;

public enum TokenType
{
    EOF,

    GREATER_THAN, LESS_THAN,
    PLUS, MINUS,
    FULL_STOP, COMMA,
    LEFT_BRACKET, RIGHT_BRACKET
}

public class Token
{
    public TokenType Type { get; }
    public string lexeme { get; }
    public int line { get; }
    public int position { get; }

    public Token(TokenType type, int line, int position, string lexeme)
    {
        this.Type = type;
        this.line = line;
        this.position = position;
        this.lexeme = lexeme;
    }

    public override string ToString()
    {
        return String.Format("token: {0}, {1}:{2}, {3}", this.Type, this.line, this.position, this.lexeme);
    }
}