using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("Base Unit Parameters")]
    [SerializeField] private float _unitRotationSpeed = 10f;
    [SerializeField] private float _unitMoveSpeed = 6.2f;

    private float _stoppingDistance = 0.1f;
    private Vector3 _targetPosition;
    private GridPosition _currentGridPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
    }
    private void Start()
    {
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
    }
    //updates character movement
    void Update()
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

        //Unit changed Grid Position check
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);//if null causes problems (can prolly move to line 44 at the end of movement)
        if (newGridPosition != _currentGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(_currentGridPosition, this, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
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