using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public TextMeshProUGUI CommandsText;
    public TextMeshProUGUI CurrentTeamText;

    public GameObject SelectionObject;
    Vector3Int selectedCell;
    public Vector3Int SelectedCell
    {
        get { return selectedCell; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < CommandController.Instance.CommandBufferSize; i++)
        {
            sb.AppendLine(CommandController.Instance.GetCommand(i).ToString());
        }

        CommandsText.text = sb.ToString();
    }

    public void SetSelection(Vector3Int cell)
    {
        //check for contents in cell...
        CellObject cellObj = Level.Instance.GetCellObject(cell);
        if (cellObj != null && cellObj.GetType() == typeof(Unit) 
            && (cellObj as Unit).Team == GameController.Instance.CurrentTeam.GetTeam())
        {
            SelectionObject.SetActive(true);
            selectedCell = cell;
            SelectionObject.transform.position = Level.Instance.GetWorldPositionFromCell(cell);
        }        
    }

    public void Deselect()
    {
        SelectionObject.SetActive(false);
    }

    public void MoveUp()
    {
        Move(Vector3Int.forward);
    }

    public void MoveDown()
    {
        Move(Vector3Int.back);
    }

    public void MoveRight()
    {
        Move(Vector3Int.right);
    }

    public void MoveLeft()
    {
        Move(Vector3Int.left);
    }

    private void Move(Vector3Int _direction)
    {
        if (GameController.Instance.CurrentTeam.Energy <= 0 || !SelectionObject.activeSelf)
            return;

        CellObject cellObj = Level.Instance.GetCellObject(selectedCell);
        if (cellObj != null && cellObj.GetType() == typeof(Unit))
        {
            Unit unit = cellObj as Unit;

            if (unit.Team == Unit.eTeam.Blue)
                _direction *= -1;

            if (unit.Direction == _direction)
            {
                Vector3Int target = unit.Cell + _direction;
                CellObject targetCellObj = Level.Instance.GetCellObject(target);
                if (targetCellObj != null)
                {
                    if (targetCellObj.GetType() == typeof(Unit) &&
                    unit.Team != (targetCellObj as Unit).Team)
                    {
                        CommandController.Instance.AddCommand(
                            new CommandTake(unit.Cell, target));
                    }
                    else if (targetCellObj.GetType() == typeof(Barrier))
                    {
                        CommandController.Instance.AddCommand(
                            new CommandBreak(target));
                    }
                }
                else if (Level.Instance.IsWithinBounds(target))
                {
                    CommandController.Instance.AddCommand(
                        new CommandMove(unit.Cell, target));
                }
            }
            else
            {
                CommandController.Instance.AddCommand(
                    new CommandTurn(unit.Cell, unit.Direction, _direction));
            }
        }
    }

    public void Undo()
    {
        CommandController.Instance.Undo();
    }

    public void EndTurn()
    {
        GameController.Instance.ChangeCurrentTeam();
        Deselect();
    }

    public void SetCurrentTeam(Team _team)
    {
        CurrentTeamText.text = string.Format("Current Team: {0}   Energy: {1}", _team.GetTeam().ToString(), _team.Energy);
    }
}
