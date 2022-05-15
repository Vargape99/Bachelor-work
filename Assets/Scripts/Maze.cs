using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    Game game;
    Transform player;
    Room room;

    [SerializeField]
    GameObject Glowlight;
    GameObject currentRoom;
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponentInParent<Game>();
        room = GetComponent<Room>();
        player = game.GetPlayer();   
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRoom == null || currentRoom != game.getCurrentRoom()) {
            currentRoom = game.getCurrentRoom();
            if (room.getRoomNumber() == game.getCurrentRoom().GetComponent<Room>().getRoomNumber())
            {
                Glowlight.SetActive(true);
                Glowlight.transform.parent = player;
                Glowlight.transform.localPosition = new Vector3(0, 2, 0);
            }
            else {
                Glowlight.SetActive(false);
                Glowlight.transform.parent = transform;
            }
        }
        
    }
}
