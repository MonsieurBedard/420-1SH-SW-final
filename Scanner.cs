using System.Text.RegularExpressions;

namespace Brainfuck;

public class Scanner
{
    private string Source;
    public List<Token> Tokens { get; }

    private int start = 0;
    private int current = 0;
    private int line = 1;
    private int position = 0;

    public Scanner(string source)
    {
        this.Source = source;
        this.Tokens = new List<Token>();
    }

    public List<Token> ScanTokens()
    {
        while (!isAtEnd())
        {
            start = current;
            this.ScanToken();
        }

        Tokens.Add(new Token(TokenType.EOF, line, position, ""));
        return this.Tokens;
    }

    private void ScanToken()
    {
        var c = advance();
        switch (c)
        {
            case '>': addToken(TokenType.GREATER_THAN, c); break;
            case '<': addToken(TokenType.LESS_THAN, c); break;
            case '+': addToken(TokenType.PLUS, c); break;
            case '-': addToken(TokenType.MINUS, c); break;
            case '.': addToken(TokenType.FULL_STOP, c); break;
            case ',': addToken(TokenType.COMMA, c); break;
            case '[': addToken(TokenType.LEFT_BRACKET, c); break;
            case ']': addToken(TokenType.RIGHT_BRACKET, c); break;
            case '\n': line++; break;
        }
    }

    private char advance()
    {
        return Source[current++];
    }

    private void addToken(TokenType type, char text)
    {
        Tokens.Add(new Token(type, line, current, text.ToString()));
    }

    private void addToken(TokenType type, string text)
    {
        Tokens.Add(new Token(type, line, current, text));
    }

    private Boolean isAtEnd()
    {
        return current >= Source.Length;
    }
}
