using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMask : LeverControl
{

    private Renderer crystalRend;
    [SerializeField]
    private GameObject crystal;
    [SerializeField]
    private Laser laser;
    private bool isOn;

    [SerializeField]
    private int timeBetweenLasers;
    [SerializeField]
    private int timeOfLaserOn;
    [SerializeField]
    private float offset;
    Color red;
    [SerializeField]
    private bool turnedOn = true;

    private bool startState;

    void Awake()
    {
        startState = turnedOn;
        red = new Color(1, 0, 0, 1);
        crystalRend = crystal.GetComponent<Renderer>();
        startInvokes();
    }

    void startInvokes() {
        if (timeBetweenLasers == 0)
        {
            Bright();
            LaserOn();
        }
        else
        {
            InvokeRepeating("Bright", offset, 1);
            InvokeRepeating("Dim", offset + 0.5f, 1);
            InvokeRepeating("LaserOn", offset + 0.01f, (timeBetweenLasers + timeOfLaserOn));
            InvokeRepeating("LaserOff", offset + 0.01f + timeOfLaserOn, (timeBetweenLasers + timeOfLaserOn));
        }
    }

    private void Bright() {
        if (!isOn)
        {
            
            crystalRend.material.SetColor("_EmissionColor", red * 50);
        }
    }

    private void Dim()
    {
        if (!isOn)
        {
            crystalRend.material.SetColor("_EmissionColor", Color.black);
        }
    }

    private void LaserOn() {
        laser.SetLaserOn();
        isOn = true;
    }

    private void LaserOff() {
        laser.SetLaserOff();
        isOn = false;
        Dim();
    }

    public void TurnOn()
    {
        startInvokes();
    }

    public void TurnOff()
    {
        CancelInvoke();
        Dim();
        LaserOff();
    }

    public override void Switch()
    {
        if (turnedOn)
        {
            turnedOn = false;
            TurnOff();
        }
        else {
            turnedOn = true;
            TurnOn();
        }
    }

    public void OnEnable()
    {
        if (turnedOn)
        {
            TurnOn();
        }
        else {
            TurnOff();
        }
    }

    public void OnDisable()
    {
        TurnOff();
    }

    public void Reset()
    {
        turnedOn = false;
        TurnOff();
        if (startState)
        {
            turnedOn = true;
            TurnOn();
        }
    }
}
