using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseModeManager : MonoBehaviour
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

    public void setMode(EditorState editorState)
    {
        if (!value)
        {
            value = gameObject.GetComponentInParent<Transform>().Find("Value").gameObject;
        }

        switch (editorState)
        {
            case EditorState.Painting:
                value.GetComponent<UnityEngine.UI.Text>().text = "painting";
                break;
            case EditorState.Deleting:
                value.GetComponent<UnityEngine.UI.Text>().text = "deleting";
                break;
            case EditorState.Connecting:
                value.GetComponent<UnityEngine.UI.Text>().text = "connecting";
                break;
            case EditorState.Play:
                value.GetComponent<UnityEngine.UI.Text>().text = "playing";
                break;
            default:
                value.GetComponent<UnityEngine.UI.Text>().text = "error";
                break;
        }
        
    }
}

