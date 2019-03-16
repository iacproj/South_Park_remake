using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator _anim;

    public float V; // Vertical movement
    public float H; // Horizontal movement

    public bool IsSprint;

    private float _sprint = 0.0f;

    [SerializeField]
    private const float _normalSpeed = 0.0f;
    private const float _sprintSpeed = 1.0f;


    // Use this for initialization
    void Start ()
    {
        _anim = GetComponent<Animator>();
        IsSprint = false;
	}


    public void FixedUpdate()
    {
        PlayerMovement();
        Sprinting();
    }

    public void PlayerMovement()
    {
        V = Input.GetAxis("Vertical");
        H = Input.GetAxis("Horizontal");

        _anim.SetFloat("Speed", V);
        _anim.SetFloat("Turn", H);
        _anim.SetFloat("Sprint", _sprint);
    }

    public void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _sprint = _sprintSpeed;
        }
        else
        {
            _sprint = _normalSpeed;
        }
    }
}
