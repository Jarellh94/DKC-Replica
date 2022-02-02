using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;

    public float moveDistance = 10f;
    public float moveSpeed = 10f;

    public Transform groundCheck;
    public float checkRadius = .5f;

    protected bool isGroundAhead = true;

    protected Vector3 startingPoint;

    public GameObject deathEffect;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckForGround();
        Move();
    }

    protected virtual void Move() {
        if(isGroundAhead) {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        } else {
            transform.Rotate(new Vector3(0, 180, 0), Space.Self);
        }
    }

    protected void CheckForGround() {
        LayerMask groundMask = LayerMask.GetMask("Ground");

        if(Physics2D.OverlapBox(groundCheck.position, new Vector2(checkRadius, checkRadius / 2), 0, groundMask)){
            isGroundAhead = true;
        } else {
            isGroundAhead = false;
        }
    }

    public void Damage() {
        health--;
        if(health <= 0) {
            Die();
        }
    }

    private void Die() {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = isGroundAhead ? Color.green : Color.red;
        Gizmos.DrawCube(groundCheck.position, new Vector2(checkRadius, checkRadius / 2));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if(player) {
            if(player.GetComponent<PlayerMovement>().IsRolling()) {
                Damage();
            } else if((Mathf.Abs(transform.position.x - player.transform.position.x) <= 1f && player.transform.position.y > transform.position.y + .75f)) {
                Damage();
                player.gameObject.GetComponent<PlayerMovement>().Bounce();
            } else {
                player.Damage();
            }
        }
    }
}

