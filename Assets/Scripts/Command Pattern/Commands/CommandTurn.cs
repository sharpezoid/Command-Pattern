using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTurn : ICommand
{
    private Vector3Int cell;
    private Vector3Int from;
    private Vector3Int to;

    public CommandTurn(Vector3Int _cell, Vector3Int _from, Vector3Int _to)
    {
        cell = _cell;
        from = _from;
        to = _to;
    }

    public void Execute()
    {
        CellObject cellObj = Level.Instance.GetCellObject(cell);
        if (cellObj != null && cellObj.GetType() == typeof(Unit))
        {
            (cellObj as Unit).SetDirection(to);
            GameController.Instance.CurrentTeam.UseEnergy();
        }
    }

    public void Undo()
    {
        CellObject cellObj = Level.Instance.GetCellObject(cell);
        if (cellObj != null && cellObj.GetType() == typeof(Unit))
        {
            (cellObj as Unit).SetDirection(from);
            GameController.Instance.CurrentTeam.ReturnEnergy();
        }
    }
}
