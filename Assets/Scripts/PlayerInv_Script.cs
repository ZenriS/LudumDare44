using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerInv_Script : MonoBehaviour
{
    [Serializable]
    public class AllSeeds
    {
        public string SeedName;
        public int SeedID;
        public GameObject SeedDrop;
        public int SeeDropAmount;
        public int SeedCount;
    }
    public AllSeeds[] Seeds;
    public float FertilizeCost;

    public float MaxBloodAmount = 100;
    public float BloodAmount;
    public Image BloodbagBackground;
    public TextMeshProUGUI SeedText;
    public TextMeshProUGUI BloodText;
    public GameMananger_Script GameManagScript;
    public GameObject BloodDropPrefab;
    private bool _damageCoolDown;
    private AudioSource _audioSource;
    public AudioClip[] AudioClips;

    void OnEnable()
    {
        GameMananger_Script.SetSFXVolume += UpdateVolume;
    }

    void OnDisable()
    {
        GameMananger_Script.SetSFXVolume -= UpdateVolume;
    }

    void UpdateVolume(float v)
    {
        _audioSource.volume = v;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        BloodAmount = MaxBloodAmount;
        UpdateSeedText();
        UpdateBlooBag(false);
    }
    
    public void UseSeed(int id)
    {
        if (Seeds[id].SeedCount > 0)
        {
            Seeds[id].SeedCount--;
            UpdateSeedText();
        }
    }

    public void UpdateSeedText()
    {
        SeedText.text = "Seeds:\n " + Seeds[0].SeedCount;
    }

    public void UseBlood(int a)
    {
        BloodAmount -= a;
        //TO-DO: spawn some blood stain effect or somthing and slite cam shake
        UpdateBlooBag();
        for (int i = 0; i < 5; i++)
        {
            GameObject bd = Instantiate(BloodDropPrefab, this.transform.position, Quaternion.identity);
            float r = Random.Range(-360, 360);
            bd.transform.localEulerAngles = new Vector3(0, 0, r);
        }
        if (BloodAmount <= 0)
        {
            Debug.Log("player dead, you bleed out");
            this.gameObject.SetActive(false);
            GameManagScript.GameOver("Game Over","You bleed out");
        }

    }

    public void GainBlood(int a)
    {
        BloodAmount += a;
        GameManagScript.TotalBlood += a;
        if (BloodAmount > MaxBloodAmount)
        {
            BloodAmount = MaxBloodAmount;
        }
        UpdateBlooBag();
        if (a > 0)
        {
            float r = Random.Range(0.9f, 1.4f);
            _audioSource.pitch = r;
            _audioSource.PlayOneShot(AudioClips[1]);
        }
    }

    void UpdateBlooBag(bool s = true)
    {
        float b = BloodAmount / MaxBloodAmount;
        BloodbagBackground.fillAmount = b;
        BloodText.text = "Blood:\n" + BloodAmount;
        if (s)
        {
            float r = Random.Range(0.9f, 1.4f);
            _audioSource.pitch = r;
            _audioSource.PlayOneShot(AudioClips[0]);
        }

    }

    public float UpgradeFertilizeCost(float a)
    {
        FertilizeCost -= a;
        Debug.Log("Fertilize Cost: " +FertilizeCost);
        return FertilizeCost;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            UseBlood(10);
        }
    }

    /*void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !_damageCoolDown)
        {
            UseBlood(10);
            Invoke("RestDamageCoolDown",1f);
            _damageCoolDown = true;
        }
    }

    void RestDamageCoolDown()
    {
        _damageCoolDown = false;
    }*/
}
