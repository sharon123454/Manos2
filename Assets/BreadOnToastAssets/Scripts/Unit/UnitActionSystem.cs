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

    private BaseAction _selectedAction;
    private Unit _selectedUnit;
    private bool _isBusy;

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
        //place under line 37 to allow switching characters mid action
        if (_isBusy) { Debug.Log("Unit is busy"); return; }

        //Handles clicked character
        if (TryRaycastUnitSelection()) { return; }

        HandleSelectedAction();
    }

    public Unit GetSelectedUnit() { return _selectedUnit; }
    public void SetSelectedAction(BaseAction action) { _selectedAction = action; }
    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        SetSelectedAction(_selectedUnit.GetMoveAction());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleSelectedAction()
    {
        //Stops action logic if no Unit selected
        if (GetSelectedUnit() == null) { Debug.Log("No unit is selected"); return; }

        if (InputManager.Instance.IsMouseButtonDown())
        {
            switch (_selectedAction)
            {
                case MoveAction moveAction:
                    GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                    if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                    {
                        SetBusy();
                        moveAction.Move(mouseGridPosition, ClearBusy);
                    }
                    else { Debug.Log("Position clicked isn't valid"); }
                    break;
                case SpinAction spinAction:
                    SetBusy();
                    spinAction.Spin(ClearBusy);
                    break;
                default:
                    break;
            }
        }
    }
    private bool TryRaycastUnitSelection()
    {
        if (InputManager.Instance.IsMouseButtonDown())
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
        }
        return false;
    }
    private void SetBusy() { _isBusy = true; }
    private void ClearBusy() { _isBusy = false; }

}