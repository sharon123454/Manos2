using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    private string[] _activeLayerNames = { "Unit" };
    private LayerMask _activeUnitLayerMask;
    private Unit _selectedUnit;

    private void Awake()
    {
        _activeUnitLayerMask.value = LayerMask.GetMask(_activeLayerNames);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryRaycastUnitSelection()) { return; }

            if (_selectedUnit != null)
                _selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryRaycastUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, _activeUnitLayerMask))
        {
            if (rayCastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                _selectedUnit = unit;
                return true;
            }
        }
        return false;
    }

}