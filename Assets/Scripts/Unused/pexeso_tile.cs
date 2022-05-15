using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pexeso_tile : MonoBehaviour
{
    [SerializeField]
    private bool turned;
    [SerializeField]
    private bool foundDouble;
    [SerializeField]
    private int number;
    [SerializeField]
    private Light theLight;
    // Start is called before the first frame update


    // Update is called once per frame
    private void Start()
    {
        theLight.intensity = 0;   
    }

    public void setColor(Color col)
    {
        theLight.color = col;
    }

    public bool getTurned()
    {
        return turned;
    }

    public void setTurned(bool set)
    {
        if (set == true)
        {
            StartCoroutine(brightUp());
        }
        else {
            StartCoroutine(brightDown());
        }
        turned = set;
    }

    public bool getFoundDouble()
    {
        return foundDouble;
    }

    public void setFoundDouble(bool set)
    {
        foundDouble = set;
    }

    public int getNumber() {
        return number;
    }
    public void setNumber(int set) {
        number = set;
    }

    IEnumerator brightUp() {
        float intensity = 0;
        float timeS = Time.time;
        while (Time.time < timeS + 1) {
            theLight.intensity = intensity;
            yield return new WaitForSeconds(0.1f);
            intensity += 1;          
        }
    }

    IEnumerator brightDown()
    {
        float intensity = 9;
        float timeS = Time.time;
        while (Time.time < timeS + 1)
        {
            theLight.intensity = intensity;
            yield return new WaitForSeconds(0.1f);
            intensity -= 1;
        }
        Debug.Log(intensity);
    }

}


