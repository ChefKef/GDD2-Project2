using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    //Public variables
    public float forceConstant;
    /*The force constant exists so that all objects do not need to be given masses. If I wanted to simulate physically correct collisions, all objects would need to have mass so that I could
     properly calculate. That's needlessly complex, so instead, the force constant will simply multiply with the velocity of the boulder on impact. This variable is public, so it can be changed
    by other scripts when needed.*/
    //Private variables
    private bool collided = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided == false && collision.gameObject.tag == "destructible") //Ensure that collision is only calculated once.
        {
            //collision.gameObject.takeDamage(forceConstant * GetComponent<Rigidbody2D>().velocity.x);
            /*Code that calls a function on the object that can take damage. I would like to add a 'destructible' flag on all structures that can be deleted in order to not cause errors.
             The takeDamage() function will need to be made for walls/target orbs. If you come up with something that doesn't use the takeDamage() signature, that is fine.*/
            collided = true;
        }
    }
}