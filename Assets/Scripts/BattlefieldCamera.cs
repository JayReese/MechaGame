using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldCamera : MonoBehaviour
{

    [SerializeField]
    Transform _centerOfBattlefield;
    [SerializeField]
    float _rotationSpeed = 20f;

    // Use this for initialization
    void Start()
    {
        _centerOfBattlefield = GameObject.Find("Level Center").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_centerOfBattlefield.position, Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
