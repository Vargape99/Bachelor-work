using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLight : MonoBehaviour
{
    [SerializeField]
    Renderer LeftPart;
    [SerializeField]
    Renderer RightPart;

    void SetOpen() {
        LeftPart.material.SetColor("_EmissionColor", Color.green * 2);
        RightPart.material.SetColor("_EmissionColor", Color.green * 2);
    }

    void SetClose()
    {
        LeftPart.material.SetColor("_EmissionColor", Color.red * 2);
        RightPart.material.SetColor("_EmissionColor", Color.red * 2);
    }
}
