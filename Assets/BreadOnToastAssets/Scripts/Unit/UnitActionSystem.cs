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
            //Handles clicked character
            if (TryRaycastUnitSelection()) { return; }

            //Stops unit logic if no Unit selected
            if (GetSelectedUnit() == null) { Debug.Log("No unit is selected"); return; }

            //Handles clicked input
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (_selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                _selectedUnit.GetMoveAction().Move(mouseGridPosition);
            else
                Debug.Log("Position clicked isn't valid");
        }
    }

    public Unit GetSelectedUnit() { return _selectedUnit; }

    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
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

}