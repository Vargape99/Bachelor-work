using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactive
{
    private Animator anim;
    [SerializeField]
    private bool leverUp = false;

    private bool startState;

    [SerializeField]
    private List<LeverControl> controledObjects;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        startState = leverUp;
    }

    private void OnEnable()
    {
        anim.SetBool("LeverUp", leverUp);
    }

    public override void Interact(Movement other)
    {
        leverUp = !leverUp;
        foreach (LeverControl controledObject in controledObjects) {
            controledObject.Switch();
        }
        
        anim.SetBool("LeverUp", leverUp);
        
    }


    public override void StopInteracting()
    {
        return;
    }

    public bool GetLeverUp() {
        return leverUp;
    }

    public void Reset() {
        leverUp = startState;
        anim.SetBool("LeverUp", leverUp);
    }
}
