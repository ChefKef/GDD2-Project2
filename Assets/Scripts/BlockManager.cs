using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Camera gameCam;

    public GameObject TestBlockPublic;
    private GameObject testBlockPrivate;

    //a good way to do corners and stuff may be to split this into multiple lists, one for each material, where each index represents a particular edge/corner.
    public List<Sprite> sprites;

    private List<GameObject> blockList;

    private List<GameObject> debugList;

    private MAT_TYPE currentMaterial;

    private MAT_TYPE connectorMaterial;

    private enum EditorState
    {
        Painting,
        Deleting,
        Connecting
    }

    private EditorState currentState;

    // Start is called before the first frame update
    void Start()
    {
        Grid grid = new Grid(21, 9, 1f);
        testBlockPrivate = TestBlockPublic;
        blockList = new List<GameObject>();

        debugList = new List<GameObject>();

        currentState = EditorState.Painting;

        currentMaterial = MAT_TYPE.WOOD;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == EditorState.Painting)
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
            //right click input code
            if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(3) || Input.GetMouseButtonDown(4))
            {
                if (currentState == EditorState.Painting)
                {
                    Debug.Log("Now Deleting");
                    currentState = EditorState.Deleting;
                }
                else if (currentState == EditorState.Deleting)
                {
                    Debug.Log("Now Connecting");
                    currentState = EditorState.Connecting;
                }
                else if (currentState == EditorState.Connecting)
                {
                    Debug.Log("Now Painting");
                    currentState = EditorState.Painting;
                }

            }

            //input checking for changing the material. (Temporarily number buttons, could be permanent as a secondary option to clicking
            if(Input.GetKeyDown(KeyCode.Alpha1))
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
                Debug.Log("Steel");
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentMaterial = MAT_TYPE.MAGIC;
                Debug.Log("Magic");
            }
        }
    }

    void PainterMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (Input.GetMouseButton(0))
        {
            if (hit.collider == null && currentState == EditorState.Painting)
            {
                bool intersecting = false;

                //Debug.Log(hit);
                GameObject instanceBlock;
                instanceBlock = Instantiate(testBlockPrivate, gameCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                instanceBlock.transform.position = new Vector3(Mathf.Round(instanceBlock.transform.position.x), Mathf.Round(instanceBlock.transform.position.y), 0);
                instanceBlock.tag = "destructible";

                //adds the script for whaichever material is selected
                switch (currentMaterial)
                {
                    case MAT_TYPE.WOOD:
                        instanceBlock.AddComponent<Wood>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[0];
                        break;
                    case MAT_TYPE.GLASS:
                        instanceBlock.AddComponent<Glass>();

                        //TEMPORARY PART - CHANGES COLOR, REPLACE WITH ACTUAL SPRITE PICKING
                        instanceBlock.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.7f, 1);
                        break;
                    case MAT_TYPE.STONE:
                        instanceBlock.AddComponent<Stone>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[1];
                        break;
                    case MAT_TYPE.STEEL:
                        instanceBlock.AddComponent<Steel>();

                        instanceBlock.GetComponent<SpriteRenderer>().sprite = sprites[2];
                        break;
                    case MAT_TYPE.MAGIC:
                        instanceBlock.AddComponent<Magic>();

                        //TEMPORARY PART - CHANGES COLOR, REPLACE WITH ACTUAL SPRITE PICKING
                        instanceBlock.GetComponent<SpriteRenderer>().color = new Color(.2f, 1, 0);
                        break;
                }

                foreach (GameObject g in blockList)
                {
                    if (g.transform.position == instanceBlock.transform.position)
                    {
                        Destroy(instanceBlock);
                        intersecting = true;
                    }
                }

                if (!intersecting)
                {
                    blockList.Add(instanceBlock);

                    if (instanceBlock.transform.position.y <= -5 || instanceBlock.transform.position.y >= 5 || instanceBlock.transform.position.x <= -11 || instanceBlock.transform.position.x >= 11)
                    {
                        blockList.Remove(instanceBlock);
                    }
                }

            }
        }
        //right click input code
        else if (Input.GetMouseButtonDown(1) && currentState == EditorState.Painting)
        {
            //Apply rigid bodies to all blocks and remove them from the list so its not done another time
            while (blockList.Count > 0)
            {
                blockList[0].AddComponent<Rigidbody2D>();
                blockList.RemoveAt(0);
            }
        }
    }

    void DeleterMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if(Input.GetMouseButton(0))
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "destructible" && currentState == EditorState.Deleting)
            {
                blockList.Remove(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
            }
        } 
    }

    void ConnectorMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            //code for merging blocks
            if (hit.collider != null && hit.collider.gameObject.tag == "destructible" && currentState == EditorState.Connecting)
            {
                if(debugList.Count == 0)
                {
                    debugList.Add(hit.collider.gameObject);
                    blockList.Remove(hit.collider.gameObject);
                    Debug.Log(debugList);

                    //sets the type of this set of connected objects, checks this before adding it to the group
                    connectorMaterial = hit.collider.gameObject.GetComponent<Material>().Type;
                }
                else if (debugList.Count < 5 && debugList.Count > 0 && connectorMaterial == hit.collider.gameObject.GetComponent<Material>().Type)
                {
                    for (int i = 0; i < debugList.Count; i++)
                    {
                        float dist = Vector3.Distance(debugList[i].transform.position, hit.collider.gameObject.transform.position);
                        Debug.Log(dist);
                        if (dist <= 1)
                        {
                            debugList.Add(hit.collider.gameObject);
                            blockList.Remove(hit.collider.gameObject);
                            Debug.Log("Adjacent");
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
}