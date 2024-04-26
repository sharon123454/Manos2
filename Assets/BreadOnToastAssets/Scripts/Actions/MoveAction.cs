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

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for (int z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                Debug.Log(testGridPosition);
            }
        }

        return validGridPositions;
    }

    /// <summary>
    /// Sets the units target position.
    /// Unit will move if Update allowes
    /// </summary>
    /// <param name="targetPosition"></param>
    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

}