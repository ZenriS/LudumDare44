using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMananger_Script : MonoBehaviour
{
    public GameObject GameOverScreen;
    private TextMeshProUGUI[] _gameOverText;
    public int TotalBlood;
    public bool isGameOver;
    public GameObject PauseScreen;

    public Slider[] MusicSlider;
    public Slider[] SFXSlider;
    private AudioSource[] _audioSource;
    public AudioClip SfxTestClip;
    private float sfxVolume;

    public delegate void UpdateSFX(float v);
    public static event UpdateSFX SetSFXVolume;

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        GameOverScreen.SetActive(false);
        _gameOverText = GameOverScreen.GetComponentsInChildren<TextMeshProUGUI>();
        _audioSource = GetComponents<AudioSource>();
        if (PlayerPrefs.GetInt("First") == 0)
        {
            _audioSource[0].volume = 0.5f;
            _audioSource[1].volume = 0.5f;
            PlayerPrefs.SetInt("First",1);
        }
        else
        {
            _audioSource[0].volume = PlayerPrefs.GetFloat("Music");
            _audioSource[1].volume = PlayerPrefs.GetFloat("SFX");
            sfxVolume = _audioSource[1].volume;
            SetSFXVolume(sfxVolume);
        }
        MusicSlider[0].value = _audioSource[0].volume;
        MusicSlider[1].value = _audioSource[0].volume;
        SFXSlider[0].value = _audioSource[1].volume;
        SFXSlider[1].value = _audioSource[1].volume;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (PauseScreen.activeInHierarchy && !isGameOver)
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            SetSFXVolume(sfxVolume);
        }
        else
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }
    }

    public void SetMusicSound(Slider s)
    {
        float v = s.value;
        PlayerPrefs.SetFloat("Music", v);
        _audioSource[0].volume = v;
        MusicSlider[0].value = v;
        MusicSlider[1].value = v;
    }

    public void SetSFXSound(Slider s)
    {
        float v = s.value;
        PlayerPrefs.SetFloat("SFX", v);
        _audioSource[1].volume = v;
        sfxVolume = v;
        _audioSource[1].clip = SfxTestClip;
        if (!_audioSource[1].isPlaying)
        {
            _audioSource[1].Play();
        }

        SFXSlider[0].value = v;
        SFXSlider[1].value = v;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver(string a, string b)
    {
        isGameOver = true;
        Time.timeScale = 0;
        _gameOverText[0].text = a;
        _gameOverText[1].text = b;
        _gameOverText[2].text = "Blood Collected: " + TotalBlood;
        GameOverScreen.SetActive(true);
    }


}
