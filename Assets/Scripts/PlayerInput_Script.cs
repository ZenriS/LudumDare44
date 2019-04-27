using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput_Script : MonoBehaviour
{
    private Vector2 _velocity;
    public float MovementMultiplier;

    private Rigidbody2D _rigidbody2D;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    
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
    }
}
