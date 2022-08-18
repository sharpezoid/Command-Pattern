using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTake : ICommand
{
    private Vector3Int from;
    private Vector3Int to;

    private Unit Taker;
    private Unit Taken;
    private Unit.eTeam FormerTeam;

    public CommandTake(Vector3Int _from, Vector3Int _to)
    {
        from = _from;
        to = _to;
    }

    public void Execute()
    {
        CellObject takenObj = Level.Instance.GetCellObject(to);
        if (takenObj != null && takenObj.GetType() == typeof(Unit))
        {
            Taken = takenObj as Unit;
            FormerTeam = Taken.Team;
        }
        CellObject takerObj = Level.Instance.GetCellObject(from);
        if (takerObj != null && takerObj.GetType() == typeof(Unit))
        {
            Taker = takerObj as Unit;
        }

        if (takerObj != null && takenObj != null)
        {
            Level.Instance.SwapUnitTeams(Taken, Taker.Team);
        }

        GameController.Instance.CurrentTeam.UseEnergy();

    }

    public void Undo()
    {
        if (Taken != null)
        {
            Taken.Team = FormerTeam;
            Taken.SetColor();
        }

        GameController.Instance.CurrentTeam.ReturnEnergy();
    }
}
