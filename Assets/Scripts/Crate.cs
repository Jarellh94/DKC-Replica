using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private bool wasThrown = false;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUp() {
        rig.isKinematic = true;
    }

    public void Throw(Vector2 force) {
        wasThrown = true;
        rig.isKinematic = false;
        rig.AddForce(force, ForceMode2D.Impulse);
    }

    private void Break() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //If it's not the player, break.
        if(wasThrown) {
            if(other.gameObject.GetComponent<Enemy>()) {
                other.gameObject.GetComponent<Enemy>().Damage();
            }

            if(other.gameObject.GetComponent<PlayerMovement>() == null) {
                Break();
            }
        }
    }
}
