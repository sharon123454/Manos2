using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Vector3 _targetPosition;
    private float _stoppingDistance = 0.1f;
    private float _moveSpeed = 4f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }

        if (Vector3.Distance(transform.position, _targetPosition) > _stoppingDistance)//Stops jittering from never reaching clean position
        {
            Vector3 moveDir = (_targetPosition - transform.position).normalized;
            transform.position += moveDir * _moveSpeed * Time.deltaTime;
        }
    }

    private void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

}