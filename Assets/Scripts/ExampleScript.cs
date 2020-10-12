using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= - 5 || transform.position.y >= 5 || transform.position.x <= -11 || transform.position.x >= 11)
        {
            Destroy(gameObject);
        }
    }
}
