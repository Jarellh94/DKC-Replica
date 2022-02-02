using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterSwitch : MonoBehaviour
{
    public int currCharacter = 0;
    public List<PlayerAnimator> characters;
    GameManager manager;

    bool hasSwitched = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        currCharacter = manager.GetCharNum();
        characters[currCharacter].gameObject.SetActive(true);

        if(manager.GetState() == GameManager.GameState.MENU) {
            characters[currCharacter].GameStarted();
            characters[currCharacter].Walk();
            characters[currCharacter].Run();
        } else {
            gameObject.GetComponent<PlayerMovement>().SetCharacter(currCharacter);
        }
    }

    private void OnEnable() {
        if(manager != null ) {
            if(manager.GetState() == GameManager.GameState.MENU) {
                characters[currCharacter].GameStarted();
                characters[currCharacter].Walk();
                characters[currCharacter].Run();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.GetState() == GameManager.GameState.MENU) {
            CheckForSwitch();
        }   
    }

    void CheckForSwitch() {
        float xAxis = Input.GetAxis("Horizontal");
        if(!hasSwitched) {
            if(xAxis > 0.1f) {
                hasSwitched = true;
                characters[currCharacter].gameObject.SetActive(false);
                if(currCharacter == characters.Count-1) {
                    currCharacter = 0;
                } else {
                    currCharacter++;
                }
                characters[currCharacter].gameObject.SetActive(true);
                characters[currCharacter].GameStarted();
                characters[currCharacter].Walk();
                characters[currCharacter].Run();
                manager.SetCurrentCharacter(currCharacter);
            } else if(xAxis < -0.1f) {
                hasSwitched = true;
                characters[currCharacter].gameObject.SetActive(false);
                if(currCharacter == 0) {
                    currCharacter = characters.Count-1;
                } else {
                    currCharacter--;
                }
                characters[currCharacter].gameObject.SetActive(true);
                characters[currCharacter].GameStarted();
                characters[currCharacter].Walk();
                characters[currCharacter].Run();
                manager.SetCurrentCharacter(currCharacter);
            }
        } else {
            if(xAxis < 0.1f && xAxis > -0.1f) {
                hasSwitched = false;
            }
        }
    }

    public void SwitchCharacters(int switchNum) {
        if(switchNum > 0) {
                hasSwitched = true;
                characters[currCharacter].gameObject.SetActive(false);
                if(currCharacter == characters.Count-1) {
                    currCharacter = 0;
                } else {
                    currCharacter++;
                }
                characters[currCharacter].gameObject.SetActive(true);
                characters[currCharacter].GameStarted();
                characters[currCharacter].Walk();
                characters[currCharacter].Run();
                manager.SetCurrentCharacter(currCharacter);
            } else if(switchNum < 0) {
                hasSwitched = true;
                characters[currCharacter].gameObject.SetActive(false);
                if(currCharacter == 0) {
                    currCharacter = characters.Count-1;
                } else {
                    currCharacter--;
                }
                characters[currCharacter].gameObject.SetActive(true);
                characters[currCharacter].GameStarted();
                characters[currCharacter].Walk();
                characters[currCharacter].Run();
                manager.SetCurrentCharacter(currCharacter);
            }
    }
}
