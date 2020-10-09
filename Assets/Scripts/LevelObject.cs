using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public int levelID;
    public GameObject trebuchetPrefab;
    public GameObject playerPrefab;
    public List<Vector2> trebuchetPos;
    public List<Vector2> trebuchetTarget;
    public List<float> upwardVels;
    public List<int> boulderType;
    public Vector2 playerPos;

    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;

        trebuchetPos = new List<Vector2>();
        trebuchetTarget = new List<Vector2>();
        upwardVels = new List<float>();
        boulderType = new List<int>();
        //get player and trebuchet prefabs
        trebuchetPrefab = GameObject.Find("GameManager").GetComponent<GameManager>().trebuchetPrefab;
        playerPrefab = GameObject.Find("GameManager").GetComponent<GameManager>().playerPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddTrebuchet(Vector2 pos, Vector2 target, float upwardForce, int type)
    {
        trebuchetPos.Add(pos);
        trebuchetTarget.Add(target);
        upwardVels.Add(upwardForce);
        boulderType.Add(type);
    }

    public List<GameObject> InitLevel()
    {
        List<GameObject> activeObjects = new List<GameObject>();
        activeObjects.Add(Instantiate(playerPrefab, playerPos, Quaternion.identity));
        for(int i = 0; i < trebuchetPos.Count; i++)
        {
            GameObject treb = Instantiate(trebuchetPrefab, trebuchetPos[i], Quaternion.identity);
            treb.GetComponent<TrebuchetManager>().updateTarget(trebuchetTarget[i], upwardVels[i], boulderType[i]);
            activeObjects.Add(treb);
        }
        return activeObjects;
    }

    public void SetActiveLevel()
    {
        active = !active;
    }
}
