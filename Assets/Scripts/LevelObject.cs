using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject
{
    public int levelID;
    static GameManager gm = GameManager.Instance;
    public GameObject trebuchetPrefab = gm.trebuchetPrefab;
    public GameObject playerPrefab = gm.playerPrefab;
    public List<Vector2> trebuchetPos = new List<Vector2>();
    public List<Vector2> trebuchetTarget = new List<Vector2>();
    public List<float> upwardVels = new List<float>();
    public List<int> boulderType = new List<int>();
    public List<float> groundLocations = new List<float>();
    public Vector2 playerPos;

    private bool active = false;

    public void AddTrebuchet(Vector2 pos, Vector2 target, float upwardForce, int type)
    {
        this.trebuchetPos.Add(pos);
        trebuchetTarget.Add(target);
        upwardVels.Add(upwardForce);
        boulderType.Add(type);
    }

    public List<GameObject> InitLevel()
    {
        List<GameObject> activeObjects = new List<GameObject>();
        trebuchetPrefab = gm.trebuchetPrefab;
        playerPrefab = gm.playerPrefab;
        GameObject groundTile = gm.groundPrefab;
        activeObjects.Add(GameObject.Instantiate(playerPrefab, playerPos, Quaternion.identity));
        for(int i = 0; i < trebuchetPos.Count; i++)
        {
            GameObject treb = GameObject.Instantiate(trebuchetPrefab, trebuchetPos[i], Quaternion.identity);
            treb.GetComponent<TrebuchetManager>().updateTarget(trebuchetTarget[i], upwardVels[i], boulderType[i]);
            activeObjects.Add(treb);
        }

        activeObjects.Add(null);

        for(int i = 1; i < groundLocations.Count; i++)
        {
            Vector2 location = new Vector2(groundLocations[i], groundLocations[0]);
            activeObjects.Add(GameObject.Instantiate(groundTile, location, Quaternion.identity));
        }

        return activeObjects;
    }

    public void SetActiveLevel()
    {
        active = !active;
    }

    public void BuildGround(Vector2 startPoint, Vector2 endPoint)
    {      
        int distance = Mathf.Abs((int)(startPoint.x - endPoint.x));
        groundLocations.Add(startPoint.y);
        for(int i = 0; i < distance; i++)
        {           
            groundLocations.Add(startPoint.x + i);
        }
    }
}
