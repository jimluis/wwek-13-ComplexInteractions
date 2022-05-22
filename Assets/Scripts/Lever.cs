using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Transform startOrientation = null;
    public Transform endOrientation = null;
    [SerializeField] Light light;

    MeshRenderer meshRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    public void OnLeverStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void OnLeverStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateLever(float percent)
    {
        transform.rotation = Quaternion.Slerp(startOrientation.rotation, endOrientation.rotation, percent);
        Debug.Log("UpdateSlider() - percent: " + percent + " - transform.position: " + transform.position);

        light.intensity = percent * 10;

    }
}
