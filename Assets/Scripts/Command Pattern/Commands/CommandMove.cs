using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : ICommand
{
    private Vector3Int from;
    private Vector3Int to;

    public CommandMove(Vector3Int _from, Vector3Int _to)
    {
        from = _from;
        to = _to;
    }

    public void Execute()
    {
        CellObject cellObj = Level.Instance.GetCellObject(to);
        CellObject unitObj = Level.Instance.GetCellObject(from);

        if (cellObj == null && unitObj != null && unitObj.GetType() == typeof(Unit))
        {
            Level.Instance.MoveUnit(unitObj as Unit, to);
            UIController.Instance.SetSelection(to);
            GameController.Instance.CurrentTeam.UseEnergy();
        }
    }

    public void Undo()
    {
        CellObject cellObj = Level.Instance.GetCellObject(to);
        if (cellObj != null && cellObj.GetType() == typeof(Unit))
        {
            Level.Instance.MoveUnit(cellObj as Unit, from);
            UIController.Instance.SetSelection(from);
            GameController.Instance.CurrentTeam.ReturnEnergy();
        }
    }
}
