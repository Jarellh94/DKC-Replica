using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int value;
    public float rotateSpeed;

    public GameObject collectEffect;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.Self);
    }

    public void Collected() {
        Instantiate(collectEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
