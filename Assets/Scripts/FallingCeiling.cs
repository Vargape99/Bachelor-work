using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCeiling : MonoBehaviour
{
    [SerializeField]
    private float wait = 2;
    [SerializeField]
    private float offset = 0;
    private Animator anima;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip boom;


    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    //start Invokes, based on given parameters
    private void OnEnable()
    {
        InvokeRepeating(nameof(setDown), offset + (offset - 1) * 0.4f + 0.4f, 0.4f + wait + (wait - 1) * 0.4f);
        InvokeRepeating(nameof(setUp), offset + (offset - 1) * 0.4f, 0.4f + wait + (wait - 1) * 0.4f);
    }

    private void setDown() {
        anima.SetBool("Down", false);
    }
    private void setUp() {
        anima.SetBool("Down", true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            GetComponentInParent<Game>().StartRespawn();
        }
    }

    private void PlaySound() {
        audioSource.PlayOneShot(boom);
    }
}
