using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : Interactive
{



    public override void Interact(Movement other)
    {
        if (isInRange) {
            GetComponentInParent<Animator>().SetBool("EndLevel", true);
            other.transform.SetParent(transform, true);
        }
    }

    public override void StopInteracting()
    {
        return;
    }

    public void nextLevel() {
       GetComponentInParent<Game>().GetLevelLoader().LoadNextLevel();
    }
}
