using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CamController : MonoBehaviour
{

    public GameObject player;

    public float speed = 1;
    public float xFloatingMult;
    public float yFloatingMult;
    
    public float xMinClamp;
    public float xMaxClamp;
    
    public float yMinClamp;
    public float yMaxClamp;
    public float yOffset;

    private void Start()
    {
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 pPos = new Vector2(player.transform.position.x, player.transform.position.y);
        
        UpdatePos(myPos, pPos);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 pPos = new Vector2(player.transform.position.x, player.transform.position.y);

         UpdatePos(myPos, pPos);
    }

    void UpdatePos(Vector2 m, Vector2 p)
    {
        Vector2 newPos;

        Vector2 pVelocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector2 pPos = new Vector2(p.x + pVelocity.x * xFloatingMult, p.y + yOffset + pVelocity.y * yFloatingMult);
        
        newPos = Vector2.Lerp(m, pPos, speed * Time.deltaTime);
        
        newPos = new Vector2(Mathf.Clamp(newPos.x, xMinClamp,xMaxClamp), Mathf.Clamp(newPos.y, yMinClamp, yMaxClamp));

        transform.position = new Vector3(newPos.x, newPos.y , transform.position.z);

    }
}
