using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject player;
    public GameObject cam;

    public float xRight, xLeft;
    public float offset;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < xLeft)
        {
            Vector3 p = player.transform.position;
            player.transform.position = new Vector3(xRight - offset, p.y, p.z);
        }

        if (player.transform.position.x > xRight)
        {
            Vector3 p = player.transform.position;
            player.transform.position = new Vector3(xLeft + offset, p.y, p.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(xLeft, 0, 0), new Vector3(0.02f, 100, 30));
        Gizmos.DrawCube(new Vector3(xRight, 0, 0), new Vector3(0.02f, 100, 30));
    }
}
