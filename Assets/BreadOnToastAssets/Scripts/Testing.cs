using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform _gridDebugObjectPrefab;

    private GridSystem _gridSystem;

    private void Start()
    {
        _gridSystem = new GridSystem(10,10,2f);
        _gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
    }

    private void Update()
    {
        
    }

}