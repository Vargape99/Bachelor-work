using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    List<GameObject> faces;
    [SerializeField]
    List<ColorReciever> recievers;
    Transform player;
    [SerializeField]
    List<GameObject> rooms;
    [SerializeField]
    AudioClip open,close;
    AudioSource audioSourece;

    Animator anim;

    private bool _open = true;
    private bool doorWasOpened = false;
    public int range = 5;
    private Game game;

    string lastPlayed = null;

    private Renderer rend;
    void Start()
    {
        audioSourece = GetComponentInChildren<AudioSource>();
        anim = GetComponent<Animator>();
        game = GetComponentInParent<Game>();
        player = game.GetPlayer();
        rend = this.GetComponent<Renderer>();
        recievers = new List<ColorReciever>();
        foreach (GameObject face in faces){
            recievers.Add(face.GetComponentInChildren<ColorReciever>());
        }
        Close();
    }

    void Update()
    {
        
        if (_open && isInRange(player))
        {
            game.setCurrentRoom();
            anim.SetBool("Opened", true);
            
            doorWasOpened = true;
            if (rooms.Count > 0)
            {
                
                rooms[0].SetActive(true);
                rooms[1].SetActive(true);
                if (rooms[0] == game.getCurrentRoom())
                {
                    game.setOtherRoom(rooms[1]);
                }
                else {
                    game.setOtherRoom(rooms[0]);
                }
                game.ActivateDoors();
            }
        }
        else if (doorWasOpened)
        {
            
            doorWasOpened = false;
            anim.SetBool("Opened", false);
            
        }
        Close();
    }

    private bool isInRange(Transform other) {
        var dx = rend.bounds.center.x - other.position.x;
        var dz = rend.bounds.center.z - other.position.z;
        if ((dx * dx + dz * dz) < range) {
            return true;
        }
        return false;
    }

    //check if all the masks are recieving the right color, if not lock the door
    public void Close() {
        if (recievers.Count > 0)
        {
            bool tobeopen = true;
            foreach (ColorReciever reciever in recievers)
            {
                if (!reciever.IsRecievingRightColor())
                {
                    tobeopen = false;
                    break;
                }
            }

            if (tobeopen)
            {
                anim.SetBool("Locked", false);
                _open = true;
            }
            else if (!tobeopen)
            {
                anim.SetBool("Locked", true);
                _open = false;
            }
        }
        else {
            anim.SetBool("Locked", false);
            _open = true;
        }
    }

    public void HideRoomsOnClose() {
        game.Rooming();
    }

    public bool IsInRoom(GameObject room) {
        if (rooms.Contains(room))
        {
            return true;
        }
        else {
            return false;
        }
    }

    public bool IsBetweenRooms() {
        if (rooms.Count > 0)
        {
            return true;
        }
        else {
            return false;
        }
    }

    private void PlayAudio() {
        if (lastPlayed != "Open" && doorWasOpened == true)
        {
            lastPlayed = "Open";
            PlayAudioOpen();
        }
        else if (lastPlayed != "Close" && doorWasOpened == false) {
            lastPlayed = "Close";
            PlayAudioClose();
        }
    }

    public void PlayAudioOpen() {
        Debug.Log("PLay open");
        audioSourece.PlayOneShot(open);
    }

    public void PlayAudioClose() {
        Debug.Log("PLay close");
        audioSourece.PlayOneShot(close);
    }
}
