using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//just to check if the player didnt put anything into the door
public class DoorCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Moveable>()) {
            collision.transform.GetComponent<Moveable>().StartResetAnimation();
        }
    }
}
