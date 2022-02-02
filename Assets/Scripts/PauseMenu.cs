using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    GameManager manager;
    public GameObject pauseMenu;

    public GameObject resumeButton;

    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
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
}
