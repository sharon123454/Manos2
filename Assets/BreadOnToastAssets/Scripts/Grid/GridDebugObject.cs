using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro _debugObjectTMP;

    private GridObject _gridObject;
    
    public void SetGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;
        _debugObjectTMP.text = _gridObject.ToString();
    }

}