using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition _currentGridPosition;
    private MoveAction _moveAction;
    private SpinAction _spinAction;
    private BaseAction[] _baseActionArray;

    private void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }
    private void Start()
    {
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
    }
    //updates character movement
    void Update()
    {
        //Unit changed Grid Position check
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);//if null causes problems (can prolly move to line 44 at the end of movement)
        if (newGridPosition != _currentGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(_currentGridPosition, this, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
    }

    /// <summary>
    /// Allows Actions to get the units GridPosition without calculations
    /// </summary>
    /// <returns></returns>
    public GridPosition GetGridPosition() { return _currentGridPosition; }
    public BaseAction[] GetBaseActionArray() { return _baseActionArray; }
    /// <summary>
    /// Allows UnitActionSystem to reach the Units' Action
    /// </summary>
    /// <returns></returns>
    public MoveAction GetMoveAction() { return _moveAction; }
    public SpinAction GetSpinAction() { return _spinAction; }

}