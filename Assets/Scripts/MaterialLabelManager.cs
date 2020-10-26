using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialLabelManager : MonoBehaviour
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

    public void setMaterial(MAT_TYPE material)
    {
        if (!value)
        {
            value = gameObject.GetComponentInParent<Transform>().Find("Value").gameObject;
        }

        switch (material)
        {
            case MAT_TYPE.WOOD:
                value.GetComponent<UnityEngine.UI.Text>().text = "wood (2.:.)";
                break;
            case MAT_TYPE.GLASS:
                value.GetComponent<UnityEngine.UI.Text>().text = "glass (3.:.)";
                break;
            case MAT_TYPE.STONE:
                value.GetComponent<UnityEngine.UI.Text>().text = "stone (5.:.)";
                break;
            case MAT_TYPE.STEEL:
                value.GetComponent<UnityEngine.UI.Text>().text = "brick (7.:.)";
                break;
            default:
                value.GetComponent<UnityEngine.UI.Text>().text = "error";
                break;
        }

    }
}
