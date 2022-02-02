using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    public float timeToDestroy;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < timeToDestroy) {
            timer += Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
}
