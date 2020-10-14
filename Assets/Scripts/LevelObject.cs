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
        activeObjects.Add(GameObject.Instantiate(playerPrefab, playerPos, Quaternion.identity));
        for(int i = 0; i < trebuchetPos.Count; i++)
        {
            GameObject treb = GameObject.Instantiate(trebuchetPrefab, trebuchetPos[i], Quaternion.identity);
            treb.GetComponent<TrebuchetManager>().updateTarget(trebuchetTarget[i], upwardVels[i], boulderType[i]);
            activeObjects.Add(treb);
        }
        return activeObjects;
    }

    public void SetActiveLevel()
    {
        active = !active;
    }

    public void BuildGround(vector2 startPoint, vector2 endPoint)
    {

    }
}
