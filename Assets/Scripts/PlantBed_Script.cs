using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlantBed_Script : MonoBehaviour
{
    public Sprite[] Graphics;
    private bool _fertilized;
    private bool _planted;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _planerSpriteRenderer;
    private float _growTimer;
    private bool _fullyGrown;
    private PlayerInv_Script _playerInv;
    //public int FertilizeCost;
    private bool _allowInteraction;
    private GameObject seedDrop;
    private int seedGiveAmount;
    private int _fertilizeUsage;
    private int _fertilzeLeft;
    private TextMeshProUGUI _fertilizeText;
    private AudioSource _audioSource;
    public AudioClip[] AudioClips;
    public GameObject PlanetBedInfoScreen;
    private TextMeshProUGUI _plantBedInfoText;


    void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
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
        _spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _planerSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _fertilizeText = GetComponentInChildren<TextMeshProUGUI>();
        _fertilizeText.gameObject.SetActive(false);

        _plantBedInfoText = PlanetBedInfoScreen.GetComponentInChildren<TextMeshProUGUI>();
        _growTimer = 4;
        _fertilizeUsage = 3;

    }

    void Update()
    {
        if (_planted && !_fullyGrown)
        {
            GrowPlant();
        }
        if (_allowInteraction)
        {
            if (Input.GetButtonDown("Plant"))
            {
                if (!_fertilized)
                {
                    FertilizBed();
                    return;
                }
                PlantSeed();
                //Harvest();
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Player at bed");
            if (_playerInv == null) 
            {
                _playerInv = col.GetComponent<PlayerInv_Script>();
            }
            _allowInteraction = true;
            PlanetBedInfoScreen.SetActive(true);
            UpdateInfoText();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _allowInteraction = false;
            PlanetBedInfoScreen.SetActive(false);
        }
    }

    /*void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!_fertilized)
                {
                    FertilizBed();
                    return;
                }
                PlantSeed();
                Harvest();
            }
        }
    }*/

    void UpdateInfoText()
    {
        if (!_fertilized)
        {
            _plantBedInfoText.text = "Press E \n to fertilize \n for " + _playerInv.FertilizeCost +" blood";
        }
        else if (_fertilized && !_planted)
        {
            if (_playerInv.Seeds[0].SeedCount > 0)
            {
                _plantBedInfoText.text = "Press E \n to plant Blood Flower";
            }
            else
            {
                _plantBedInfoText.text = "Buy seeds at the shop";
            }

        }
        else if (_fertilized && _planted)
        {
            _plantBedInfoText.text = "Blood Flower growing";
        }
    }

    void FertilizBed()
    {
        if (!_fertilized)
        {
            _playerInv.UseBlood((int) _playerInv.FertilizeCost);
            _spriteRenderer.color = Color.red;
            _fertilzeLeft = _fertilizeUsage;
            _fertilizeText.text = _fertilzeLeft.ToString();
            _fertilizeText.gameObject.SetActive(true);
            _fertilized = true;
            UpdateInfoText();
        }
    }

    void PlantSeed()
    {
        if (!_planted && _fertilized)
        {
            if (_playerInv.Seeds[0].SeedCount > 0)
            {
                Debug.Log("seed planeted");
                _playerInv.UseSeed(0);
                seedDrop = _playerInv.Seeds[0].SeedDrop;
                seedGiveAmount = _playerInv.Seeds[0].SeeDropAmount;
                _spriteRenderer.color = Color.green;
                _fertilzeLeft--;
                _fertilizeText.text = _fertilzeLeft.ToString();
                _planted = true;
                float r = Random.Range(0.9f, 1.4f);
                _audioSource.pitch = r;
                _audioSource.PlayOneShot(AudioClips[0]);
                UpdateInfoText();
            }
        }
    }

    void GrowPlant()
    {
        Debug.Log("plant growing");
        if (_growTimer <= 0)
        {
            //_spriteRenderer.color = Color.blue;
            _fullyGrown = true;
            Invoke("Harvest",1f);
        }
        else
        {
            _growTimer -= Time.deltaTime;
        }
        UpdateGraphics();
    }

    void Harvest()
    {
        if (_fullyGrown)
        {
            _spriteRenderer.color = Color.green;
            _planted = false;
            _fullyGrown = false;
            _growTimer = 4;
            _planerSpriteRenderer.sprite = null;
            for (int i = 0; i < seedGiveAmount; i++)
            {
                Instantiate(seedDrop, this.transform.position, Quaternion.identity, this.transform);
            }
            if (_fertilzeLeft <= 0)
            {
                _fertilizeText.gameObject.SetActive(false);
                _spriteRenderer.color = Color.white;
                _fertilized = false;
            }
            float r = Random.Range(0.9f, 1.4f);
            _audioSource.pitch = r;
            _audioSource.PlayOneShot(AudioClips[1]);
        }
    }

    void UpdateGraphics()
    {
        if (_growTimer < 4 && _growTimer > 3)
        {
            _planerSpriteRenderer.sprite = Graphics[0];
        }
        else if (_growTimer < 3 && _growTimer > 2)
        {
            _planerSpriteRenderer.sprite = Graphics[1];
        }
        else if (_growTimer < 2 && _growTimer > 1)
        {
            _planerSpriteRenderer.sprite = Graphics[2];
        }
        else if (_growTimer <= 0 )
        {
            _planerSpriteRenderer.sprite = Graphics[3];
        }
    }

    
}
