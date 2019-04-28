using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput_Script : MonoBehaviour
{
    private Vector2 _velocity;
    public float MovementMultiplier;

    private Rigidbody2D _rigidbody2D;
    public Transform CameraTrans;
    private Vector3 zero;
    private SpriteRenderer _spriteRenderer;
    private float _xScale;
    private float _yScale;
    private Transform _swordPivot;
    private float attackSpeed;
    private bool _attackCooldown;
    [HideInInspector] public bool _doDamage;
    private AudioSource _audioSource;
    public AudioClip AttackAudioClip;

    private Animator _animator;

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

    void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	    _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
	    _animator = _spriteRenderer.GetComponent<Animator>();
	    _audioSource = GetComponent<AudioSource>();
	    zero = Vector3.zero;
	    _xScale = transform.localScale.x;
	    _yScale = transform.localScale.y;
	    _swordPivot = transform.GetChild(1);
	    attackSpeed = 1.5f;

	}
	
	void Update ()
	{
	    CamFollow();
        Attack();
        FlipChar();
        //Attack2();
    }

    void FixedUpdate()
    {
        CharacterMove();
        //Movement();

    }

    void Movement() //cause a lot of lag, no idea why...
    {
        float xInput = Input.GetAxis("Horizontal") * MovementMultiplier;
        float xStep = xInput * Time.deltaTime;
        _velocity.x = xStep;
        float yInput = Input.GetAxis("Vertical") * MovementMultiplier;
        float yStep = yInput * Time.deltaTime;
        _velocity.y = yStep;

        transform.Translate(_velocity.x,_velocity.y, 0);
    }

    void CharacterMove()
    {
        float step = MovementMultiplier * Time.deltaTime;
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(xInput * step, yInput * step);
        _rigidbody2D.velocity = dir;
        /*if (xInput > 0)
        {

            _spriteRenderer.flipX = false;
            Vector3 flip = new Vector3(_xScale,_yScale,1);
            transform.localScale = flip;
        }
        else if(xInput < 0)
        {
            _spriteRenderer.flipX = true;
            Vector3 flip = new Vector3(-_xScale, _yScale, 1);
            transform.localScale = flip;
        }*/

        if (xInput > 0 || xInput < 0 || yInput > 0 || yInput < 0)
        {
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking",false);
        }
    }

    void FlipChar()
    {
        Vector2 mousePos = Input.mousePosition;
        
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,
            transform.position.z - Camera.main.transform.position.z));
        float cX = this.transform.position.x;
        //Debug.Log("mX: " + screenPos.x + " cX: " +cX);
        if (screenPos.x > cX)
        {
            //_spriteRenderer.flipX = false;
            Vector3 flip = new Vector3(_xScale, _yScale, 1);
            transform.localScale = flip;
        }
        else if (screenPos.x < cX)
        {
            //_spriteRenderer.flipX = true;
            Vector3 flip = new Vector3(-_xScale, _yScale, 1);
            transform.localScale = flip;
        }
    }

    void Attack()
    {
        if(!_attackCooldown && Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            float r = Random.Range(0.9f, 1.4f);
            _audioSource.pitch = r;
            _audioSource.PlayOneShot(AttackAudioClip);
            _swordPivot.DORewind();
            _swordPivot.DOLocalRotate(new Vector3(0, 0,-90), 0.25f);
            _doDamage = true;
            _attackCooldown = true;
            StopCoroutine("ResetAttck");
            
            StartCoroutine("ResetAttack");
        }
    }

    void Attack2()
    {
        if (Input.GetButtonDown("Fire1") && !_attackCooldown)
        {
            _swordPivot.DOLocalMoveX(0.03f, 0.2f);
            _attackCooldown = true;

            StopCoroutine("ResetAttack2");
            StartCoroutine("ResetAttack2");
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.25f);
        _doDamage = false;
        _swordPivot.DOLocalRotate(new Vector3(0, 0, 0), attackSpeed);
        yield return new WaitForSeconds(attackSpeed-0.2f);
        _attackCooldown = false;
        Debug.Log("Attack reset done");
    }

    IEnumerator ResetAttack2()
    {
        yield return new WaitForSeconds(0.25f);
        _swordPivot.DOLocalMoveX(0, attackSpeed);
        yield return new WaitForSeconds(attackSpeed - 0.2f);
        _attackCooldown = false;
        Debug.Log("Attack reset done");
    }

    public float UpgradeAttackSpeed(float d)
    {
        attackSpeed -= d;
        Debug.Log("Attack Speed: " +attackSpeed);
        return attackSpeed;
    }

    void CamFollow()
    {
        CameraTrans.transform.position = Vector3.SmoothDamp(CameraTrans.transform.position, this.transform.position,ref zero, 0.2f);
    }
}
