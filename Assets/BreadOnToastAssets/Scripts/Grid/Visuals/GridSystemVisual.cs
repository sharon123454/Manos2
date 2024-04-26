using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform _gridSystemVisualSinglePrefab;

    private void Start()
    {
        //needs to be called from somewhere else if combat doesn't start automatically
        DrawGridSystem();
    }

    public void DrawGridSystem()
    {
        for (int x = 0; x < LevelGrid.Instance.GetGridWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetGridHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Instantiate(_gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity, transform);
            }
        }
    }

}