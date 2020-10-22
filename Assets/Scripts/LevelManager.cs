using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelObject> levels = new List<LevelObject>();

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public int currentLevel;
    public List<GameObject> activeObjects;

    int gridWidth;
    int gridHeight;
    int gridCellSize;
    GameObject[,] gridSquares;
    GameObject gridContainer;

    public GameObject gmPrefabBackup;

    public GameObject gridPrefab;

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
        currentLevel = -1;
        gridWidth = 20;
        gridHeight = 11;
        gridCellSize = 1;
        gridContainer = GameObject.Find("GridContainer");

        gridSquares = new GameObject[gridHeight, gridWidth];
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                GameObject temp = Instantiate(gridPrefab, new Vector3(j - (gridWidth / 2), (i - (gridHeight / 2) + 1), 0.0f), Quaternion.identity);
                temp.transform.parent = gridContainer.transform;
                
                gridSquares[i,j] = temp;
            }
        }

        gridContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() { if (this == _instance) { _instance = null; } }

    public void InitLevels()
    {
        LevelObject level1 = new LevelObject();
        level1.AddTrebuchet(new Vector2(15, -1.2f), new Vector2(2, -4), 8, SHOT_TYPE.STANDARD, false);
        level1.BuildGround(new Vector3(-20, -5.5f, 5f), new Vector3(20, -5.5f, 5f));
        level1.BuildGround(new Vector3(-20, -7.5f, 10f), new Vector3(20, -7.5f, 10f));
        level1.BuildGround(new Vector3(-20, -9.5f, 10f), new Vector3(20, -9.5f, 10f));
        level1.levelID = 1;
        level1.playerPos = new Vector2(0, -4);
        levels.Add(level1);

        LevelObject level2 = new LevelObject();
        level2.AddTrebuchet(new Vector2(15, -1.2f), new Vector2(3, -4), 7, SHOT_TYPE.ICE, false);
        level2.AddTrebuchet(new Vector2(-15, -1.2f), new Vector2(-1, -4), 12, SHOT_TYPE.FIRE, true);
        level2.BuildGround(new Vector3(-20, -5.5f, 5f), new Vector3(20, -5.5f, 5f));
        level2.BuildGround(new Vector3(-20, -7.5f, 10f), new Vector3(20, -7.5f, 10f));
        level2.BuildGround(new Vector3(-20, -9.5f, 10f), new Vector3(20, -9.5f, 10f));
        level2.levelID = 2;
        level2.playerPos = new Vector2(0, -4);
        levels.Add(level2);
    }

    public void setCurrentLevel(int levelID)
    {
        levels[levelID].SetActiveLevel();
        activeObjects = levels[levelID].InitLevel();
    }

    public void startLevelLoop()
    {
        InvokeRepeating("levelLoop", 3f, 4f);
    }

    public void cancelLevelLoop()
    {
        CancelInvoke();
    }

    public void levelLoop()
    {
        //LevelObject currentLevelObject = levels[currentLevel].GetComponent<currentLevelObject>();
        for (int i = 0; i < activeObjects.Count; i++)
        {
            if (i == 0)
            {
                //player
                continue;
            }
            else if(activeObjects[i] == null)
            {
                //ground
                break;
            }

            activeObjects[i].GetComponent<TrebuchetManager>().LaunchDefaultBoulder(Random.Range(-0.5f,0.5f));
        }
    }

    public void CleanUpLevel()
    {
        if(activeObjects != null)
        {
            for (int i = 0; i < activeObjects.Count; i++)
            {
                Destroy(activeObjects[i]);
            }
            activeObjects.Clear();
        }
        
    }

    public void DisplayGrid()
    {
        gridContainer.SetActive(true);
    }

    public void HideGrid()
    {
        gridContainer.SetActive(false);
    }
}
