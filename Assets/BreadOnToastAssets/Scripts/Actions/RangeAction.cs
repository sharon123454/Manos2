using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class RangeAction : BaseAction
{
    private float _totalSpinAmount;
    private int _maxShootDistance;

    private void Update()
    {
        if (!_isActive) { return; }

        float spinAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAmount, 0);

        _totalSpinAmount += spinAmount;
        if (_totalSpinAmount >= 360)
        {
            _isActive = false;
            _onActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        _onActionComplete = onActionComplete;
        _totalSpinAmount = 0;
        _isActive = true;
    }
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxShootDistance; x <= _maxShootDistance; x++)
        {
            for (int z = -_maxShootDistance; z <= _maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) { continue; } // if position is outside the gridSystem

                //Collective distance from position. Makes valid grid circular instead of square
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > _maxShootDistance) { continue; }

                //if (unitGridPosition == testGridPosition) { continue; } // if position is the same as units'
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) { continue; } // if position empty, NO UNIT

                Unit unitAtGridPosition = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                if (_unit.IsEnemy() == unitAtGridPosition.IsEnemy()) { continue; } // if units are of the same side

                validGridPositions.Add(testGridPosition);
            }
        }

        return validGridPositions;
    }

}