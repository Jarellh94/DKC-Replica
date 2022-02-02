using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameMenus : MonoBehaviour
{
    GameManager manager;
    public PlayerScore playerScore;
    public GameObject pauseMenu;
    public GameObject loseScreen;
    public GameObject winScreen;

    public GameObject resumeButton;
    public GameObject playAgainButton;
    public GameObject winPlayAgainButton;
    public TextMeshProUGUI loseCoinText;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI winCoinText;

    bool paused = false;

    public AudioClip winClip;
    public AudioClip loseClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7))) {
           Pause();
        } else if(paused && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton1) )) {
            ResumeGame();
        }
    }

    public void Pause() {
        paused = true;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
        manager.Pause();
    }

    public void ResumeGame() {
        paused = false;
        manager.UnPause();
        pauseMenu.SetActive(false);
    }

    public void QuitGame() {
        manager.QuitGame();
    }

    public void Died() {
        loseScreen.SetActive(true);
        audioSource.PlayOneShot(loseClip);
        manager.LowerMusicVolume();
        int coinNum = playerScore.GetCoins();
        loseCoinText.text = "Coins: " + coinNum + "/" + 175; //Max coin num, make this accurate!
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playAgainButton);
    }

    public void Won() {
        int coinNum = playerScore.GetCoins();
        audioSource.PlayOneShot(winClip);
        manager.LowerMusicVolume();
        winCoinText.text = "Coins: " + coinNum + "/" + 175; //Max coin num, make this accurate!
        winTimeText.text = playerScore.GetTime();
        winScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(winPlayAgainButton);
    }

    public void RestartGame() {
        manager.LoadScene(1);
    }
}
