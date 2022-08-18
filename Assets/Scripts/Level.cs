using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject UnitPrefab;
    public GameObject BarrierPrefab;

    public int BarrierCount = 10;

    public Vector2Int Size;

    private Grid grid;
    public Grid Grid
    {
        get { return grid; }
    }

    private CellObject[,] cellContents;

    public static Level Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        grid = GetComponent<Grid>();
        cellContents = new CellObject[Size.x, Size.y];

        SpawnUnits();

        SpawnBarriers();
    }

    void SpawnUnits()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (i % 2 == 0)
                {
                    Vector3Int cell = new Vector3Int(i, 0, 0);
                    if (j == 1)
                        cell.z = Size.y - 1;
                    GameObject newUnit = GameObject.Instantiate(UnitPrefab, GetWorldPositionFromCell(cell), Quaternion.identity);
                    Unit unit = newUnit.GetComponent<Unit>();
                    SetCellObject(cell, unit);
                    if (j == 0)
                    {
                        unit.Team = Unit.eTeam.Red;
                        unit.SetDirection(Vector3Int.forward);
                    }
                    else
                    {
                        unit.Team = Unit.eTeam.Blue;
                        unit.SetDirection(Vector3Int.back);
                    }
                    unit.SetColor();
                }
            }
        }
    }

    void SpawnBarriers()
    {
        int maxAttempts = BarrierCount * 2;
        int placedBarriers = 0;
        do
        {
            Vector3Int pos = new Vector3Int();
            pos.x = Random.Range(1, Size.x - 1);
            pos.y = 0;
            pos.z = Random.Range(1, Size.y - 1);

            if (SpawnBarrier(pos))
                placedBarriers++;

            maxAttempts--;
        }
        while (placedBarriers < BarrierCount && maxAttempts > 0);
    }

    public Vector3Int GetCellFromWorldPosition(Vector3 _pos)
    {
        return grid.WorldToCell(_pos);
    }

    public Vector3 GetWorldPositionFromCell(Vector3Int _pos)
    {
        return grid.CellToWorld(_pos) + new Vector3(0.5f, 0, 0.5f);
    }

    public void SetCellObject(Vector3Int _pos, CellObject _object)
    {
        if (!IsWithinBounds(_pos))
            return;

        if (cellContents[_pos.x, _pos.z] != null)
            return;

        cellContents[_pos.x, _pos.z] = _object;
        _object.Cell = _pos;
    }

    public CellObject GetCellObject(Vector3Int _pos)
    {
        if (!IsWithinBounds(_pos))
            return null;

        return cellContents[_pos.x, _pos.z];
    }

    public void MoveUnit(Unit _unit, Vector3Int _to)
    {
        if (!IsWithinBounds(_to))
            return;

        if (cellContents[_to.x, _to.z] == null)
        {
            cellContents[_to.x, _to.z] = _unit;
            cellContents[_unit.Cell.x, _unit.Cell.z] = null;
            _unit.Cell = _to;
            _unit.transform.position = GetWorldPositionFromCell(_to);
        }
    }

    public bool IsWithinBounds(Vector3Int _pos)
    {
        return (_pos.x >= 0
             && _pos.x < Size.x
             && _pos.z >= 0
             && _pos.z < Size.y);
    }

    public bool SpawnBarrier(Vector3Int _cell)
    {
        if (GetCellObject(_cell) == null)
        {
            GameObject newBarrier = GameObject.Instantiate(BarrierPrefab);
            newBarrier.transform.position = GetWorldPositionFromCell(_cell);
            Barrier barrier = newBarrier.GetComponent<Barrier>();
            cellContents[_cell.x, _cell.z] = barrier;
            return true;
        }

        return false;
    }

    public void SwapUnitTeams(Unit _unit, Unit.eTeam _toTeam)
    {
        _unit.Team = _toTeam;
        _unit.SetColor();
    }
}
