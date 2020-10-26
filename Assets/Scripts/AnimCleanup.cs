using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCleanup : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            //anim.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !anim.IsInTransition(0))
                Destroy(gameObject);
        }
    }
}
