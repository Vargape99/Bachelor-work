using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keep lists of all resetable objects
public class Reseter : MonoBehaviour
{
    Lever[] levers;
    LaserMask[] laserMasks;
    Moveable[] moveabels;

    public void Awake()
    {
        levers = GetComponentsInChildren<Lever>();
        laserMasks = GetComponentsInChildren<LaserMask>();
        moveabels = GetComponentsInChildren<Moveable>();
    }


    public void Reset()
    {
        foreach (Lever lever in levers) {
            lever.Reset();
        }

        foreach (LaserMask laserMask in laserMasks) {
            laserMask.Reset();
        }

        foreach (Moveable moveab in moveabels) {
            moveab.StartResetAnimation();
        }
    }
}
