using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Camera gameCam;

    public GameObject TestBlockPublic;
    private GameObject testBlockPrivate;

    private List<GameObject> BlockList;

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
        BlockList = new List<GameObject>();


        currentState = EditorState.Painting;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        //Left click input code
        if(Input.GetMouseButton(0))
        {
            if (hit.collider == null && currentState == EditorState.Painting)
            {
                bool intersecting = false;

                //Debug.Log(hit);
                GameObject instanceBlock;
                instanceBlock = Instantiate(testBlockPrivate, gameCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                instanceBlock.transform.position = new Vector3(Mathf.Round(instanceBlock.transform.position.x), Mathf.Round(instanceBlock.transform.position.y), 0);

                foreach(GameObject g in BlockList)
                {
                    if (g.transform.position == instanceBlock.transform.position)
                    {
                        Destroy(instanceBlock);
                        intersecting = true;
                    }
                }

                if(!intersecting)
                {
                    BlockList.Add(instanceBlock);
                }
                
            }
            else if (hit.collider != null && hit.collider.gameObject.tag == "block" && currentState == EditorState.Deleting)
            {
                BlockList.Remove(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
            }
        }
        //right click input code
        else if (Input.GetMouseButtonDown(1))
        {
            //Apply rigid bodies to all blocks and remove them from the list so its not done another time
            while(BlockList.Count > 0)
            {
                BlockList[0].AddComponent<Rigidbody2D>();
                BlockList.RemoveAt(0);
            }
        }

        //right click input code
        else if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(3) || Input.GetMouseButtonDown(4))
        {
            if (currentState == EditorState.Painting)
            {
                Debug.Log("Now Deleting");
                currentState = EditorState.Deleting;
            }
            else if (currentState == EditorState.Deleting)
            {
                Debug.Log("Now Painting");
                currentState = EditorState.Painting;
            }
        }

        //for (int i = 0; i < BlockList.Count - 1; i++)
        //{
        //    for(int j = 1; j < BlockList.Count; j++)
        //    {
        //        if(BlockList[i].transform.position == BlockList[j].transform.position)
        //        {
        //            Destroy(BlockList[j]);
        //            BlockList.RemoveAt(j);
        //        }
        //    }
        //}
    }
}
