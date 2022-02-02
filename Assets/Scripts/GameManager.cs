using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> characterList;

    public enum GameState
    {
        MENU, PLAYING, PAUSED, SWITCHING_CHARACTERS
    }

    public GameState currState;

    private PlayerCharacterSwitch characterSwitch;

    GameManager instance;

    PauseMenu pauseMenu;

    public int charNum = 0;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = -1;
        GameManager otherInstance = FindObjectOfType<GameManager>();
        if(otherInstance && otherInstance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) { //In-Game State
            switch(currState) {
                case GameState.MENU:
                    break;
                case GameState.PLAYING:
                    break;
                case GameState.PAUSED:
                    break;
                case GameState.SWITCHING_CHARACTERS:

                    break;
            }
        }
    }

    void SwitchState(GameState state) {
        switch(currState) { //Stuff to do when switching from these
            case GameState.MENU:
                break;
            case GameState.PLAYING:
                break;
            case GameState.PAUSED:
                break;
            case GameState.SWITCHING_CHARACTERS:
                break;
        }

        switch(state) { //Stuff to do when switching to these
            case GameState.MENU:
                Time.timeScale = 1;
                break;
            case GameState.PLAYING:
                Time.timeScale = 1;
                break;
            case GameState.PAUSED:
            case GameState.SWITCHING_CHARACTERS:
                Time.timeScale = 0;
                break;
        }

        currState = state;
    }

    public GameState GetState() {
        return currState;
    }

    public void SetCurrentCharacter(int charNum) {
        this.charNum = charNum;
    }

    public int GetCharNum() {
        return charNum;
    }

    public void DoneSwitchingCharacters() {
        SwitchState(GameState.PLAYING);
    }

    public void SwitchingCharacters() {
        SwitchState(GameState.SWITCHING_CHARACTERS);
    }

    public void LoadScene(int scene) {
        if(scene == 1) {
            SwitchState(GameState.PLAYING);
        }
        RaiseMusicVolume();
        SceneManager.LoadScene(scene);
    }

    public void UnPause() {
        SwitchState(GameState.PLAYING);
        RaiseMusicVolume();
    }

    public void Pause() {
        SwitchState(GameState.PAUSED);
        LowerMusicVolume();
    }

    public void QuitGame() {
        SwitchState(GameState.MENU);
        LoadScene(0);
    }

    public void SetPauseMenu(PauseMenu menu) {
        pauseMenu = menu;
    }

    public void LowerMusicVolume() {
        audioSource.volume = .15f;
    }

    public void RaiseMusicVolume() {
        audioSource.volume = .75f;
    }
}
