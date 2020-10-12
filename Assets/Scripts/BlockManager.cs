using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Camera gameCam;

    public GameObject TestBlockPublic;
    private GameObject testBlockPrivate;

    private List<GameObject> blockList;

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


        currentState = EditorState.Painting;
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
            if (hit.collider != null && hit.collider.gameObject.tag == "block" && currentState == EditorState.Deleting)
            {
                blockList.Remove(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
            }
        } 
    }

    void ConnectorMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        GameObject[] children = new GameObject[5];

        if (Input.GetMouseButtonDown(0))
        {
            //code for merging blocks
            if (hit.collider != null && hit.collider.gameObject.tag == "block" && currentState == EditorState.Connecting)
            {
                /*
                 * Code in here for merging blocks
                 */

                //Make array of blocks, max 5
                
                if(children[0] = null)
                {
                    children[0] = hit.collider.gameObject;
                    blockList.Remove(hit.collider.gameObject);
                    Debug.Log(children);
                }
                else
                {
                    //adjacency check, if adkacent, add to array
                    for(int i = 0; i < 5; i++)
                    {
                        if(children[i] != null)
                        {

                        }
                        else
                        {
                            Debug.Log(children[i]);
                            children[i] = hit.collider.gameObject;
                            blockList.Remove(hit.collider.gameObject);
                            Debug.Log(children);
                            i = 5;
                        }
                    }
                }
                //add clicked block to array
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //merge into parent and shit
            GameObject parentObject = new GameObject();
            //for (int i = 0; i < 5; i++)
            //{
            //    if (children[i].tag == "block")
            //    {
            //        children[i].transform.parent = parentObject.transform;
            //    }
            //    else
            //    {
            //
            //    }
            //
            //}

            foreach(GameObject g in children)
            {
                g.transform.parent = parentObject.transform;
            }

            blockList.Add(parentObject);

            Debug.Log("Parent has been made");
        }
    }
}
