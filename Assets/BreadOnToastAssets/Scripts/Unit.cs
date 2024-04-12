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

    private void Awake()
    {
        _targetPosition = transform.position;
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
                _animator.SetBool("IsWalking", false);
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