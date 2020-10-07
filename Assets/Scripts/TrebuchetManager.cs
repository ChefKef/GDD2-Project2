using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetManager : MonoBehaviour
{
    //Public variables
    public Object projectile; //The projectile that the trebuchet can launch. Will later be an array of different types of projectiles.

    //Private variables
    private GameObject projectileHolder; //Holds the projectile to run calculations on it after creation.
    private Rigidbody2D projectileRigidbody; //Holds the projectile's rigidbody.
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive!");
        //Test
        LaunchBoulder(new Vector2(0.0f, 0.0f), 20.0f, 1);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /// <summary>
    /// Creates and launches a boulder at a target.
    /// </summary>
    /// <param name="target">The target the boulder will be launched at.</param>
    /// <param name="upwardVel">The initial upward velocity of the boulder.</param>
    /// <param name="boulderType">The type of boulder being launched. Currently not in use.</param>
    private void LaunchBoulder(Vector2 target, float upwardVel, int boulderType)
    {
        projectileHolder = (GameObject)Instantiate(projectile, new Vector3(gameObject.transform.position.x + .5f, gameObject.transform.position.y + .22f, gameObject.transform.position.z - .001f), Quaternion.identity); //Create projectile
        projectileRigidbody = projectileHolder.GetComponent<Rigidbody2D>();
        //First, use vertex formula (v = (-initialVel)/(gravity[9.8])) to get time of apex of launch. Then multiply that by 2 for launch time, then apply an x velocity of 
        //(distance between launch point x and target x)/(time of arc). If I'm calculating this correctly, this will even work when objects aren't lined up.
        float airTime = ((-upwardVel) / (-9.8f)) * 2;
        float xForce = (target.x - projectileHolder.transform.position.x) / airTime;
        projectileRigidbody.AddForce(new Vector2(xForce, upwardVel), ForceMode2D.Impulse);
        Debug.Log("X force: " + xForce);
        Debug.Log("Upward velocity: " + upwardVel);
        Debug.Log("Air time: " + airTime);
    }
}
