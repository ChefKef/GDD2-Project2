using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    protected int cost;
    protected float durability;
    protected float forceTransfer;
    protected MAT_TYPE type;
    protected SHOT_TYPE weakness;
    protected SHOT_TYPE resistance;

    //everything colliding with this, for force transfer damage calculations. Resets every frame
    protected List<Material> neighbors;

    public MAT_TYPE Type
    {
        get { return type; }
    }

    //constructor for the material, this is the base which won't likely be called
    public Material()
    {
        //initiallizes things as unimportant values
        cost = 0;
        durability = 10f;
        forceTransfer = 0.0f;
        type = MAT_TYPE.NONE;
        weakness = SHOT_TYPE.NONE;
        resistance = SHOT_TYPE.NONE;
        neighbors = new List<Material>();
    }

    //deals damage based on a previously calculated amount (either when hit or by transferrence)
    public void DoDamage(float damage, SHOT_TYPE damageType)
    {
        if (damageType == weakness)
        {
            //double damage if it is weak to this attack
            durability -= (damage * 2);
        }
        else if (damageType == resistance) 
        {
            //halves if it resists it
            durability -= (damage / 2);
        }
        else
        {
            //regular damage otherwise
            durability -= damage;
        }

        if (durability <= 0.0f)
        {
            //destroy the object this is attatched to.
        }
    }

    //calculates the damage based on some factor of the shot that hit this block
    public void CalcDamage(float shotSpeed, SHOT_TYPE damageType)
    {
        //arbitrary for now until we get collisions functioning.
        float damage = shotSpeed / 50;
        DoDamage(damage, damageType);

        //as long as force can transfer, deals damage to the neigboring materials
        if (forceTransfer != 0)
        {
            damage = damage * forceTransfer;
            for (int i = 0; i < neighbors.Count; i++)
            {
                neighbors[i].DoDamage(damage, damageType);
            }
        }
    }
}