using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
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
