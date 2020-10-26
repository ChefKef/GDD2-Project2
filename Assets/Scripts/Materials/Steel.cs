using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steel : Material
{
    //constructor for the material, this is the base which won't likely be called
    public Steel()
    {
        //initiallizes things as unimportant values
        cost = 7;
        durability = 15f;
        durabilityMax = 15f;
        forceTransfer = 0.25f;
        type = MAT_TYPE.STEEL;
        weakness = SHOT_TYPE.SPIKE;
        resistance = SHOT_TYPE.FIRE;
    }
}
