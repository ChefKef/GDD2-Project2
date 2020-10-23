using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Material
{
    //constructor for the material, this is the base which won't likely be called
    public Glass()
    {
        //initiallizes things as unimportant values
        cost = 3;
        durability = 8f;
        forceTransfer = 0.0f;
        type = MAT_TYPE.GLASS;
        weakness = SHOT_TYPE.BOMB;
        resistance = SHOT_TYPE.ICE;
    }
}
