using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform _gridDebugObjectPrefab;
    [SerializeField] private bool _gridDebugEnabled = false;
    [SerializeField] private int _levelGridWidth = 10;
    [SerializeField] private int _levelGridHeight = 10;
    [SerializeField] private float _levelGridCellSize = 2f;

    private GridSystem _gridSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"There's more than one LevelGrid! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _gridSystem = new GridSystem(_levelGridWidth, _levelGridHeight, _levelGridCellSize);

        if (_gridDebugEnabled)
            _gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
    }

    public void UnitMovedGridPosition(GridPosition fromGridPosition, Unit unit, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    /// <summary>
    /// Passing the mathematical gridPosition from the GridSystem
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return _gridSystem.GetGridPosition(worldPosition);
    }

}