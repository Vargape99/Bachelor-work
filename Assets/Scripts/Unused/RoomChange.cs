using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField]
    private GameObject room;
    private Game game;

    private void Start()
    {
        game = GetComponentInParent<Game>();
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player") {
          //  game.setCurrentRoom(room);
        }
    }
}
