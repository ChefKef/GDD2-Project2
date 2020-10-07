using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Material
{
    //constructor for the material, this is the base which won't likely be called
    public Magic()
    {
        //initiallizes things as unimportant values
        cost = 30;
        durability = 0.01f;
        forceTransfer = 0.0f;
        type = MAT_TYPE.MAGIC;
        weakness = SHOT_TYPE.NONE;
        resistance = SHOT_TYPE.NONE;
    }
}
