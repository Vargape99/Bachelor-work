using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private int speed = 1;
    Rigidbody rb;
    [SerializeField]
    private bool pushing = false;
    [SerializeField]
    private int pushingspeed = 100;
    private float angleWhenE;
    private Game game;
    private bool wDown, aDown, sDown, dDown;
    private bool avaibleMovement = true;
    private CameraScript cam;
    [SerializeField]
    LayerMask mask;
    private Interactive interactedItem;

    Vector3 lastposition;
    bool isPlaying;
    AudioSource audioSource;

    private bool eDown;

    private void Awake()
    {
        cam = GetComponentInChildren<CameraScript>();
        rb = GetComponent<Rigidbody>();
        game = GetComponentInParent<Game>();
        rb.centerOfMass = Vector3.zero;
        avaibleMovement = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            eDown = true;
        }
        wDown = Input.GetKey(KeyCode.W);
        aDown = Input.GetKey(KeyCode.A);
        sDown = Input.GetKey(KeyCode.S);
        dDown = Input.GetKey(KeyCode.D);
    }


    public bool getPushing() {
        return pushing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actionButton();
        if (avaibleMovement)
        {
            Vector3 movement = Vector3.zero;

            if (!pushing)
            {
                if (wDown)
                {
                    movement += transform.forward;
                }
                if (sDown)
                {
                    movement += -transform.forward;
                }
                if (aDown)
                {
                    movement += -transform.right;
                }
                if (dDown)
                {
                    movement += transform.right;
                }
                rb.velocity = movement.normalized * Time.deltaTime * speed;
            }
            else if (pushing)
            {
                if (wDown)
                {
                    movement = transform.forward;
                }
                if (sDown) {
                    movement = -transform.forward;
                }
                rb.velocity = movement.normalized * Time.deltaTime * pushingspeed;
            }
        }
        eDown = false;

        if (pushing)
        {
            if (Mathf.Abs(Vector3.Distance(lastposition, transform.position)) > 0.01 && isPlaying == false)
            {
                audioSource.mute = false;
                isPlaying = true;
            }
            else if (Mathf.Abs(Vector3.Distance(lastposition, transform.position)) <= 0.01 && isPlaying == true)
            {
                isPlaying = false;
                audioSource.mute = true;
            }
        }
        else if(isPlaying == true) {
            audioSource.mute = true;
            isPlaying = false;
        }
        lastposition = transform.position;
    }

    // when e down cast a ray and try to interact
    private void actionButton() {
        RaycastHit hit;
        if (eDown) {
            if (pushing) {
                interactedItem.StopInteracting();
                pushing = false;
            }
            else if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, 1, mask))
            {

                if (hit.collider != null)
                {
                    if ((hit.transform.parent && hit.transform.parent.GetComponent<Interactive>()) || hit.transform.GetComponent<Interactive>())
                    {
                        Interactive interact;
                        if (hit.transform.parent && hit.transform.parent.GetComponent<Interactive>())
                        {
                            interact = hit.transform.parent.GetComponent<Interactive>();
                        }
                        else
                        {
                            interact = hit.transform.GetComponent<Interactive>();
                        }
                        interact.Interact(this);
                        interactedItem = interact;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactive>()) {
            other.GetComponent<Interactive>().SetIsInRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactive>())
        {
            other.GetComponent<Interactive>().SetIsInRange(false);
        }
    }

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation) {
        transform.rotation = rotation;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void setPushing(bool push) {
        if (pushing == false && push) {
            lastposition = transform.position;
            angleWhenE = transform.eulerAngles.y;
        }
        if (pushing && push == false) {
            interactedItem.StopInteracting();
        }
        pushing = push;
    }


    public float angleYWhenPushPress() {
        return angleWhenE;
    }


    //For now also set the ability to rotate
    public void SetAvaibleMovement(bool value) {
        avaibleMovement = value;
        cam.setActive(value);
    }

    public CameraScript GetCamera() {
        return cam;
    }

    //checks in which room the player is by sending a raycast to the floor and finding the floors parent
    public Room getCurrentRoom(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up, -transform.up, out hit, 2, mask))
        {
            if (hit.transform.GetComponentInParent<Room>())
            {
                return hit.transform.GetComponentInParent<Room>();
            }
            else
            {
                Debug.Log("RAYCAST FROM PLAYER DIDNT HIT AN OBJECT WHICH WOULD HAVE A PARETN WITH ROOM");
                return null;
            }
        }
        else {
            Debug.Log("RAYCAST FROM PLAYER TO DETECT ROOM DID NOT HIT ANYTHING");
            return null;
        
        }


    }
}
