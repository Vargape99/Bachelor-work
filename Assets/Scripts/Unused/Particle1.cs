using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField]
    GameObject particle;
    [SerializeField]
    string colour;
    float step = 1.0f;
    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
       
    }

    void Update()
    {
        Vector3 parPos = transform.parent.GetComponentInChildren<LightBeam>().hitpos;
        transform.position = Vector3.MoveTowards(transform.position, parPos , step * Time.deltaTime);
        transform.Rotate(1, 0, 0, Space.Self);

        if (Vector3.Distance(transform.position, transform.parent.GetComponentInChildren<LightBeam>().hitpos) < 0.1f) {
            Destroy(this.gameObject);
        }
        if (Vector3.Dot((parPos- transform.position).normalized , transform.right) < 0.9) { 
            Destroy(this.gameObject);
        }
    }

    public void SetColor(string color) {
        particle.GetComponent<Renderer>().material.SetColor("Emmision", Colors.colors[color]);
        colour = color;


    }


}
