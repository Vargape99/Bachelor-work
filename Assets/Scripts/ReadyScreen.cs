using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadyScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmp;
    Animator anim;
    int lastText;
    //if there would be a loading screen the lastText would force the same message on the screen
    void Start()
    {
        anim = GetComponent<Animator>();
        ChangeText();
        GetComponentInParent<Game>().GetPlayer().GetComponent<Movement>().SetAvaibleMovement(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetBool("FadeOut", true);
        }
    }

    public void EndLife() {
        GetComponentInParent<Game>().EndRespawn();
        GetComponentInParent<Game>().TryStartTutorial();
        Destroy(this.gameObject);  
    }

    public void ChangeText() {
        int rand = Random.Range(0, 6);
        while (rand == lastText) {
            rand = Random.Range(0, 6);
        }
        lastText = rand;
        string text = LoadingTexts.getText(rand);
        tmp.text = text;
    }
}
