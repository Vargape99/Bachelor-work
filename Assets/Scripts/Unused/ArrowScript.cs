                                                                                                                                                     using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject effect;
    private Rigidbody rb;
    [SerializeField]
    public Game game;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(effect, 10);
        Destroy(this.gameObject, 10);
    }
    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed); 
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == this.transform.parent.gameObject) {
            return;
        }
        if (other.gameObject.tag == "Arrow") {
            return;
        }
        if (other.collider.tag == "Player")
        {
            Debug.Log("Player HIT");
            game.StartRespawn();
        }
        effect.transform.parent = null;
        Destroy(effect.gameObject, 1);
        Destroy(this.gameObject);
    }
}
