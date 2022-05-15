using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private float rotY;


    public Vector3 getRespawnPoint() {
        return transform.position;
    }

    public float GetRotY() {
        return rotY;
    }

}
