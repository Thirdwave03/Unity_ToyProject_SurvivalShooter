using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//using UnityEditor;

public class UIManager : MonoBehaviour
{
    public GameObject paused;
    public Slider musicVol;
    public Slider effectsVol;
    private float musicVolume;
    private float effectsVolume;
    
    public Toggle soundToggle;
    public Button quitGame;
    public Button resumeGame;
    public PlayerShooting playerShooting;

    public AudioMixer master;

    private bool isPaused;
    
    private void Start()
    {
        musicVolume = 0.5f;
        effectsVolume = 0.5f;
        quitGame.onClick.AddListener(PressedButtonQuitGame);
        resumeGame.onClick.AddListener(PressedButtonResume);
        musicVol.onValueChanged.AddListener(OnSlideMusicVolume);
        effectsVol.onValueChanged.AddListener(OnSlideEffectVolume);
        soundToggle.onValueChanged.AddListener(OnSoundToggled);
        musicVol.value = 1f;
        effectsVol.value = 1f;
        musicVolume = musicVol.value;
        effectsVolume = effectsVol.value;
        paused.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            paused.SetActive(isPaused);
            if (isPaused)
            {
                playerShooting.enabled = false;
                Time.timeScale = 0f;
            }
            else
            {
                playerShooting.enabled = true;
                Time.timeScale = 1f;
            }
        }
        if (!isPaused)
            return;
    }

    private void OnSlideMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        master.SetFloat("musicVol", ToDecibel(musicVolume));        
    }

    private void OnSlideEffectVolume(float newVolume)
    {
        effectsVolume = newVolume;
        master.SetFloat("sfxVol", ToDecibel(effectsVolume));
    }

    private float ToDecibel(float val)
    {
        float db = -80f;
        db += val * 80f;
        return db;
    }

    private void OnSoundToggled(bool isOn)
    {
        musicVol.interactable = isOn;
        effectsVol.interactable = isOn;

        if (isOn)
        {
            master.SetFloat("musicVol", ToDecibel(musicVolume));
            master.SetFloat("sfxVol", ToDecibel(effectsVolume));
        }
        else
        {
            master.SetFloat("musicVol", ToDecibel(0f));
            master.SetFloat("sfxVol", ToDecibel(0f));
        }

    }

    private void PressedButtonQuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
    
    private void PressedButtonResume()
    {
        isPaused = false;
        paused.SetActive(isPaused);
        playerShooting.enabled = true;
        Time.timeScale = 1f;
    }
}
