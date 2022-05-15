using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldButton : Interactive
{
    private Animator anim;
    private Game game;
    private bool pushed;
    [SerializeField]
    private GameObject field;
    [SerializeField]
    private Camera secondCamera;
    Movement playerMovement;
    Camera mainCamera;
    GameObject room;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pushed = false;
        game = GetComponentInParent<Game>();

    }

    private void Start()
    {
        playerMovement = game.GetPlayer().GetComponent<Movement>();
        mainCamera = playerMovement.GetCamera().gameObject.GetComponent<Camera>();
        room = field.GetComponentsInParent<Room>(true)[0].gameObject;
    }

    public override void Interact(Movement other)
    {
        if (!pushed) {         
            anim.SetBool("Pushed", true);
            pushed = true;
        }
    }

    public void StartCutScene() {
        playerMovement.SetAvaibleMovement(false);
        room.SetActive(true);
        game.ActivateDoorsInRoom(room.gameObject);
    }

    public void EndCutScene() {
        playerMovement.SetAvaibleMovement(true);
        game.Rooming();
    }

    public void ChangeToSecondcamera() {
        mainCamera.enabled = false;
        secondCamera.enabled = true;
    }

    public void TurnFieldOff() {
        field.SetActive(false);
    }


    public void ChangeToFirstCamera() {
        secondCamera.enabled = false;
        mainCamera.enabled = true;
    }


    public override void StopInteracting()
    {
        return;
    }
}
