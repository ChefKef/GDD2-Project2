using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    private AudioManagerScript audioManager;
    
    void Start()
    {
        health = 10.0f;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float damage)
    {
        //Play animation to signal damage
        Instantiate(GameManager.Instance.dustAnim, gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity, GameObject.Find("DrawnObjects").transform);

        health -= damage;

        if (audioManager != null)
            audioManager.playBlockHitClip();

        if (health <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.EndLevel(false);
        Destroy(this.gameObject);
    }
}
