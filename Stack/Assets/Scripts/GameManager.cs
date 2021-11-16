using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (MovingCube.CurrentCube != null) {
                MovingCube.LastCube = MovingCube.CurrentCube;
                //MovingCube.CurrentCube.Stop();
                MovingCube.LastCube.Stop();
            }
            MovingCube.CurrentCube = FindObjectOfType<CubeSpawner>().SpawnCube();
            MovingCube.CurrentCube.moveSpeed = 1f;
        }
    }
}
