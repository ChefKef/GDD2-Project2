using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Material
{
    //constructor for the material, this is the base which won't likely be called
    public Wood()
    {
        //initiallizes things as unimportant values
        cost = 2;
        durability = 5f;
        durabilityMax = 5f;
        forceTransfer = 0.75f;
        type = MAT_TYPE.WOOD;
        weakness = SHOT_TYPE.FIRE;
        resistance = SHOT_TYPE.SPIKE;
    }
}
