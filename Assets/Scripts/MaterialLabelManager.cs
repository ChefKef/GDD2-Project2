using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialLabelManager : MonoBehaviour
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

    public void setMaterial(MAT_TYPE material)
    {
        switch (material)
        {
            case MAT_TYPE.WOOD:
                value.GetComponent<UnityEngine.UI.Text>().text = "wood (3.:.)";
                break;
            case MAT_TYPE.GLASS:
                value.GetComponent<UnityEngine.UI.Text>().text = "glass (3.:.)";
                break;
            case MAT_TYPE.STONE:
                value.GetComponent<UnityEngine.UI.Text>().text = "stone (5.:.)";
                break;
            case MAT_TYPE.STEEL:
                value.GetComponent<UnityEngine.UI.Text>().text = "brick (8.:.)";
                break;
            default:
                value.GetComponent<UnityEngine.UI.Text>().text = "error";
                break;
        }

    }
}
