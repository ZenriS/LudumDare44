using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Sword_Script : MonoBehaviour
{
    private PlayerInput_Script _playerInput;
    private PlayerInv_Script _playerInv;
    public int BloodGain;
    private Transform _swordPivot;
    private Vector2 _mousePos;
    private Vector3 _screenPos;
    private AudioSource _audioSource;


    void Start()
    {
        _playerInput = transform.parent.parent.GetComponent<PlayerInput_Script>();
        _playerInv = _playerInput.GetComponent<PlayerInv_Script>();
        _swordPivot = transform.parent;
        _audioSource = _playerInv.GetComponent<AudioSource>();
    }

    void Update()
    {
        /*_screenPos = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, transform.position.z - Camera.main.transform.position.z));
        _mousePos = Input.mousePosition;

        float zRot = Mathf.Atan2((_screenPos.y - transform.position.y), (_screenPos.x - transform.position.x)) * Mathf.Rad2Deg;
        
        _swordPivot.eulerAngles = new Vector3(0,0,zRot);*/
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && _playerInput._doDamage)
        {
            col.GetComponent<Enemey_Script>().Death();
            Destroy(col.gameObject);
            _playerInv.GainBlood(BloodGain);
            float r = Random.Range(0.9f, 1.4f);
            _audioSource.pitch = r;
            _audioSource.PlayOneShot(_playerInv.AudioClips[2]);
        }
    }

    public int IncreaseBloodgain(int b)
    {
        BloodGain += b;
        Debug.Log("Blood gain: " +BloodGain);
        return BloodGain;
    }
}
