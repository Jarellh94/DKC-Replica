using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject leaderBoard;
    GameManager manager;

    public GameObject startButton;
    bool leaderBoardActive = false;

    public List<GameObject> toToggleOff;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        manager.LoadScene(1);
    }

    public void ToggleLeaderBoard() {
        leaderBoardActive = !leaderBoardActive;
        leaderBoard.SetActive(leaderBoardActive);
        foreach (GameObject item in toToggleOff)
        {
            item.SetActive(!leaderBoardActive);
        }
    }

    public void SwitchCharacter(int dir) {

    }
}
