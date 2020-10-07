using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steel : Material
{
    //constructor for the material, this is the base which won't likely be called
    public Steel()
    {
        //initiallizes things as unimportant values
        cost = 10;
        durability = 25f;
        forceTransfer = 0.75f;
        type = MAT_TYPE.STEEL;
        weakness = SHOT_TYPE.SPIKE;
        resistance = SHOT_TYPE.FIRE;
    }
}
