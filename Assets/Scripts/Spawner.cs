using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            SpawnObject();
        }
    }

    private void SpawnObject() {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(transform.position, new Vector2(1, 1));
    }
}
