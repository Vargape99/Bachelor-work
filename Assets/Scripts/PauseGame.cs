using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{

    CanvasGroup canvasGroup;
    bool isPaused;
    [SerializeField]
    GameObject mainMenu, optionsMenu, helpMenu;
    [SerializeField]
    Slider mouseSensitivitySlider;
    [SerializeField]
    Slider volumeSlider;
    Game game;
    CameraScript cam;
    Movement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponentInParent<Game>();
        cam = game.GetPlayer().GetComponent<Movement>().GetCamera().GetComponent<CameraScript>();
        isPaused = false;
        canvasGroup = GetComponent<CanvasGroup>();
        playerMovement = game.GetPlayer().GetComponent<Movement>();
        canvasGroup.alpha = 0;
        volumeSlider.value = TransferData.Instance.volume;
        mouseSensitivitySlider.value = TransferData.Instance.mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Pause() {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        canvasGroup.alpha = 1;
        playerMovement.SetAvaibleMovement(false);
        EnterMainMenu();
        isPaused = true;
    }
    public void Resume() {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        canvasGroup.alpha = 0;
        playerMovement.SetAvaibleMovement(true);
        isPaused = false;
    }

    public void GoToMenu() {
        GetComponentInParent<Game>().GetLevelLoader().LoadLevelByNumber(0);
        Time.timeScale = 1;
    }

    public void Quit() {
        Application.Quit();
    }

    public void Reset() {
        Resume();
        game.StartRespawn();
    }

    public void EnterHelpMenu() {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void EnterMainMenu() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false); 
        helpMenu.SetActive(false);
    }

    public void ChangeMouseSensibility()
    {
        float value = mouseSensitivitySlider.value;
        TransferData.Instance.mouseSensitivity = value * 10;
        cam.UpdateSensitivity(value * 10);
    }

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        TransferData.Instance.volume = volume;
        AudioListener.volume = volume;
    }

    public void EnterOptionsMenu() {
        
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

}
