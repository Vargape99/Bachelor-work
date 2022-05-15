using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//transfer data between scenes
public class TransferData : MonoBehaviour
{
    public static TransferData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }


    public int messageNumber = 1;
    public float mouseSensitivity = 200;
    public float volume = 1;

}
