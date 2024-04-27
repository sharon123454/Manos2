using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
    private float _totalSpinAmount;

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
    public void Spin(Action onActionComplete)
    {
        _isActive = true;
        _totalSpinAmount = 0;
        _onActionComplete = onActionComplete;
    }
}