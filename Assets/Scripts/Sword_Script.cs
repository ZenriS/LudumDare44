using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Script : MonoBehaviour
{
    private PlayerInput_Script _playerInput;

    void Start()
    {
        _playerInput = transform.parent.parent.GetComponent<PlayerInput_Script>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && _playerInput._attackCooldown)
        {
            Destroy(col.gameObject);
        }
    }
}
