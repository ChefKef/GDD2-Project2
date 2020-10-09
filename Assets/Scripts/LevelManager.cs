using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelObject> levels = new List<LevelObject>();

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public int currentLevel;
    private List<GameObject> activeObjects;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() { if (this == _instance) { _instance = null; } }

    public void InitLevels()
    {
        LevelObject level1 = new LevelObject();
        level1.AddTrebuchet(new Vector2(5, 0), new Vector2(0, 0), 5, 0);
        level1.levelID = 1;
        level1.playerPos = new Vector2(0, 0);
        levels.Add(level1);

        LevelObject level2 = new LevelObject();
        level2.AddTrebuchet(new Vector2(5, 0), new Vector2(0, 0), 5, 0);
        level2.AddTrebuchet(new Vector2(7, 0), new Vector2(0, 0), 7, 0);
        level2.levelID = 1;
        level2.playerPos = new Vector2(-1, 0);
        levels.Add(level2);
    }

    public void setCurrentLevel(int levelID)
    {
        levels[levelID].SetActiveLevel();
        activeObjects = levels[levelID].InitLevel();
        InvokeRepeating("levelLoop", 3f, 4f);
    }

    void levelLoop()
    {
        //LevelObject currentLevelObject = levels[currentLevel].GetComponent<currentLevelObject>();
        for (int i = 0; i < activeObjects.Count; i++)
        {
            if (i == 0)
            {
                //player
                continue;
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
}
