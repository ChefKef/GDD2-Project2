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
    public static LevelManager levelManager;

    public GameObject trebuchetPrefab;
    public GameObject playerPrefab;
    public GameObject groundPrefab;
    public GameObject gridPrefab;
    public GameObject blockPrefab;

    public GameObject currentBlock;
    private bool cbAlive = false;

    public GridObject grid;

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
        previousState = State.lvlSelect;
        levelManager = LevelManager.Instance;
        if(levelSelectButtons == null) levelSelectButtons = new List<GameObject>();
        if(levelManager.levels.Count == 0) levelManager.InitLevels();

        grid = gridPrefab.GetComponent<GridObject>();
        grid.buildIndices(new Vector2(-10, -5), 20, 10, 1);
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
                PlaceBlock();
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
            cbAlive = !cbAlive;
        }

        currentBlock.transform.position = gridLoc;
    }
}

public enum SHOT_TYPE { NONE, STANDARD, FIRE, BOMB, ICE, SPIKE };
public enum MAT_TYPE { NONE, WOOD, GLASS, STONE, STEEL, MAGIC };