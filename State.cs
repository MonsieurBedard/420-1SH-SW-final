namespace Brainfuck;

public sealed class State
{
    private char[] memory;
    private int pointer;

    private static State? instance = null;

    private State()
    {
        memory = new char[30_000];
        for (int i = 0; i < memory.Length; i++)
        {
            memory[i] = (char)0;
        }
        pointer = 0;
    }

    public static State Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new State();
            }
            return instance;
        }
    }

    public (char[], int) GetState()
    {
        return (memory, pointer);
    }

    public void SetState(char[] memory, int pointer)
    {
        this.memory = memory;
        this.pointer = pointer;
    }
}