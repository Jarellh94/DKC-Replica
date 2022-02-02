using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    int coinNum;
    float timer = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Coins: " + coinNum;
        timer += Time.deltaTime;
        int minutes = (int) timer / 60;
        int seconds = (int) timer - 60 * minutes;
        int milliseconds = (int) (1000 * (timer - minutes * 60 - seconds));
        timerText.text = "Timer: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
        //timerText.text = "Timer: " + timer;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Collectable coll = other.GetComponent<Collectable>();
        if(coll) {
            coinNum++;
            //Instantiate Coin Graphic
            coll.Collected();
        }
    }

    public int GetCoins() {
        return coinNum;
    }

    public string GetTime() {
        return timerText.text;
    }
}
