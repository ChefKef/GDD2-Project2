using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetManager : MonoBehaviour
{
    //Public variables
    GameObject projectile; //The projectile that the trebuchet can launch. Will later be an array of different types of projectiles.

    //Private variables
    private GameObject projectileHolder; //Holds the projectile to run calculations on it after creation.
    private Rigidbody2D projectileRigidbody; //Holds the projectile's rigidbody.

    private Vector2 defaultTarget;
    private float defaultUpwardVal;
    private SHOT_TYPE defaultBoulderType;

    Animator anim;

    static GameManager gm;
    private GameObject drawnObjects;

    // Start is called before the first frame update
    void Start()
    {
        //Debug boulder
        //LaunchBoulder(new Vector2(0.0f, 0.0f), 20.0f, 0);
        anim = gameObject.GetComponent<Animator>();
        drawnObjects = GameObject.Find("DrawnObjects");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /// <summary>
    /// Changes where the automatic firing will target.
    /// </summary>
    /// <param name="target">The target the boulder will be launched at.</param>
    /// <param name="upwardVel">The initial upward velocity of the boulder.</param>
    /// <param name="boulderType">The type of boulder being launched. Currently not in use.</param>
    public void updateTarget(Vector2 target, float upwardVel, SHOT_TYPE boulderType)
    {
        defaultTarget = target;
        defaultUpwardVal = upwardVel;
        defaultBoulderType = boulderType;

        switch (defaultBoulderType)
        {
            case SHOT_TYPE.STANDARD:
                projectile = GameManager.Instance.regularShot;
                break;
            case SHOT_TYPE.ICE:
                projectile = GameManager.Instance.iceShot;
                break;
            case SHOT_TYPE.FIRE:
                projectile = GameManager.Instance.fireShot;
                break;
            case SHOT_TYPE.BOMB:
                projectile = GameManager.Instance.bombShot;
                break;
            case SHOT_TYPE.SPIKE:
                projectile = GameManager.Instance.spikeShot;
                break;
            default:
                projectile = GameManager.Instance.regularShot;
                break;
        }

        Debug.Log(defaultUpwardVal);
    }

    /// <summary>
    /// Creates and launches a boulder at a target.
    /// </summary>
    /// <param name="target">The target the boulder will be launched at.</param>
    /// <param name="upwardVel">The initial upward velocity of the boulder.</param>
    /// <param name="boulderType">The type of boulder being launched. Currently not in use.</param>
    IEnumerator LaunchBoulder(Vector2 target, float upwardVel, SHOT_TYPE boulderType)
    {
        yield return new WaitForSeconds(0.5f);

        projectileHolder = (GameObject)Instantiate(projectile, new Vector3(gameObject.transform.position.x + .5f, gameObject.transform.position.y + .22f, gameObject.transform.position.z - .001f), Quaternion.identity); //Create projectile
        projectileHolder.transform.parent = drawnObjects.transform;
        projectileHolder.GetComponent<RockManager>().shot = boulderType;
        projectileRigidbody = projectileHolder.GetComponent<Rigidbody2D>();
        //First, use vertex formula (v = (-initialVel)/(gravity[9.8])) to get time of apex of launch. Then multiply that by 2 for launch time, then apply an x velocity of 
        //(distance between launch point x and target x)/(time of arc). If I'm calculating this correctly, this will even work when objects aren't lined up.
        float airTime = ((-upwardVel) / (-9.8f)) * 2;
        float xForce = (target.x - projectileHolder.transform.position.x) / airTime;
        projectileRigidbody.AddForce(new Vector2(xForce, upwardVel), ForceMode2D.Impulse);
        Debug.Log("X force: " + xForce);
        Debug.Log("Upward velocity: " + upwardVel);
        Debug.Log("Air time: " + airTime);
        StartCoroutine(DestroyOldProjectiles(false, 5.0f, projectileHolder));
    }

    /// <summary>
    /// Launch a boulder at the default positions, given a random variance
    /// </summary>
    /// <param name="variance">A value between -0.5 and 0.5. Slightly changes where the trebuchet fires.</param>
    public void LaunchDefaultBoulder(float variance)
    {
        anim.SetTrigger("TrebuchetTrigger");

        StartCoroutine(LaunchBoulder(defaultTarget, defaultUpwardVal + variance, defaultBoulderType));
    }

    IEnumerator DestroyOldProjectiles(bool status, float delayTime, GameObject toDestroy)
    {
        yield return new WaitForSeconds(delayTime);
        // Now do your thing here
        Destroy(toDestroy);
    }
}
