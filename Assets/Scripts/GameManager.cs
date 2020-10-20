using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State { Title, lvlSelect, Grid, Game, Pause};

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

    GameObject bm;

    GameObject lvlSelectUI;
    GameObject gridUI;
    GameObject gameUI;
    GameObject pauseUI;
    public GameObject levelObjects;

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
        currentLevel = 1;
        currentState = State.lvlSelect;
        previousState = State.lvlSelect;
        levelManager = LevelManager.Instance;
        if(levelSelectButtons == null) levelSelectButtons = new List<GameObject>();
        if(levelManager.levels.Count == 0) levelManager.InitLevels();

        lvlSelectUI = GameObject.Find("LevelSelectUI");
        gridUI = GameObject.Find("GridUI");
        gameUI = GameObject.Find("LevelUI");
        pauseUI = GameObject.Find("PauseUI");
        levelObjects = GameObject.Find("LevelObjects");

        gridUI.SetActive(false);
        gameUI.SetActive(false);
        pauseUI.SetActive(false);

        bm = GameObject.Find("BlockManager");

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
        previousState = currentState;
    }

    void OnDestroy() { if (this == _instance) { _instance = null; } }

    public void StartLevel(int value)
    {
        levelManager = LevelManager.Instance;
        levelManager.CleanUpLevel();
        levelManager.setCurrentLevel(value);
        ChangeGameState(State.Grid);
    }

    void ChangeGameState(State state)
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
                levelObjects.SetActive(true);
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
                levelObjects.SetActive(true);
                levelManager.cancelLevelLoop();
                levelManager.DisplayGrid();

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
                levelObjects.SetActive(true);
                levelManager.startLevelLoop();
                levelManager.HideGrid();

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
        levelManager.CleanUpLevel();
    }

    public void ButtonRestart()
    {
        if(prePauseState == State.lvlSelect)
        {
            ChangeGameState(State.lvlSelect);
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
        }
        else
        {
            //failed
        }
    }
}

public enum SHOT_TYPE { NONE, STANDARD, FIRE, BOMB, ICE, SPIKE };
public enum MAT_TYPE { NONE, WOOD, GLASS, STONE, STEEL, MAGIC };