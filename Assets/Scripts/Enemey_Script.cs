using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemey_Script : MonoBehaviour
{
    private Transform _player;
    public float MoveSpeed;
    private bool _coolDown;
    private bool _stopMovement;
    private SpriteRenderer _spriteRenderer;
    public GameObject BloodPoolPrefab;

    void Start()
    {
        _player = FindObjectOfType<PlayerInv_Script>().transform;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
    }

   /* void OnCollisionStay2D(Collision2D col)
    {
     
        if (col.transform.tag == "Player" && !_coolDown)
        {
            col.transform.GetComponent<PlayerInv_Script>().UseBlood(10);
            _coolDown = true;
            Invoke("ResetMovement", 1f);
        }
    }*/

    /*
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
    */

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            //_stopMovement = true;
            MoveSpeed = -MoveSpeed;
            MoveSpeed *= 3;
            _spriteRenderer.flipX = true;
            Invoke("ResetMovement",0.2f);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            //_stopMovement = false;
        }
    }

    void ResetMovement()
    {
        _coolDown = false;
        MoveSpeed = -MoveSpeed;
        _spriteRenderer.flipX = false;
        MoveSpeed /= 3;
    }

    public void Death()
    {
        Instantiate(BloodPoolPrefab, transform.position, Quaternion.identity);
    }

    void Movement()
    {
        if (!_stopMovement && _player != null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _player.position, MoveSpeed * Time.deltaTime);
        }

    }
}
