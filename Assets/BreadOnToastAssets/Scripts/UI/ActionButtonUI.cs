using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionNameText;
    [SerializeField] private Button _actionButton;
    [SerializeField] private Image _actionImage;

    public void SetBaseAction(BaseAction baseAction)
    {
        _actionNameText.text = baseAction.GetActionName();

        Image actionImage = baseAction.GetActionImage();
        if (actionImage != null) { _actionImage = actionImage; }

        _actionButton.onClick.AddListener(() => { UnitActionSystem.Instance.SetSelectedAction(baseAction); });
    }

}