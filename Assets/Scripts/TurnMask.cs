using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMask : BeamInteractive
{
    [SerializeField]
    GameObject recievingLightBeam;

    [SerializeField]
    List<LightBeam> newLightBeams;
    //ready for the possibility of making more beams from one 

    [SerializeField]
    Renderer gemstone;


    private void Start()
    {
        if (gemstone)
        {
            gemstone.material.SetColor("_EmissionColor", Color.black);
        }
    }


    public override void BeamStay(GameObject beam)
    {
        return;
    }

    public override void BeamEnter(string color, GameObject other)
    {
            if (recievingLightBeam == null) {
                recievingLightBeam = other.gameObject;
            foreach (LightBeam newLightBeam in newLightBeams) {
                newLightBeam.SetColour(color);
                newLightBeam.SetLightBeamOn();
                if (gemstone)
                {
                    gemstone.material.SetColor("_EmissionColor", Colors.colors[color] * 5);
                }
            }
        }
    }


    public override void BeamExit(GameObject other)
    {
            if (other.gameObject == recievingLightBeam)
            {
                if (gemstone)
                {
                    gemstone.material.SetColor("_EmissionColor", Color.black);
                }
                foreach (LightBeam newLightBeam in newLightBeams)
                {
                    newLightBeam.SetLightBeamOff();
                }
                    recievingLightBeam = null;
            }
    }


}
