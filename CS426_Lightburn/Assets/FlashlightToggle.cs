using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    bool state = true;
    private Light flashlight;

    void Start()
    {
        flashlight = GetComponent<Light>();
    }

    void Update()
    {
        if (flashlight != null && Input.GetKeyDown(KeyCode.F)){
            state = !state;
            flashlight.enabled = state;
        }
    }
}
