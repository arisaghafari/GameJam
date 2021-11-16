using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get;  set; }
    public static MovingCube LastCube { get;  set; }

    [SerializeField]
    public float moveSpeed = 1f;

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
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f); ///change it
    }

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

        GetComponent<Renderer>().material.color = GetColor();
        /*if (CurrentCube.moveSpeed == 0)
        {
            LastCube = CurrentCube;
        }*/
    }

    private Color GetColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f)); //change it
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
