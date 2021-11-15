using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    
    [SerializeField]
    private float moveSpeed = 1f;

    internal void Stop()
    {
        moveSpeed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        CurrentCube = this;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
