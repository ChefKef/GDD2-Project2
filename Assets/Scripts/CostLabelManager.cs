using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostLabelManager : MonoBehaviour
{
    private GameObject value;

    // Start is called before the first frame update
    void Start()
    {
        value = GetComponentInParent<Transform>().Find("Value").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setNumber(int remainingComponents)
    {
        value.GetComponent<UnityEngine.UI.Text>().text = remainingComponents.ToString();
    }
}
