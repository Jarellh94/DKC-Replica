using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Transform spawnPoint;

    public int extraLives;
    public GameObject hurtIndicator; 

    public GameMenus gameMenu;

    public float invincibilityTime = 4f;

    private float invinTimer = 0;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -4) {
            Die();
        }

        if(invinTimer > 0) {
            invinTimer -= Time.deltaTime;
            if(Mathf.RoundToInt(invinTimer) % 2 == 1) {
                hurtIndicator.SetActive(true);
            } else {
                hurtIndicator.SetActive(false);
            }
        }
    }

    public void Die() {
        gameMenu.Died();
        gameObject.SetActive(false);
    }

    public void Damage() {
        //If has another player, spawn other player.
        //The way we'll do it is just by switching the mesh to the new player.
        if(invinTimer <= 0) {
            if(extraLives > 0) {
                invinTimer = invincibilityTime;
            } else {
                Instantiate(deathEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                Die();
            }
        }
    }

    public void SetDeathEffect(GameObject effect) {
        deathEffect = effect;
    }
}
