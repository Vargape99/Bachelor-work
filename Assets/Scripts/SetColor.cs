using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{   
    [SerializeField]
    private string myColor;

    public string GetMyColor() {
        return myColor;
    }
}
