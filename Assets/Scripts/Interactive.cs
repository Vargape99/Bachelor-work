using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//all things player can interact with should derive from this class
abstract public class Interactive : MonoBehaviour
{

    [SerializeField]
    protected LayerMask mask;
    [SerializeField]
    protected bool isInRange = false;
    [SerializeField]
    protected bool interactableOverTime = true;
    [SerializeField]
    protected float distanceModifier;
    protected Transform firstParent;




    public abstract void Interact(Movement other);
    public abstract void StopInteracting();

    public bool IsInRange() {
        return isInRange;
    }


    public void SetIsInRange(bool set) {
        isInRange = set;
    }


    public bool GetInteractableOverTime() {
        return interactableOverTime;
    }


}
