using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{

    public Transform startPosition = null;
    public Transform endPosition = null;

    MeshRenderer meshRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void OnSlideStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateSlider(float percentage)
    {
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percentage);
    }
}
