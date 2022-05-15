using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for moving walls 
public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 newPosition;
    [SerializeField]
    private bool GoPos = true;
    [SerializeField]
    private float step = 2;
    [SerializeField]
    private float value;
    [SerializeField]
    GameObject face;
    ColorReciever reciever;

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        reciever = face.GetComponentInChildren<ColorReciever>();
        transform.localPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (reciever.IsRecievingRightColor())
        {
            GoPos = true;
        }
        else {
            GoPos = false;
        }
        if (GoPos)
        {
            if (value < 1)
            {
                moving = true;
                transform.localPosition = Vector3.Lerp(startPosition, newPosition, value);
                value += Time.deltaTime / step;
            }
            else if (value > 1)
            {
                moving = false;
                value = 1;
                transform.localPosition = newPosition;
            }
        }
        else {
            if ( value > 0)
            {
                moving = true;
                transform.localPosition = Vector3.Lerp(startPosition, newPosition, value);
                value -= Time.deltaTime / step;
            }
            else if (value < 0)
            {
                moving = false;
                value = 0;
                transform.localPosition = startPosition;
            }
        }
    }
    //if it hits an object the player put in its way, it will start its respawn
    private void OnCollisionEnter(Collision collision)
    {
        if (moving) {
            if (collision.transform.GetComponent<Moveable>()) {
                collision.transform.GetComponent<Moveable>().StartResetAnimation();
            }
        }
    }
}
