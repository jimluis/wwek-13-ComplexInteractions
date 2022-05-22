using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeeButtonColor : MonoBehaviour
{
    MeshRenderer m_meshRenderer = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor()
    {
        m_meshRenderer.material.SetColor("_Color", Random.ColorHSV(0, 1, 0.9f, 1, 0.9f, 1.0f));
    }

    // Update is called once per frame
    public void LogInteractionStarted()
    {
        Debug.Log("Interaction Started");
    }

    public void LogInteractionEnded()
    {
        Debug.Log("Interaction Ended");
    }
}
