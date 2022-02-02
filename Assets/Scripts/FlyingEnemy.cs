using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public bool goingUp = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if(goingUp) {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.up * -1 * moveSpeed * Time.deltaTime);
        }

        
        if(Vector3.Distance(startingPoint, transform.position) > moveDistance) {
            goingUp = !goingUp;
            if(goingUp) {
                transform.Translate(Vector3.up * .05f);
            } else {
                transform.Translate(Vector3.down * .05f);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startingPoint + new Vector3(0, moveDistance, 0), startingPoint - new Vector3(0, moveDistance, 0));
    }
}
