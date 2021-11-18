using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get;  private set; }
    public static MovingCube LastCube { get;  private set; }
    public MoveDirection MoveDirection { get; set; }
    public bool moveD = true;
    private Color[] colors = { Color.green, Color.blue, Color.red, Color.black, Color.white};
    private int s = 0;
    private int d = 0;
    private Color sc;
    private Color dc;
    private bool flag = true;

    private AudioSource source;
    
    [SerializeField][Range(0f, 1f)] float lerpTime;

    [SerializeField]
    public float moveSpeed = 1f; //private bood
    /*private void Awake()
    {
        //colors =  {Color.green, Color.blue, Color.red, Color.black, Color.white};
        Debug.Log("hi");
        int sindex = UnityEngine.Random.Range(0, 5);
        int dindex;
        
        if (sindex == 4)
            dindex = 0;
        else
            dindex = sindex + 1;
        s = colors[sindex];
        d = colors[dindex];
    }*/
    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        CurrentCube = this;

        source = GetComponent<AudioSource>();
        GetComponent<Renderer>().material.color = GetColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }
    private Color GetColor()
    {
        
        if (flag)
        {
            Debug.Log("hi");
            s = UnityEngine.Random.Range(0, 5);
            if (s == 4)
                d = 0;
            else
                d = s + 1;
            flag = false;
            sc = colors[s];
            dc = colors[d];
        }
        //Debug.Log("sindex : " + sc);
        //Debug.Log("dindex : " + dc);
        return Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1f));
   //     return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));   
    }
    internal void Stop()
    {
        moveSpeed = 0;
        //if (GameObject.Find("Start").GetComponent<MovingCube>() != LastCube)
        source.Play();

        float hangOver = GetHangOver();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if (Mathf.Abs(hangOver) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangOver > 0 ? 1f : -1f;
        if (MoveDirection == MoveDirection.Z) {
            SplitCubeOnZ(hangOver, direction);
        }
        else
        {
            SplitCubeOnX(hangOver, direction);
        }
        LastCube = this;
    }

    private float GetHangOver()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
        {
            return transform.position.x - LastCube.transform.position.x;
        }
    }

    private void SplitCubeOnX(float hangOver, float direction)
    {
        float newSize = LastCube.transform.localScale.x - Math.Abs(hangOver);
        float fallingBlockSize = transform.localScale.x - newSize;

        float newXPosition = LastCube.transform.position.x + (hangOver / 2);

        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }
    private void SplitCubeOnZ(float hangOver, float direction)
    {
        float newSize = LastCube.transform.localScale.z - Math.Abs(hangOver);
        float fallingBlockSize = transform.localScale.z - newSize;

        float newZPosition = LastCube.transform.position.z + (hangOver / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z) {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f); ///change it
    }

    private void Update()
    {
        //if (MoveDirection == MoveDirection.Z)
        if (MoveDirection == MoveDirection.Z)
        {
            if (moveD)
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
            else
                transform.position -= transform.forward * Time.deltaTime * moveSpeed;
            if (transform.position.z >= 1.82)
            {
                moveD = false;
            }
            if (transform.position.z <= -1.82)
            {
                moveD = true;
            }

        }
        else
        {
            if(moveD)
                transform.position += transform.right * Time.deltaTime * moveSpeed;
            else
                transform.position -= transform.right * Time.deltaTime * moveSpeed;
            if (transform.position.x >= 1.82)
            {
                moveD = false;
            }
            if (transform.position.x <= -1.82)
            {
                moveD = true;
            }
        }
    }
}
