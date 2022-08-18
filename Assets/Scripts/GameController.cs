using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private Dictionary<Unit.eTeam, Team> Teams = new Dictionary<Unit.eTeam, Team>();

    private Team currentTeam;
    public Team CurrentTeam
    {
        get { return currentTeam; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Unit.eTeam)).Length; i++)
        {
            Teams.Add((Unit.eTeam)i, new Team((Unit.eTeam)i));
        }

        currentTeam = Teams[Unit.eTeam.Red];

        UIController.Instance.SetCurrentTeam(currentTeam);        
    }

    public void ChangeCurrentTeam()
    {
        currentTeam.EndTurn();

        if (currentTeam == Teams[Unit.eTeam.Red])
        {
            currentTeam = Teams[Unit.eTeam.Blue];
            CameraController.Instance.SetBlueCameraTarget();
        }
        else
        {
            currentTeam = Teams[Unit.eTeam.Red];
            CameraController.Instance.SetRedCameraTarget();
        }

        currentTeam.StartTurn();

        CommandController.Instance.ClearCommands();

        UIController.Instance.SetCurrentTeam(currentTeam);

        UIController.Instance.Deselect();
    }
}
