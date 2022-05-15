using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the core class 
public class Game : MonoBehaviour
{
    private GameObject currentRoom;
    private GameObject otherRoom;
    private Transform player;
    private Movement playerMovement;
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private Tutorial tutorial;

    private List<GameObject> rooms = new List<GameObject>();
    private List<GameObject> doors = new List<GameObject>();

    private CrossFade crossFade;


    private void Awake()
    {
        playerMovement = GetComponentInChildren<Movement>();
        crossFade = GetComponentInChildren<CrossFade>();
        player = playerMovement.transform;
        GetRoomsAndDoors();
        currentRoom = rooms[0];
    }
    private void Start()
    {
        Respawn();
        AudioListener.volume = TransferData.Instance.volume;
    }

    //find all rooms and doors in the level
    private void GetRoomsAndDoors() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<Room>()) {
                rooms.Add(child.gameObject);
            }
            if (child.GetComponent<Door>()) {
                if (child.GetComponent<Door>().IsBetweenRooms())
                {
                    doors.Add(child.gameObject);
                }
            }
        }
        rooms.Sort((r1, r2) => r1.GetComponent<Room>().getRoomNumber().CompareTo(r2.GetComponent<Room>().getRoomNumber()));
    }
    

    public void setCurrentRoom() {
        currentRoom = playerMovement.getCurrentRoom().gameObject;
    }

    public void setOtherRoom(GameObject newRoom) {
        otherRoom = newRoom;
    }

    public GameObject getCurrentRoom()
    {
       return currentRoom;
    }

    //set doors and rooms to active or deactive based on current room
    public void Rooming() {       
        foreach (var room in rooms) {
            if (room == currentRoom)
            {
                room.SetActive(true);
            }
            else {
                room.SetActive(false);
            }
        }

        foreach (var door in doors)
        {
            if (door.GetComponent<Door>().IsInRoom(currentRoom))
            {
                door.SetActive(true);
            }
            else
            {
                door.SetActive(false);
            }
        }

    }

    //call this when you want to start respawn
    public void StartRespawn()
    {
        crossFade.setCrossIt(true);
        playerMovement.SetAvaibleMovement(false);
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    //this is public just beacuse animator needs access
    public void Respawn() {
        playerMovement.setPushing(false);
        CheckPoint checkpoint = currentRoom.GetComponentInChildren<CheckPoint>();
        player.transform.position = checkpoint.getRespawnPoint();
        playerMovement.GetCamera().setRotY(checkpoint.GetRotY());
        playerMovement.GetCamera().setRotX(0);
        currentRoom.GetComponent<Room>().Respawn();
        DisableAllDoors();
        Rooming();
    }
    //this is public just beacuse animator needs access
    public void EndRespawn() {
        playerMovement.SetAvaibleMovement(true);  
    }
    //based on activated rooms also activate doors
    public void ActivateDoors() {
        foreach (var door in doors) {
            if (door.GetComponent<Door>().IsInRoom(otherRoom) || door.GetComponent<Door>().IsInRoom(currentRoom))
            {
                door.SetActive(true);
            }
            else {
                door.SetActive(false);
            }
        }
    }
    //activate doors in one given room
    public void ActivateDoorsInRoom(GameObject room)
    {
        foreach (var door in doors)
        {
            if (door.GetComponent<Door>().IsInRoom(room))
            {
                door.SetActive(true);
            }
        }
    }

    public void DisableAllDoors() {
        foreach (var door in doors)
        {
            door.SetActive(false);
           
        }

    }

    public Transform GetPlayer() {
        return player;
    }

    public void TryStartTutorial() {
        if (tutorial) {
            tutorial.StartTutorial();
        }
    
    }

    public LevelLoader GetLevelLoader() {
        return levelLoader;
    }

}
