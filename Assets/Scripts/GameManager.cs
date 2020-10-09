using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum State { Title, lvlSelect, Grid, Game, Pause};

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public int currentLevel;
    State currentState;
    public static LevelManager levelManager;

    public GameObject trebuchetPrefab;
    public GameObject playerPrefab;

    List<GameObject> levelSelectButtons;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
        currentState = State.lvlSelect;
        levelManager = LevelManager.Instance;
        if(levelSelectButtons == null) levelSelectButtons = new List<GameObject>();
        if(levelManager.levels.Count == 0) levelManager.InitLevels();
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

    void OnDestroy() { if (this == _instance) { _instance = null; } }

    void DisplayLevelSelect()
    {

        
    }

    void LoadLevels()
    {

        
    }

    public void StartLevel(int value)
    {
        levelManager = LevelManager.Instance;
        levelManager.CleanUpLevel();
        levelManager.setCurrentLevel(value);     
    }
}
