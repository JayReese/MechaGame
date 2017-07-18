using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedManager : MonoBehaviour
{
    [SerializeField] protected GameManager GlobalManagement;
    protected GameState _currentGameState;

    protected void Awake()
    {
        GlobalManagement = GameObject.FindGameObjectWithTag("Global").GetComponent<GameManager>();
        //Debug.Log("called");
    }

    void Start()
    {

    }
}
