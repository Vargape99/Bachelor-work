using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all things that can be directed by levers should derive from this
public abstract class LeverControl : MonoBehaviour
{
    public abstract void Switch();
}
