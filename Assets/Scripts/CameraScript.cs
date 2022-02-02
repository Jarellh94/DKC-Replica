using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    private Rigidbody2D playerRig;

    private float moveCounter = 0;
    public float  offsetX = 0, offsetY = 0;
    public float trackDistance = 5f;
    public float panSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerRig = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*offsetX = Mathf.Lerp(0, 3, moveCounter);

        if(playerRig.velocity.x > 0.3f) {
            moveCounter += Time.deltaTime;
            if(moveCounter > 1f) {
                moveCounter = 1;
            }
        } else if(playerRig.velocity.x < -0.3f) {
            if(moveCounter < 1f) {
                moveCounter = 1;
            }
            moveCounter += Time.deltaTime;
            offsetX *= -1;
        } else {
            moveCounter -= Time.deltaTime;
            if(moveCounter < 0) {
                moveCounter = 0;
            }
        }*/

        if(Mathf.Abs(target.transform.position.y - transform.position.y) > trackDistance) {
            if(transform.position.y < target.transform.position.y) {
                offsetY += Time.deltaTime * panSpeed;
            } else {
                offsetY -= Time.deltaTime * panSpeed;
            }
        } else {
            offsetY = 0;
        }

        transform.position = new Vector3(target.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
    }
}
