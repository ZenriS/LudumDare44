using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerInput_Script : MonoBehaviour
{
    private Vector2 _velocity;
    public float MovementMultiplier;

    private Rigidbody2D _rigidbody2D;
    public Transform Camera;
    private Vector3 zero;
    private SpriteRenderer _spriteRenderer;
    private float _xScale;
    private float _yScale;
    private Transform _swordPivot;
    private float attackSpeed;
    [HideInInspector] public bool _attackCooldown;
    // Use this for initialization
    void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	    _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
	    zero = Vector3.zero;
	    _xScale = transform.localScale.x;
	    _yScale = transform.localScale.y;
	    _swordPivot = transform.GetChild(1);
	    attackSpeed = 1.5f;

	}
	
	// Update is called once per frame
	void Update ()
	{
	    CamFollow();
        Attack();
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
        if (xInput > 0)
        {

            //_spriteRenderer.flipX = false;
            Vector3 flip = new Vector3(_xScale,_yScale,1);
            transform.localScale = flip;
        }
        else if(xInput < 0)
        {
            //_spriteRenderer.flipX = true;
            Vector3 flip = new Vector3(-_xScale, _yScale, 1);
            transform.localScale = flip;
        }
    }

    void Attack()
    {
        if(!_attackCooldown && Input.GetButtonDown("Fire1"))
        {
            _swordPivot.DORewind();
            _swordPivot.DOLocalRotate(new Vector3(0, 0,-90), 0.25f);
            _attackCooldown = true;
            StopCoroutine("ResetAttck");
            
            StartCoroutine("ResetAttack");
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.25f);
        _swordPivot.DOLocalRotate(new Vector3(0, 0, 0), attackSpeed);
        yield return new WaitForSeconds(attackSpeed+0.1f);
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
        Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, this.transform.position,ref zero, 0.2f);
    }
}
