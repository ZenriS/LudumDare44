using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Script : MonoBehaviour
{
    private PlayerInput_Script _playerInput;
    private PlayerInv_Script _playerInv;
    public int BloodGain;

    void Start()
    {
        _playerInput = transform.parent.parent.GetComponent<PlayerInput_Script>();
        _playerInv = _playerInput.GetComponent<PlayerInv_Script>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && _playerInput._attackCooldown)
        {
            Destroy(col.gameObject);
            _playerInv.GainBlood(BloodGain);
        }
    }

    public int IncreaseBloodgain(int b)
    {
        BloodGain += b;
        Debug.Log("Blood gain: " +BloodGain);
        return BloodGain;
    }
}
