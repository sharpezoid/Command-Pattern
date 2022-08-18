using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : CellObject
{
    public MeshRenderer MeshRenderer;
    public enum eTeam
    {
        Red,
        Blue
    }
    public eTeam Team;

    private Vector3Int direction;
    public Vector3Int Direction
    {
        get { return direction; }
    }
    public void SetDirection(Vector3Int newDirection)
    {
        direction = newDirection;
        transform.rotation = Quaternion.LookRotation(newDirection, Vector3Int.up);
    }

    private void OnMouseDown()
    {
        UIController.Instance.SetSelection(Cell);
    }

    public void SetColor()
    {
        switch (Team)
        {
            case eTeam.Red:
                MeshRenderer.material.color = Color.red;
                break;

            case eTeam.Blue:
                MeshRenderer.material.color = Color.blue;
                break;
        }
    }
}

