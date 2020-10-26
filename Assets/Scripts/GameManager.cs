using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State { Title, lvlSelect, Grid, Game, Pause, Fail, Clear};

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public int currentLevel;
    public State currentState;
    public State previousState;
    State prePauseState;
    public static LevelManager levelManager;

    public GameObject trebuchetPrefab;
    public GameObject playerPrefab;
    public GameObject groundPrefab;
    public GameObject gridPrefab;
    public GameObject blockPrefab;

    public GameObject regularShot;
    public GameObject fireShot;
    public GameObject iceShot;
    public GameObject bombShot;
    public GameObject spikeShot;

    public GameObject dustAnim;
    public GameObject shotAnim;


    GameObject bm;

    GameObject lvlSelectUI;
    GameObject gridUI;
    GameObject gameUI;
    GameObject pauseUI;
    GameObject failUI;
    GameObject clearUI;
    public GameObject levelObjects;
    public GameObject groundContainer;

    public GameObject timerObject;
    public TimerManager tm;

    public GameObject currentBlock;
    private bool cbAlive = false;

    List<GameObject> levelSelectButtons;

    private AudioManagerScript audioManager;

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
        currentLevel = 0;
        currentState = State.lvlSelect;
        previousState = State.lvlSelect;
        levelManager = LevelManager.Instance;

        lvlSelectUI = GameObject.Find("LevelSelectUI");
        gridUI = GameObject.Find("GridUI");
        gameUI = GameObject.Find("LevelUI");
        pauseUI = GameObject.Find("PauseUI");
        failUI = GameObject.Find("FailUI");
        clearUI = GameObject.Find("ClearUI");
        levelObjects = GameObject.Find("LevelObjects");
        groundContainer = GameObject.Find("GroundContainer");

        gridUI.SetActive(false);
        gameUI.SetActive(false);
        pauseUI.SetActive(false);
        failUI.SetActive(false);
        clearUI.SetActive(false);

        bm = GameObject.Find("BlockManager");
        bm.GetComponent<BlockManager>().DeactivateGridPaint();

        tm = timerObject.GetComponent<TimerManager>();

        if (levelSelectButtons == null) levelSelectButtons = new List<GameObject>();
        if(levelManager.levels.Count == 0) levelManager.InitLevels();
        
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        if (audioManager != null)
        {
            audioManager.setTitleBGM();
            audioManager.playCurrentBGM();
        }
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
                break;

            case State.Grid:
                //player is building
                if (tm.getDone())
                    StartGame();
                break;

            case State.Game:
                //Game is active
                if (tm.getDone())
                    ChangeGameState(State.Clear);
                break;

            case State.Pause:
                //Game is paused
                break;

            case State.Clear:
                //Level has been cleared
                break;

            case State.Fail:
                //Level has been failed
                break;

            default:
                //title screen
                break;
        }
        previousState = currentState;
    }

    void OnDestroy() {
        Debug.Log("Destroyed gm");
        if (this == _instance) { _instance = null; } 
    }

    //Changes the current state to Game and adds physics to the blocks
    public void StartGame()
    {
        ChangeGameState(State.Game);
        bm.GetComponent<BlockManager>().Play();
    }

    public void StartLevel(int value)
    {
        levelManager = LevelManager.Instance;
        levelManager.CleanUpLevel();
        levelManager.setCurrentLevel(value);
        ChangeGameState(State.Grid);
        bm.GetComponent<BlockManager>().StartCode();
        tm.setTimer(60); //Resets Timer
        currentLevel = value; //Update current level variable
    }

    public void ChangeGameState(State state)
    {
        prePauseState = currentState;
        currentState = state;
        switch (currentState)
        {
            case State.Title:
                break;
            case State.lvlSelect:
                lvlSelectUI.SetActive(true);
                gridUI.SetActive(false);
                gameUI.SetActive(false);
                pauseUI.SetActive(false);
                failUI.SetActive(false);
                clearUI.SetActive(false);
                levelObjects.SetActive(false);
                levelManager.cancelLevelLoop();
                levelManager.HideGrid();

                bm.GetComponent<BlockManager>().DeactivateGridPaint();

                Destroy(currentBlock);
                cbAlive = false;

                if (audioManager != null) //Updates Background Music
                {
                    audioManager.setTitleBGM();
                    audioManager.playCurrentBGM();
                }
                break;
            case State.Grid:
                gridUI.SetActive(true);
                lvlSelectUI.SetActive(false);
                gameUI.SetActive(true);
                pauseUI.SetActive(false);
                failUI.SetActive(false);
                clearUI.SetActive(false);
                levelObjects.SetActive(true);
                levelManager.cancelLevelLoop();
                levelManager.DisplayGrid();

                if (previousState != State.Pause) //Prevents timer reseting after pausing
                    tm.setTimer(60);

                bm.GetComponent<BlockManager>().ActivateGridPaint();
                if (audioManager != null) //Updates Background Music
                {
                    audioManager.setLevelBGM();
                    audioManager.playCurrentBGM();
                }
                break;
            case State.Game:
                gameUI.SetActive(true);
                lvlSelectUI.SetActive(false);
                gridUI.SetActive(false);
                pauseUI.SetActive(false);
                failUI.SetActive(false);
                clearUI.SetActive(false);
                levelObjects.SetActive(true);
                levelManager.startLevelLoop();
                levelManager.HideGrid();

                if (previousState != State.Pause) //Prevents timer reseting after pausing
                    tm.setTimer(30);

                bm.GetComponent<BlockManager>().DeactivateGridPaint();

                Destroy(currentBlock);
                cbAlive = false;

                if (audioManager != null) //Updates Background Music
                {
                    audioManager.setFiringBGM();
                    audioManager.playCurrentBGM();
                }
                break;
            case State.Pause:
                pauseUI.SetActive(true);
                lvlSelectUI.SetActive(false);
                gridUI.SetActive(false);
                gameUI.SetActive(false);
                failUI.SetActive(false);
                clearUI.SetActive(false);
                levelObjects.SetActive(false);
                levelManager.cancelLevelLoop();
                levelManager.HideGrid();

                bm.GetComponent<BlockManager>().DeactivateGridPaint();

                Destroy(currentBlock);
                cbAlive = false;
                if (audioManager != null) //Stops Background Music
                {
                    audioManager.stopBGM();
                }

                break;
            case State.Clear:
                pauseUI.SetActive(false);
                lvlSelectUI.SetActive(false);
                gridUI.SetActive(false);
                gameUI.SetActive(false);
                failUI.SetActive(false);
                clearUI.SetActive(true);
                levelObjects.SetActive(false);
                levelManager.cancelLevelLoop();
                levelManager.HideGrid();

                bm.GetComponent<BlockManager>().DeactivateGridPaint();

                Destroy(currentBlock);
                cbAlive = false;
                if (audioManager != null) //Stops Background Music
                {
                    audioManager.stopBGM();
                    audioManager.playWinClip();
                }

                break;
            case State.Fail:
                pauseUI.SetActive(false);
                lvlSelectUI.SetActive(false);
                gridUI.SetActive(false);
                gameUI.SetActive(false);
                failUI.SetActive(true);
                clearUI.SetActive(false);
                levelObjects.SetActive(false);
                levelManager.cancelLevelLoop();
                levelManager.HideGrid();

                bm.GetComponent<BlockManager>().DeactivateGridPaint();

                Destroy(currentBlock);
                cbAlive = false;
                if (audioManager != null) //Stops Background Music
                {
                    audioManager.stopBGM();
                    audioManager.playLossClip();
                }

                break;
        }
        /*switch (previousState)
        {
            case State.Title:
                break;
            case State.lvlSelect:
                
                break;
            case State.Grid:
                Destroy(currentBlock);
                break;
            case State.Game:
                
                break;
            case State.Pause:
                
                break;
        }*/
    }

    public void ButtonGameState(int state)
    {
        switch (state)
        {
            case 0:
                ChangeGameState(State.Title);
                break;
            case 1:
                ChangeGameState(State.lvlSelect);
                break;
            case 2:
                ChangeGameState(State.Grid);
                break;
            case 3:
                ChangeGameState(State.Game);
                break;
            case 4:
                ChangeGameState(State.Pause);
                break;
        }
    }

    public void ButtonLevelSelect()
    {
        ChangeGameState(State.lvlSelect);
        foreach (Transform child in levelObjects.transform.GetChild(2))
        {
            Destroy(child.gameObject);
        }
        levelManager.CleanUpLevel();

    }

    public void ButtonRestart()
    {
        foreach (Transform child in levelObjects.transform.GetChild(2))
        {
            Destroy(child.gameObject);
        }

        StartLevel(currentLevel);
    }

    public void ButtonResume()
    {
        if (prePauseState == State.lvlSelect)
        {
            ChangeGameState(State.lvlSelect);
        }
        else if (prePauseState == State.Game)
        {
            ChangeGameState(State.Game);
        }
        else
        {
            ChangeGameState(State.Grid);
        }
    }

    Vector3 GetMouseInput()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return worldPos;
    }

    void PlaceBlock()
    {
        Vector3 loc = GetMouseInput();
        Vector3 gridLoc = new Vector3((int)loc.x, (int)loc.y, 0.0f);
        if (!cbAlive)
        {
            currentBlock = GameObject.Instantiate(blockPrefab, gridLoc, Quaternion.identity);
            currentBlock.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            levelManager.activeObjects.Add(currentBlock);
            cbAlive = !cbAlive;
        }

        currentBlock.transform.position = gridLoc;

        if (Input.GetMouseButtonDown(1))
        {
            currentBlock.transform.parent = levelObjects.transform;
            cbAlive = !cbAlive;
        }
    }

    public void EndLevel(bool success)
    {
        if (success)
        {
            //won level
            ChangeGameState(State.Clear);
        }
        else
        {
            //failed
            ChangeGameState(State.Fail);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

public enum SHOT_TYPE { NONE, STANDARD, FIRE, BOMB, ICE, SPIKE };
public enum MAT_TYPE { NONE, WOOD, GLASS, STONE, STEEL, MAGIC };