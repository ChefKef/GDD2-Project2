using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawSpriteGrid : MonoBehaviour
{
    Tilemap tilemap; //Tilemap containing the block grid

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>(); //Sets the tilemap reference
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++) //Loop in x-direction
        {
            for (int j = 0; j < 10; j++) //Loop in y-direction
            {
                //For now, check for mouse down in each tile 
                //and make sprite red if colliding
                //Otherwise, sprite is white
            }
        }
    }
}
