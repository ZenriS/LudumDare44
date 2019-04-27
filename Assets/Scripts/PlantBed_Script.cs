using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBed_Script : MonoBehaviour
{
    private bool _fertilized;
    private bool _planted;
    private SpriteRenderer _spriteRenderer;
    private float _growTimer;
    private bool _fullyGrown;
    private PlayerInv_Script _playerInv;
    public int FertilizeCost;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _growTimer = 4;
    }

    void Update()
    {
        if (_planted)
        {
            GrowPlant();
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

        }
    }

    void OnTriggerStay2D(Collider2D col)
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
    }

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
        }
        else
        {
            _growTimer -= Time.deltaTime;
        }
    }

    void Harvest()
    {
        if (_fullyGrown)
        {
            _spriteRenderer.color = Color.white;
            _planted = false;
            _fullyGrown = false;
            _growTimer = 4;
        }
    }

    
}
