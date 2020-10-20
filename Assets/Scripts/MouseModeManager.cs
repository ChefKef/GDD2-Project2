using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseModeManager : MonoBehaviour
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

    public void setMode(int editorState)
    {
        switch(editorState)
        {
            case 0:
                value.GetComponent<UnityEngine.UI.Text>().text = "painting";
                break;
            case 1:
                value.GetComponent<UnityEngine.UI.Text>().text = "deleting";
                break;
            case 2:
                value.GetComponent<UnityEngine.UI.Text>().text = "connecting";
                break;
            default:
                value.GetComponent<UnityEngine.UI.Text>().text = "error";
                break;
        }
        
    }
}

