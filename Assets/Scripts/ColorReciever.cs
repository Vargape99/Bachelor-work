using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReciever : BeamInteractive
{
    private string wantedColor;
    [SerializeField]
    private Renderer eyes;
    [SerializeField]
    private bool recievingRightColor;
    [SerializeField]
    List<LightBeam> recievingBeams;

    private void Awake()
    {
        recievingBeams = new List<LightBeam>();
        if (wantedColor == "Black")
        {
            recievingRightColor = true;
        }
        else
        {
            recievingRightColor = false;
        }
        wantedColor = GetComponentInParent<SetColor>().GetMyColor();
        eyes.material.color = Colors.colors[wantedColor];
        eyes.material.SetColor("_EmissionColor", Colors.colors[wantedColor] * 0.1f);
        eyes.material.EnableKeyword("_EMISSION");
    }



    public override void BeamEnter(string beamColor ,GameObject other)
    {
            recievingBeams.Add(other.GetComponent<LightBeam>());
            ColorIsThere();
    }

    //check if Im receivin the right color
    private bool ColorIsThere() {
        foreach (LightBeam beam in recievingBeams) {
            if (beam.GetColour() == wantedColor) {
                eyes.material.SetColor("_EmissionColor", Colors.colors[wantedColor] * 5);
                recievingRightColor = true;
                return true;
            }
        }
        eyes.material.SetColor("_EmissionColor", Colors.colors[wantedColor] * 0.1f);
        recievingRightColor = false;
        return false;

    }



    public override void BeamExit(GameObject other)
    {
        recievingBeams.Remove(other.GetComponent<LightBeam>());
        ColorIsThere();
    }

    public override void BeamStay(GameObject beam)
    {
        return;
    }

    public bool IsRecievingRightColor() {
        return recievingRightColor;
    }
}
