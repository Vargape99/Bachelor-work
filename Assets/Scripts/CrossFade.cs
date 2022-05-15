using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//UI when player dies
public class CrossFade : MonoBehaviour
{
    private Animator anim;
    private Game game;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        game = GetComponentInParent<Game>();
    }

    public void setCrossIt(bool value) {
        anim.SetBool("FadeIn", value);
    }


    public void InitializeRespawn() {
        game.Respawn();
        setCrossIt(false);
    }

    public void FinalizeRespawn() {
        game.EndRespawn();
    }
}
