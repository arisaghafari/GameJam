using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField]
    private float moveSpeed = 1f;

    internal void Stop()
    {
        moveSpeed = 0;
        float hangOver = transform.position.z - LastCube.transform.position.z;
        float direction = hangOver > 0 ? 1f : -1f;
        SplitCubeOnZ(hangOver, direction);
    }

    private void SplitCubeOnZ(float hangOver, float direction)
    {
        float newSize = LastCube.transform.localScale.z - Math.Abs(hangOver);
        float fallingBlockSize = LastCube.transform.localScale.z - newSize;

        float newZPosition = LastCube.transform.position.z + (hangOver / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        cube.AddComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        CurrentCube = this;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
