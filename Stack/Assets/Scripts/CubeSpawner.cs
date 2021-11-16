using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;

    public MovingCube SpawnCube()
    {
        var cube = Instantiate(cubePrefab);
        //cube.moveSpeed = 1f;
       // MovingCube.LastCube = MovingCube.CurrentCube;
        //MovingCube.CurrentCube = cube;
        //cube.moveSpeed = 1f;
        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start")) {
            cube.transform.position = new Vector3(transform.position.x, MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }
        return cube;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
