using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum State { Title, lvlSelect, Grid, Game, Pause};
    public int currentLevel;
    State currentState;
    public LevelManager levelManager;

    public GameObject trebuchetPrefab;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
        currentState = State.lvlSelect;
        levelManager = GameObject.Find("LevelManagerObj").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Title:
                //Display title screen, probably scene switch
                break;
            case State.lvlSelect:
                //Display level select, probably scene switch eventually, rn is all done in one
                DisplayLevelSelect();
                break;
            case State.Grid:
                //player is building

                break;
            case State.Game:
                //Game is active

                break;
            case State.Pause:
                //Game is paused

                break;
            default:
                //title screen
                break;
        }
    }

    void DisplayLevelSelect()
    {

    }
}
