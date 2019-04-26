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
            
        }

        if (player.transform.position.x > xRight)
        {
            
        }
    }
}
