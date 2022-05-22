using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonLight : MonoBehaviour
{

    Light streetLight = null;

    // Start is called before the first frame update
    public void Awake()
    {
        streetLight = gameObject.GetComponent<Light>();
    }

    public void LightSwitch()
    {
        if (streetLight.enabled)
        {
            streetLight.enabled = false;
            Debug.Log("LightSwitch turning on: "+ streetLight.enabled);
        }

        else{
            streetLight.enabled = true;
            Debug.Log("LightSwitch turning off: " + streetLight.enabled);
        }

    }

}
