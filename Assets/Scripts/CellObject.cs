using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellObject : MonoBehaviour
{
    Vector3Int currentCell;
    public Vector3Int Cell
    {
        get { return currentCell; }
        set { currentCell = value; }
    }
}
