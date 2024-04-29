using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject _actionsUIParent;
    [SerializeField] private Transform _actionButtonPrefab;
    [SerializeField] private Transform _buttonContainer;

    private List<ActionButtonUI> _actionButtonUIList;

    private void Awake()
    {
        _actionButtonUIList = new List<ActionButtonUI>();
    }
    private void Start()
    {
        UnitActionSystem.Instance.OnActionStart += UnitActionSystem_OnActionStart;
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
    }
    private void OnDisable()
    {
        UnitActionSystem.Instance.OnActionStart -= UnitActionSystem_OnActionStart;
        UnitActionSystem.Instance.OnBusyChanged -= UnitActionSystem_OnBusyChanged;
        UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged -= UnitActionSystem_OnSelectedActionChanged;
    }

    private void UnitActionSystem_OnActionStart(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }
    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        _actionsUIParent.SetActive(!isBusy);
    }
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedButtonVisual();
    }
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedButtonVisual();
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        Debug.Log($"{transform} - {selectedUnit.GetActionPoints()}");
        Debug.Log($"{transform} - {selectedUnit.GetBonusActionPoints()}");
    }
    private void UpdateSelectedButtonVisual()
    {
        foreach (ActionButtonUI actionButton in _actionButtonUIList)
        {
            actionButton.UpdateSelectedVisual();
        }
    }
    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in _buttonContainer)
        {
            Destroy(buttonTransform.gameObject);
        }
        _actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(_actionButtonPrefab, _buttonContainer);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
            _actionButtonUIList.Add(actionButtonUI);
        }
    }

}