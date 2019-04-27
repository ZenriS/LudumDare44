using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemey_Script : MonoBehaviour
{
    private Transform _player;
    public float MoveSpeed;
    private bool _coolDown;
    private bool _stopMovement;


    void Start()
    {
        _player = FindObjectOfType<PlayerInv_Script>().transform;
    }

    void Update()
    {
        Movement();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        /*if (col.tag == "Sword")
        {
            Destroy(this.gameObject);
        }*/
        if (col.transform.tag == "Player" && !_coolDown)
        {
            col.transform.GetComponent<PlayerInv_Script>().UseBlood(10);
            _coolDown = true;
            Invoke("ResetCooldown",1f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            _stopMovement = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            _stopMovement = false;
        }
    }

    void ResetCooldown()
    {
        _coolDown = false;
    }

    void Movement()
    {
        if (!_stopMovement)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _player.position, MoveSpeed * Time.deltaTime);
        }

    }
}
