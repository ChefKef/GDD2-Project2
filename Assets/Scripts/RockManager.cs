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
    public SHOT_TYPE shot;
    public Sprite[] shotSprites;
    //Private variables
    private bool collided = false;
    void Start()
    {
        switch(shot)
        {
            default:
            case SHOT_TYPE.STANDARD:
                GetComponent<SpriteRenderer>().sprite = shotSprites[0];
                break;
            case SHOT_TYPE.FIRE:
                GetComponent<SpriteRenderer>().sprite = shotSprites[1];
                break;
            case SHOT_TYPE.BOMB:
                GetComponent<SpriteRenderer>().sprite = shotSprites[2];
                break;
            case SHOT_TYPE.ICE:
                GetComponent<SpriteRenderer>().sprite = shotSprites[3];
                break;
            case SHOT_TYPE.SPIKE:
                GetComponent<SpriteRenderer>().sprite = shotSprites[4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided == false && (collision.gameObject.tag == "destructible" || collision.gameObject.tag == "childBlock")) //Ensure that collision is only calculated once.
        {
            collision.gameObject.GetComponentInParent<Material>().CalcDamage(forceConstant * GetComponent<Rigidbody2D>().velocity.x, shot);
            /*Code that calls a function on the object that can take damage. I would like to add a 'destructible' flag on all structures that can be deleted in order to not cause errors.
             The takeDamage() function will need to be made for walls/target orbs. If you come up with something that doesn't use the takeDamage() signature, that is fine.*/
            collided = true;
            Destroy(gameObject);
        }
    }
}