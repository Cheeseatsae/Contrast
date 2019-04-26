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
            cam.transform.SetParent(player.transform, true);
            player.transform.position = new Vector3(xRight - offset, p.y, p.z);
            cam.transform.SetParent(cam.transform, true);
        }

        if (player.transform.position.x > xRight)
        {
            Vector3 p = player.transform.position;
            cam.transform.SetParent(player.transform, true);
            player.transform.position = new Vector3(xLeft + offset, p.y, p.z);
            cam.transform.SetParent(cam.transform, true);
        }
    }

    private void TeleportCamera()
    {
        Vector3 p = player.transform.position;
        Vector3 c = cam.transform.position;

        cam.transform.SetParent(player.transform);
        
        Vector3 diff = p - c;
        Debug.Log(diff);

        cam.transform.position = new Vector3(p.x + diff.x,p.y + diff.y, c.z);
        Debug.Log(player.transform.position - cam.transform.position); 
    }
    
}
