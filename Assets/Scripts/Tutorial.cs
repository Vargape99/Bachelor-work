using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{

    private TextMeshProUGUI tmp;
    [SerializeField]
    private int tutorialIndex = 0;
    private Animator anim;
    Vector3 position;
    [SerializeField]
    private bool firstTimeHere = true;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Movement playerMovement;
    private Game game;
    private GameObject firstRoom;
    private bool[] done = { false, false ,false ,false};
    int number;
    void Start()
    {
        anim = GetComponent<Animator>();
        tmp = GetComponent<TextMeshProUGUI>();
        game = GetComponentInParent<Game>();
        playerTransform = game.GetPlayer();
        playerMovement = playerTransform.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        number = game.getCurrentRoom().GetComponent<Room>().getRoomNumber();
        if ((number == 5 && tutorialIndex == 8) || (number == 10 && tutorialIndex == 9))
        {
            StepDone();
        }
        SetText();
    }

    private void SetText() {
        if (tutorialIndex == 1)
        {
            float rotx = playerMovement.GetCamera().GetRotX();
            float roty = playerMovement.GetCamera().GetRotY();
            if (rotx > 20)
            {
                done[0] = true;
            }
            else if (rotx < -20)
            {
                done[1] = true;
            }
            if (roty < 250)
            {
                done[2] = true;
            }
            else if (roty > 290)
            {
                done[3] = true;
            }
            tmp.text = "Use a mouse to look<color=" + getColor(done[0]) + "> Up,</color><color=" + getColor(done[1]) + "> Down, </color><color=" + getColor(done[2]) + "> Left,</color><color=" + getColor(done[3]) + "> Right.</color> ";
            if (allDone())
            {
                StepDone();
            }
        }
        else if (tutorialIndex == 2)
        {
            if (firstTimeHere)
            {
                firstTimeHere = false;
                position = playerTransform.position;
            }
            Vector3 currentPosition = playerTransform.position;
            tmp.text = "You can move by pressing W,A,S or D";
            if (Vector3.Distance(position, currentPosition) > 3)
            {
                StepDone();
            }
        }
        else if (tutorialIndex == 3)
        {
            tmp.text = "Go to the next room";
            if (number == 2)
            {
                StepDone();
            }
        }
        else if (tutorialIndex == 4)
        {
            tmp.text = "The next door is locked, and there are masks on the wall that need to be hit by a beam of light of the same color as the mask's eyes.";
            if (firstTimeHere)
            {
                firstTimeHere = false;
                StartCoroutine(StepWillBeDoneIn(7f));
            }
        }
        else if (tutorialIndex == 5)
        {
            tmp.text = "To move the objects, press E when you are near them.";
            if (playerMovement.getPushing())
            {
                StepDone();
            }
        }
        else if (tutorialIndex == 6)
        {
            tmp.text = "Try to find out how the objects interact with the light beams, watch the small bubbles of light they should help you figure it out.";
            if (firstTimeHere)
            {
                firstTimeHere = false;
                StartCoroutine(StepWillBeDoneIn(8f));
            }
        }
        else if (tutorialIndex == 7)
        {
            tmp.text = "When the right color hits the mask, its eyes will begin to shine.";
            if (firstTimeHere)
            {
                firstTimeHere = false;
                StartCoroutine(StepWillBeDoneIn(3f));
            }
        }
        else if (tutorialIndex == 8) {
            tmp.text = "If you need help, press ESC and go to Help. The light interactions are described there.";
            if(firstTimeHere)
            {
                firstTimeHere = false;
                StartCoroutine(FadeOutIn(5f));
            }

        }
        else if (tutorialIndex == 9)
        {
            tmp.text = "Try not to get crushed. If you die, you will respawn at the entrance to the room.";
            if (firstTimeHere)
            {
                firstTimeHere = false;
                StartCoroutine(FadeOutIn(5f));
            }
        }

        else if (tutorialIndex == 10)
        {
            tmp.text = "Be careful. Those are concentrated beams. If you touch them, you will be disintegrated";
            if (firstTimeHere)
            {
                firstTimeHere = false;
                StartCoroutine(FadeOutIn(5f));
            }
        }
    }

    IEnumerator StepWillBeDoneIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        StepDone();
    }

    IEnumerator FadeOutIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FadeTutorial();
    }


    string getColor(bool done) {
        if (done)
        {
            return "green";
        }
        else {
            return "#00ffffff";
        }
    }

    public void StartTutorial() {
        tutorialIndex = 0;
        anim.SetBool("StepDone", true);
    }

    private void FadeTutorial() {
        anim.SetBool("Fade", true);
    }

    private void StepDone() {
        anim.SetBool("Fade", false);
        anim.SetBool("StepDone", true); 
    }

    public void IncreaseIndex() {
        firstTimeHere = true;
        anim.SetBool("StepDone", false);
        tutorialIndex++;
    }

    private bool allDone() {
        foreach (bool boo in done) {
            if (boo == false) {
                return false;
            }
        }
        return true;
    }

    private void ResetDone() {
        for (int i = 0; i < done.Length; i++)
        {
            done[i] = false;
        }
    }
}
