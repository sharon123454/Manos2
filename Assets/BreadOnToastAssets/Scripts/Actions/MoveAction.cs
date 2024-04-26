using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("Base Unit Parameters")]
    [SerializeField] private float _unitRotationSpeed = 7.5f;
    [SerializeField] private float _unitMoveSpeed = 4f;
    [SerializeField] private int _maxMoveDistance = 6;

    private float _stoppingDistance = 0.1f;
    private Vector3 _targetPosition;
    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetPosition) > _stoppingDistance)//Stops jittering from never reaching clean position
        {
            //Animation
            _animator.SetBool("IsWalking", true);
            //Movement
            Vector3 moveDir = (_targetPosition - transform.position).normalized;
            transform.position += moveDir * _unitMoveSpeed * Time.deltaTime;
            //Rotation (smooth rotation because starting point isn't cached)
            transform.forward = Vector3.Lerp(transform.forward, moveDir, _unitRotationSpeed * Time.deltaTime);
        }
        else
        {
            if (_animator.GetBool("IsWalking"))
            {
                _animator.SetBool("IsWalking", false);
            }
        }
    }

    /// <summary>
    /// Sets the units target position.
    /// Unit will move if Update allowes
    /// </summary>
    /// <param name="targetPosition"></param>
    public void Move(GridPosition targetPosition)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
    }

    /// <summary>
    /// Checks if grid selected by input is valid for specific action
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    /// <summary>
    /// Validation of the actions' grid
    /// </summary>
    /// <returns></returns>
    private List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for (int z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) { continue; } //if position is outside the gridSystem
                if (unitGridPosition == testGridPosition) { continue; } //if position is the same as units'
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) { continue; } //if position is occupied by other unit

                validGridPositions.Add(testGridPosition);
            }
        }

        return validGridPositions;
    }

}