using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Camera gameCam;

    public GameObject TestBlockPublic;
    private GameObject testBlockPrivate;

    // Start is called before the first frame update
    void Start()
    {
        Grid grid = new Grid(21, 9, 1f);
        testBlockPrivate = TestBlockPublic;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        
        if(Input.GetMouseButtonDown(0))
        {
            if (hit.collider == null)
            {
                Debug.Log(hit);
                GameObject instanceBlock;
                instanceBlock = Instantiate(testBlockPrivate, gameCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                instanceBlock.transform.position = new Vector3(Mathf.Round(instanceBlock.transform.position.x), Mathf.Round(instanceBlock.transform.position.y), 0);
            }
            if (hit.collider != null && hit.collider.gameObject.tag == "block")
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
