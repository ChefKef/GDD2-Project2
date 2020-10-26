using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostLabelManager : MonoBehaviour
{
    private GameObject value;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setNumber(int remainingComponents)
    {
        if (!value)
        {
            value = gameObject.GetComponentInParent<Transform>().Find("Value").gameObject;
        }

        value.GetComponent<UnityEngine.UI.Text>().text = remainingComponents.ToString() + ".:.";
    }
}
