using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected bool _isActive;
    protected Unit _unit;

    protected virtual void Awake()
    {
        _unit = GetComponent<Unit>();
    }

}