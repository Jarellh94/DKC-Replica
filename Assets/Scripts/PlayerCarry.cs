using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public Vector3 crateOffset;
    public float throwStrength;
    public Crate myCrate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myCrate) {
            myCrate.transform.position = transform.position + crateOffset;
        }
    }

    public void Pickup(Crate crate) {
        myCrate = crate;
    }

    public void Throw() {
        myCrate.Throw(new Vector2(transform.right.x, transform.right.y) * throwStrength);
        myCrate = null;
    }
}
