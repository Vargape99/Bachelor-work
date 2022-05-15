using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField]
    private bool ispressed;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - new Vector3(0.5f, 0.5f, 0.5f), new  Vector3(0,1,0), out hit, 5))
        {
            if (hit.collider != null)
            {
                if (hit.collider.transform.tag == "Pillar" || hit.collider.transform.tag == "Player" || hit.collider.transform.tag == "Box")
                {
                    ispressed = true;
                    GetComponent<Renderer>().material.color = Color.blue;
                }
                else {
                    ispressed = false;
                    GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
        else {
            ispressed = false;
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public bool getIsPressed() {
        return ispressed;
    }


}
