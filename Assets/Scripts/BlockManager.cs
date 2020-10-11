using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Camera gameCam;

    public GameObject TestBlockPublic;
    private GameObject testBlockPrivate;

    private enum EditorState
    {
        Painting,
        Deleting
    }

    private EditorState currentState;

    // Start is called before the first frame update
    void Start()
    {
        Grid grid = new Grid(21, 9, 1f);
        testBlockPrivate = TestBlockPublic;

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
                //Debug.Log(hit);
                GameObject instanceBlock;
                instanceBlock = Instantiate(testBlockPrivate, gameCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                instanceBlock.transform.position = new Vector3(Mathf.Round(instanceBlock.transform.position.x), Mathf.Round(instanceBlock.transform.position.y), 0);
            }
            else if (hit.collider != null && hit.collider.gameObject.tag == "block" && currentState == EditorState.Deleting)
            {
                Destroy(hit.collider.gameObject);
            }
        }

        //right click input code
        else if (Input.GetMouseButton(1))
        {
            if (hit.collider == null)
            {
                //Debug.Log("nothing happened");
            }
            if (hit.collider != null && hit.collider.gameObject.tag == "block")
            {
                //Debug.Log("hit a block");
            }

            Debug.Log("held");
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
    }
}
