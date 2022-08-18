using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public static CommandController Instance;
    private void Awake()
    {
        Instance = this;
    }

    private Stack<ICommand> CommandBuffer = new Stack<ICommand>();

    public int CommandBufferSize
    {
        get { return CommandBuffer.Count; }
    }

    public void ClearCommands()
    {
        CommandBuffer.Clear();
    }

    public ICommand GetCommand(int _index)
    {
        return CommandBuffer.ToArray()[_index];
    }

    public void AddCommand(ICommand _command)
    {
        _command.Execute();
        CommandBuffer.Push(_command);
    }

    public void Undo()
    {
        if (CommandBuffer.Count == 0)
            return;

        ICommand command = CommandBuffer.Pop();
        command.Undo();
    }
}
