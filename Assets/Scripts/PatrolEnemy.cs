using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    public bool goingRight = true;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
    }

    private void FixedUpdate() {
        CheckForGround();
        Move();
    }

    protected override void Move()
    {
        if(isGroundAhead) {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        
        if(transform.position.x >= startingPoint.x + moveDistance || (!isGroundAhead && goingRight)) {
            goingRight = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if(transform.position.x <= startingPoint.x - moveDistance || (!isGroundAhead && !goingRight)) {
            goingRight = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

}
