using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public abstract class BaseAction : MonoBehaviour
{
    protected Action _onActionComplete;
    protected bool _isActive;
    protected Unit _unit;

    protected virtual void Awake()
    {
        _unit = GetComponent<Unit>();
    }

}