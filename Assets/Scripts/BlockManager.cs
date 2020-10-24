using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Camera gameCam;

    public GameObject TestBlockPublic;

    public GameObject Material;
    public GameObject Mouse;
    public GameObject Components;

    private GameObject testBlockPrivate;

    //a good way to do corners and stuff may be to split this into multiple lists, one for each material, where each index represents a particular edge/corner.
    public List<Sprite> sprites;

    public List<GameObject> blockList;

    private List<GameObject> debugList;

    private MAT_TYPE currentMaterial;

    private MAT_TYPE connectorMaterial;

    private int remainingComponents;

    private enum EditorState
    {
        Painting,
        Deleting,
        Connecting,
        Play
    }

    private EditorState currentState;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        StartCode();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && currentState != EditorState.Play)
            GridModeActive();
    }

    public void GridModeActive()
    {
        Cleanup();

        if (currentState == EditorState.Painting)
        {
            PainterMode();
        }
        else if (currentState == EditorState.Deleting)
        {
            DeleterMode();
        }
        else if (currentState == EditorState.Connecting)
        {
            ConnectorMode();
        }

        //checks if anything is being done now
        if (Input.anyKey)
        {
            //input checking for changing the drawing mode.
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentState = EditorState.Painting;
                CancelConnecting();
                Debug.Log("Now Painting");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentState = EditorState.Connecting;
                Debug.Log("Now Connecting");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentState = EditorState.Deleting;
                CancelConnecting();
                Debug.Log("Now Deleting");
            }
            Mouse.GetComponent<MouseModeManager>().setMode((int)currentState);

            //input checking for changing the material. (Temporarily number buttons, could be permanent as a secondary option to clicking
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentMaterial = MAT_TYPE.WOOD;
                Debug.Log("Wood");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentMaterial = MAT_TYPE.GLASS;
                Debug.Log("Glass");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentMaterial = MAT_TYPE.STONE;
                Debug.Log("Stone");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentMaterial = MAT_TYPE.STEEL;
                Debug.Log("Brick");
            }
            Material.GetComponent<MaterialLabelManager>().setMaterial(currentMaterial);
            /*if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentMaterial = MAT_TYPE.MAGIC;
                Debug.Log("Magic");
            }*/
        }
    }

    public void ActivateGridPaint()
    {
        //InvokeRepeating("GridModeActive", 0.01f, 0.01f);
        paused = false;
    }

    public void DeactivateGridPaint()
    {
        //CancelInvoke();
        paused = true;
        LevelManager.Instance.activeObjects.AddRange(blockList);
        //instanceBlock.transform.parent = GameObject.Find("LevelObjects").transform;
    }

    void PainterMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (Input.GetMouseButton(0))
        {
            if (hit.collider == null)
            {
                bool intersecting = false;

                //Debug.Log(hit);
                GameObject instanceBlock;
                instanceBlock = Instantiate(testBlockPrivate, gameCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                instanceBlock.transform.position = new Vector3(Mathf.Round(instanceBlock.transform.position.x), Mathf.Round(instanceBlock.transform.position.y), 0);
                instanceBlock.transform.parent = GameManager.Instance.levelObjects.transform.GetChild(2);
                instanceBlock.tag = "destructible";

                //adds the script for whichever material is selected
                switch (currentMaterial)
                {
                    case MAT_TYPE.WOOD:
                        instanceBlock.AddComponent<Wood>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[0];
                        break;
                    case MAT_TYPE.GLASS:
                        instanceBlock.AddComponent<Glass>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[1];
                        break;
                    case MAT_TYPE.STONE:
                        instanceBlock.AddComponent<Stone>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[2];
                        break;
                    case MAT_TYPE.STEEL:
                        instanceBlock.AddComponent<Steel>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[3];
                        break;
                    case MAT_TYPE.MAGIC:
                        instanceBlock.AddComponent<Magic>();

                        //TEMPORARY PART - CHANGES COLOR, REPLACE WITH ACTUAL SPRITE PICKING
                        instanceBlock.GetComponent<SpriteRenderer>().color = new Color(.2f, 1, 0);
                        break;
                }

                //dupe checking - don't think we need since the raycast checks if the collider is null
                //foreach (GameObject g in blockList)
                //{
                //    if (g.transform.position == instanceBlock.transform.position)
                //    {
                //        intersecting = true;
                //    }
                //}

                //if (!intersecting)
                //{
                    blockList.Add(instanceBlock);

                //boundary checking
                if (instanceBlock.transform.position.y <= -5 || instanceBlock.transform.position.y >= 5 || instanceBlock.transform.position.x <= -11 || instanceBlock.transform.position.x >= 11)
                {
                    intersecting = true;
                }

                //cost checking
                if (instanceBlock.GetComponent<Material>().Cost > remainingComponents)
                {
                    intersecting = true;
                }
                //}

                //final check, adds it to active or destroys it
                if(intersecting)
                {
                    blockList.Remove(instanceBlock);
                    Destroy(instanceBlock);
                }
                else
                {
                    //adds it
                    LevelManager.Instance.activeObjects.Add(instanceBlock);
                    //update components
                    remainingComponents -= instanceBlock.GetComponent<Material>().Cost;
                    Components.GetComponent<CostLabelManager>().setNumber(remainingComponents);
                }
            }
        }
    }

    void DeleterMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if(Input.GetMouseButton(0))
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "destructible")
            {
                //edits cost
                remainingComponents += hit.collider.gameObject.GetComponent<Material>().Cost;
                //deletes it
                blockList.Remove(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
            }
            //similar code but loops through all its siblings as well
            if (hit.collider != null && hit.collider.gameObject.tag == "childBlock")
            {
                //gets the parent
                GameObject parent = hit.collider.gameObject.transform.parent.gameObject;
                //increases the components
                int cost = parent.GetComponent<Material>().Cost;
                remainingComponents += cost * parent.transform.childCount;
                //deletes
                blockList.Remove(parent);
                Destroy(parent);
            }
            Components.GetComponent<CostLabelManager>().setNumber(remainingComponents);
        }
    }

    void ConnectorMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            //code for merging blocks
            if (hit.collider != null && hit.collider.gameObject.tag == "destructible")
            {
                if(debugList.Count == 0)
                {
                    debugList.Add(hit.collider.gameObject);
                    blockList.Remove(hit.collider.gameObject);
                    Debug.Log(debugList);
                    hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    hit.collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, -0.1f);

                    //sets the type of this set of connected objects, checks this before adding it to the group
                    connectorMaterial = hit.collider.gameObject.GetComponent<Material>().Type;
                }
                else if (debugList.Count < 5 && debugList.Count > 0 && connectorMaterial == hit.collider.gameObject.GetComponent<Material>().Type)
                {
                    for (int i = 0; i < debugList.Count; i++)
                    {
                        float dist = Vector3.Distance(debugList[i].transform.position, hit.collider.gameObject.transform.position);
                        Debug.Log(dist);
                        if (dist <= 1.2f)
                        {
                            debugList.Add(hit.collider.gameObject);
                            blockList.Remove(hit.collider.gameObject);
                            Debug.Log("Adjacent");
                            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                            hit.collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                            hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, -0.1f);
                            break;
                        }
                        Debug.Log("Adjacency was checked");
                    }
                }
                else if(debugList.Count == 5)
                {

                }
            }
        }

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(3) || Input.GetMouseButtonDown(4))
        {
            if(debugList == null)
            {
                Debug.Log("The group is empty");
            }
            else if(debugList.Count > 0)
            {
                //merge into parent and shit
                GameObject parentObject = new GameObject();
                parentObject.name = "block group";
                parentObject.transform.parent = GameManager.Instance.levelObjects.transform.GetChild(2);

                List<GameObject> childList = new List<GameObject>();
                childList = debugList;
                debugList = new List<GameObject>();

                //sets the parent's material up
                switch (connectorMaterial)
                {
                    case MAT_TYPE.WOOD:
                        parentObject.AddComponent<Wood>();
                        break;
                    case MAT_TYPE.GLASS:
                        parentObject.AddComponent<Glass>();
                        break;
                    case MAT_TYPE.STONE:
                        parentObject.AddComponent<Stone>();
                        break;
                    case MAT_TYPE.STEEL:
                        parentObject.AddComponent<Steel>();
                        break;
                    case MAT_TYPE.MAGIC:
                        parentObject.AddComponent<Magic>();
                        break;
                }

                foreach (GameObject g in childList)
                {
                    g.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
                    g.transform.parent = parentObject.transform;
                    Destroy(g.GetComponent<Material>());
                    Debug.Log("Child has been connected");
                    g.tag = "childBlock";
                }

                parentObject.tag = "destructible";

                blockList.Add(parentObject);

                Debug.Log("Parent has been made");
            }
        }
    }


    void CancelConnecting()
    {
        foreach(GameObject g in debugList)
        {
            blockList.Add(g);
            g.transform.GetChild(0).gameObject.SetActive(false);
            g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0);
        }
        debugList = new List<GameObject>();
    }

    //deletes any null blocks
    void Cleanup()
    {
        for (int i = 0; i < blockList.Count; i++) 
        {
            if (blockList[i] == null)
            {
                blockList.RemoveAt(i);
                i--;
            }
        }
    }

    public void Play()
    {
        CancelConnecting();
        foreach(GameObject g in blockList)
        {
            g.AddComponent<Rigidbody2D>();
        }
        currentState = EditorState.Play;
    }

    public void StartCode()
    {
        //Grid grid = new Grid(21, 9, 1f);
        testBlockPrivate = TestBlockPublic;
        blockList = new List<GameObject>();

        debugList = new List<GameObject>();

        currentState = EditorState.Painting;

        Mouse.GetComponent<MouseModeManager>().setMode((int)currentState);

        currentMaterial = MAT_TYPE.WOOD;

        Material.GetComponent<MaterialLabelManager>().setMaterial(currentMaterial);

        remainingComponents = 50;

        Components.GetComponent<CostLabelManager>().setNumber(remainingComponents);
    }
}