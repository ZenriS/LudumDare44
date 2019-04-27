using System.Collections;
using System.Collections.Generic;
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
    public int FertilizeCost;
    private bool _allowInteraction;
    private GameObject seedDrop;
    private int seedGiveAmount;


    void Start()
    {
        _spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _planerSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _growTimer = 4;
    }

    void Update()
    {
        if (_planted && !_fullyGrown)
        {
            GrowPlant();
        }
        if (_allowInteraction)
        {
            if (Input.GetButtonDown("Fire1"))
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

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _allowInteraction = false;
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

    void FertilizBed()
    {
        if (!_fertilized)
        {
            _playerInv.UseBlood(FertilizeCost);
            _spriteRenderer.color = Color.red;
            _fertilized = true;
        }
    }

    void PlantSeed()
    {
        if (!_planted && _fertilized)
        {
            Debug.Log("seed planeted");
            _playerInv.UseSeed(0);
            seedDrop = _playerInv.Seeds[0].SeedDrop;
            seedGiveAmount = _playerInv.Seeds[0].SeeDropAmount;
            _spriteRenderer.color = Color.green;
            _planted = true;
        }
    }

    void GrowPlant()
    {
        Debug.Log("plant growing");
        if (_growTimer <= 0)
        {
            _spriteRenderer.color = Color.blue;
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
            _spriteRenderer.color = Color.white;
            _planted = false;
            _fullyGrown = false;
            _growTimer = 4;
            _planerSpriteRenderer.sprite = null;
            for (int i = 0; i < seedGiveAmount; i++)
            {
                Instantiate(seedDrop, this.transform.position, Quaternion.identity, this.transform);
            }
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
