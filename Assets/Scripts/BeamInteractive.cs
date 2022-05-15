using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//all things that interact with beam should derive from this
abstract public class BeamInteractive : MonoBehaviour
{
    public abstract void BeamEnter(string color, GameObject beam);
    public abstract void BeamStay(GameObject beam);
    public abstract void BeamExit(GameObject beam);
}
