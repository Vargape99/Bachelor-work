using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), out hit, 3))
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Player")
                {
                    Debug.Log("PLAYER HIT and is dead");
                }
            }
        }
    }
}
