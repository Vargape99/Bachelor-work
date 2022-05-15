using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject firstMainSelected, firstOptionsSelected, optionsExitSelected, firstLevelSelected, levelExitSelected;
    [SerializeField]
    private GameObject mainMenu, options, level;
    [SerializeField]
    private Slider mouseSensibility;
    [SerializeField]
    private Slider volume;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(true);
        options.SetActive(false);
        level.SetActive(false);
        mouseSensibility.value = TransferData.Instance.mouseSensitivity/10;
    }

    public void OpenOptions() {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void CloseOptions() {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void OpenLevel() {
        mainMenu.SetActive(false);
        level.SetActive(true);
    }

    public void CloseLevel() {
        mainMenu.SetActive(true);
        level.SetActive(false);
    }

    public void ChangeMouseSensibility() {
        float value = mouseSensibility.value;
        TransferData.Instance.mouseSensitivity = value*10;
    }

    private void Select(GameObject it) {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(it);
    }

    public void ChangeVolume()
    {
        TransferData.Instance.volume = volume.value;
        AudioListener.volume = volume.value;
    }

    public void Quit() {
        Application.Quit();
    }
}
