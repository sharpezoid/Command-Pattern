using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBreak : ICommand
{

    private Vector3Int target;

    public CommandBreak(Vector3Int _target)
    {
        target = _target;
    }

    public void Execute()
    {
        
        CellObject cellObj = Level.Instance.GetCellObject(target);
        if (cellObj != null && cellObj.GetType() == typeof(Barrier))
        {
            GameObject.Destroy(cellObj.gameObject);
            Level.Instance.SetCellObject(target, null);
            GameController.Instance.CurrentTeam.UseEnergy();
        }
    }

    public void Undo()
    {
        Level.Instance.SpawnBarrier(target);
        GameController.Instance.CurrentTeam.ReturnEnergy();
    }
}
