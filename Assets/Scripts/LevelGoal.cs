using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public GameMenus gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerMovement>()){
            other.GetComponent<PlayerMovement>().Won();
            gameMenu.Won();
        }
    }
}
