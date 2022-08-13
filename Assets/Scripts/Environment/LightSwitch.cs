using UnityEngine;
using Items.Logic;

public class LightSwitch : MonoBehaviour, IClickable
{
    [SerializeField] private Light[] connectedLights;

    [SerializeField] private Transform rotationAxis;
    [SerializeField] private float xRotation = 6.5f;

    [SerializeField] private bool isEnabled = true;
    public void OnClick()
    {
        if (!isEnabled)
        {
            foreach (Light light in connectedLights)
            {
                light.enabled = true;
                isEnabled = true;
                rotationAxis.Rotate(xRotation, 0f, 0f);
            }
        }
        else
        {
            foreach (Light light in connectedLights)
            {
                light.enabled = false;
                isEnabled = false;
                rotationAxis.Rotate(-xRotation, 0f, 0f);
            }
        }
    }
}
