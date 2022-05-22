using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{

    public Transform startPosition = null;
    public Transform endPosition = null;
    [SerializeField] Light light;

    MeshRenderer meshRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public void OnSlideStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void OnSlideStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateSlider(float percent)
    {
        Debug.Log("UpdateSlider() - before percent: " + percent);
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percent);
        Debug.Log("UpdateSlider() - percent: " + percent + " - transform.position: " + transform.position);

        light.intensity = percent * 10;
    }
}
