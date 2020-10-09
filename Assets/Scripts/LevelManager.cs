using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<LevelObject> levels;

    private int currentLevel;
    private List<GameObject> activeObjects;
    // Start is called before the first frame update
    void Start()
    {
        levels = new List<LevelObject>();

        currentLevel = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLevel != -1)
        {
            InvokeRepeating("levelLoop", 1f, 1f);
        }
    }

    public void InitLevels()
    {
        LevelObject level1 = new LevelObject();
        level1.AddTrebuchet(new Vector2(-5, 0), new Vector2(0, 0), 5, 0);
        level1.levelID = 1;
        level1.playerPos = new Vector2(0, 0);
        levels.Add(level1);

        LevelObject level2 = new LevelObject();
        level2.AddTrebuchet(new Vector2(-5, 0), new Vector2(0, 0), 5, 0);
        level2.levelID = 1;
        level2.playerPos = new Vector2(-1, 0);
        levels.Add(level2);
    }

    void setCurrentLevel(int levelID)
    {
        levels[levelID].GetComponent<LevelObject>().SetActiveLevel();
        activeObjects = levels[levelID].GetComponent<LevelObject>().InitLevel();
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
}
