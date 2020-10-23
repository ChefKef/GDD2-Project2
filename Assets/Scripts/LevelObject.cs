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
    public List<SHOT_TYPE> boulderType = new List<SHOT_TYPE>();
    public List<Vector3> groundLocations = new List<Vector3>();
    public List<bool> flipTreb = new List<bool>();
    public Vector2 playerPos;

    private bool active = false;

    public void Awake()
    {
        Debug.Log("GM Instance: " + GameManager.Instance);
    }

    public void AddTrebuchet(Vector2 pos, Vector2 target, float upwardForce, SHOT_TYPE shot, bool flip)
    {
        this.trebuchetPos.Add(pos);
        trebuchetTarget.Add(target);
        upwardVels.Add(upwardForce);
        boulderType.Add(shot);
        flipTreb.Add(flip);
    }

    public List<GameObject> InitLevel()
    {
        if (gm == null)
        {
            Debug.Log("recreating gm");
            gm = GameManager.Instance;
        }

        List<GameObject> activeObjects = new List<GameObject>();
        trebuchetPrefab = gm.trebuchetPrefab;
        playerPrefab = gm.playerPrefab;
        GameObject groundTile = gm.groundPrefab;
        GameObject player = GameObject.Instantiate(playerPrefab, playerPos, Quaternion.identity);

        player.transform.parent = gm.levelObjects.transform;
        activeObjects.Add(player);

        for(int i = 0; i < trebuchetPos.Count; i++)
        {
            GameObject treb = GameObject.Instantiate(trebuchetPrefab, trebuchetPos[i], Quaternion.identity);
            treb.GetComponent<TrebuchetManager>().updateTarget(trebuchetTarget[i], upwardVels[i], (SHOT_TYPE)boulderType[i]);
            treb.transform.parent = gm.levelObjects.transform;
            treb.transform.position = new Vector3(treb.transform.position.x, treb.transform.position.y, -8);
            if (flipTreb[i])
                treb.transform.localScale = new Vector2(-treb.transform.localScale.x, treb.transform.localScale.y);
            activeObjects.Add(treb);
        }

        activeObjects.Add(null);

        for(int i = 1; i < groundLocations.Count; i++)
        {
            Vector2 location = groundLocations[i];
            GameObject ground = GameObject.Instantiate(groundTile, location, Quaternion.identity);
            ground.transform.parent = gm.groundContainer.transform;
            //ground.transform.position = new Vector3(ground.transform.position.x, ground.transform.position.y, 2 + i);
            activeObjects.Add(ground);
        }

        return activeObjects;
    }

    public void SetActiveLevel()
    {
        active = !active;
    }

    public void BuildGround(Vector3 startPoint, Vector3 endPoint)
    {      
        int distance = Mathf.Abs((int)(startPoint.x - endPoint.x));
        for(int i = 0; i < distance; i+=2)
        {           
            groundLocations.Add(new Vector3(startPoint.x + i, startPoint.y, startPoint.z));
        }
    }
}
