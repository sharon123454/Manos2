using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    private string[] _activeLayerNames = { "Unit" };
    private LayerMask _activeUnitLayerMask;

    private Unit _selectedUnit;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"There's more than one UnitActionSystem! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _activeUnitLayerMask.value = LayerMask.GetMask(_activeLayerNames);
    }
    private void Update()
    {
        if (InputManager.Instance.IsMouseButtonDown())
        {
            if (TryRaycastUnitSelection()) { return; }

            if (_selectedUnit != null)
                _selectedUnit.GetMoveAction().Move(MouseWorld.GetPosition());
        }
    }

    public Unit GetSelectedUnit() { return _selectedUnit; }

    private bool TryRaycastUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetPointerPosition());
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, _activeUnitLayerMask))
        {
            if (rayCastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }
    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

}