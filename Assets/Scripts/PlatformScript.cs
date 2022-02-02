using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Transform playerFoot;
    private Collider2D playerCollider;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        playerFoot = FindObjectOfType<PlayerMovement>().GetFoot();   
        playerCollider = playerFoot.GetComponentInParent<CapsuleCollider2D>();
        coll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(playerFoot.position.y <= transform.position.y) {
            Physics2D.IgnoreCollision(coll, playerCollider, true);
        } else {
            Physics2D.IgnoreCollision(coll, playerCollider, false);
        }
    }
}
