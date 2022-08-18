using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    const int INITIAL_ENERGY = 5;

    Unit.eTeam team;
    public List<Unit> Units = new List<Unit>();
    private int energy;
    public int Energy
    {
        get { return energy; }
    }
    private int rolloverEnergy;
    
    public Team(Unit.eTeam _team)
    {
        team = _team;
        energy = INITIAL_ENERGY;
    }

    public Unit.eTeam GetTeam()
    {
        return team;
    }

    public void UseEnergy()
    {
        energy--;
        UIController.Instance.SetCurrentTeam(this);
    }

    public void ReturnEnergy()
    {
        energy++;
        UIController.Instance.SetCurrentTeam(this);
    }

    public void StartTurn()
    {
        energy = INITIAL_ENERGY + rolloverEnergy;
        UIController.Instance.SetCurrentTeam(this);
    }

    public void EndTurn()
    {
        rolloverEnergy = energy;
        UIController.Instance.SetCurrentTeam(this);
    }
}
