using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Material
{
    //constructor for the material, this is the base which won't likely be called
    public Stone()
    {
        //initiallizes things as unimportant values
        cost = 5;
        durability = 12f;
        forceTransfer = 0.5f;
        type = MAT_TYPE.STONE;
        weakness = SHOT_TYPE.ICE;
        resistance = SHOT_TYPE.BOMB;
    }
}
