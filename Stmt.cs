namespace Brainfuck;

public abstract class Stmt
{
    public int ID { get; set; }
    public Token Token { get; set; }

    public Stmt? Next { get; set; }

    public Stmt(Token token)
    {
        Token = token;
        ID = new Random().Next();
    }

    public abstract Stmt? Execute();
}

public class StmtGreaterThan : Stmt
{
    public StmtGreaterThan(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        pointer++;
        state.SetState(memory, pointer);
        return Next;
    }
}

public class StmtLessThan : Stmt
{
    public StmtLessThan(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        pointer--;
        state.SetState(memory, pointer);
        return Next;
    }
}

public class StmtPlus : Stmt
{
    public StmtPlus(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        memory[pointer]++;
        state.SetState(memory, pointer);
        return Next;
    }
}

public class StmtMinus : Stmt
{
    public StmtMinus(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        memory[pointer]--;
        state.SetState(memory, pointer);
        return Next;
    }
}

public class StmtFullStop : Stmt
{
    public StmtFullStop(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        Console.Write((char)memory[pointer]);
        return Next;
    }
}

public class StmtComma : Stmt
{
    public StmtComma(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        var c = Console.ReadKey(true).KeyChar;
        if (c == '\n') {
            Console.WriteLine("Hello there");
        }
        memory[pointer] = c;
        state.SetState(memory, pointer);
        return Next;
    }
}

public class StmtLoopStart : Stmt
{
    public StmtLoopStart(Token token) : base(token) { }

    public Stmt? Jump { get; set; }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        if (memory[pointer] == 0)
        {
            return Jump.Next;
        }
        else
        {
            return Next;
        }
    }

    public override string ToString()
    {
        return base.ToString() + " " + this.ID + " " + Jump.ID;
    }
}

public class StmtLoopEnd : Stmt
{
    public Stmt? Jump { get; set; }

    public StmtLoopEnd(Token token, Stmt jump) : base(token)
    {
        Jump = jump;
    }

    public override Stmt? Execute()
    {
        var state = State.Instance;
        var (memory, pointer) = state.GetState();
        if (memory[pointer] != 0)
        {
            return Jump;
        }
        else
        {
            return Next;
        }
    }

    public override string ToString()
    {
        return base.ToString() + " " + this.ID + " " + Jump.ID;
    }
}

public class StmtEOF : Stmt
{
    public StmtEOF(Token token) : base(token) { }

    public override Stmt? Execute()
    {
        return null;
    }
}
