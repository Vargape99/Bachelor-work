using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{   

    [SerializeField]
    private int roomNumber;
    [SerializeField]
    private Reseter reseter;
    Vector3[] pillarCoord;

    private void Awake()
    {
        reseter = GetComponent<Reseter>();
    }

    public void Respawn() {
        if (reseter)
        {
            reseter.Reset();
        }
    }

    public int getRoomNumber() {
        return roomNumber;
    }
}
