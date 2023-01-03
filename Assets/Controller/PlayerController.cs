using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    //private bool isJumping;
    //private bool isGrounded;


    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private Camera _followCamera;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    [SerializeField]
    private float _jumpHeight = 1.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    //private Vector3 MoveDir;

    [SerializeField]
    private FallPlat fallFlatScript;

    [SerializeField]
    private float forceMagnitude;

    public Text scoreText;
    int score = 0;
    public GameObject txtScore;
    public GameOver GameOver;


    public float force = 10f; //Force 10000f
    public float stunTime = 0.5f;
    private Vector3 hitDir;

    private void Start()
    {   _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        scoreText.text = score.ToString() + " Gift";
    }

    private void Update()
    {
        Movement();
    }


    void Movement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y <= 0)
        {
            _playerVelocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;
        //MoveDir = movementDirection;
        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            _animator.SetBool("IsMoving", true);
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }


        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _animator.SetBool("IsJumping", true);
            //Debug.Log("enter jump");
        }
        else
        {
            _animator.SetBool("IsJumping", false);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Glass"))
    //    {
    //        fallFlatScript.destroyThis();

    //    }
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        Debug.DrawRay(contact.point, contact.normal, Color.white);
    //        if (collision.gameObject.tag == "BounceCube")
    //        {
    //            Debug.Log("hit cube");
    //            hitDir = contact.normal;
    //            collision.gameObject.GetComponent<CharacterControls>().HitPlayer(-hitDir * force, stunTime);
    //            return;
    //        }
    //    }
    //    if (collision.relativeVelocity.magnitude > 2)
    //    {
    //        if (collision.gameObject.tag == "BounceCube")
    //        {
    //            Debug.Log("Hit");
    //            collision.gameObject.GetComponent<CharacterControls>().HitPlayer(-hitDir * force, stunTime);
    //        }
    //        //audioSource.Play();
    //    }
    //}

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.CompareTag("Glass")) // làm v? kính
        {
            fallFlatScript = collision.gameObject.GetComponent<FallPlat>();
            fallFlatScript.destroyThis();
            Debug.Log("hit glass");
        }
        //if (collision.gameObject.CompareTag("BounceCube"))
        //{
        //    gameObject.GetComponent<CharacterControls>().HitPlayer(-MoveDir * 10, 0.5f);
        //    Debug.Log("hit");
        //}
        //Rigidbody rigidbody = collision.collider.attachedRigidbody;
        //if (rigidbody != null)
        //{
        //    Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
        //    forceDirection.y = 0;
        //    forceDirection.Normalize();
        //    rigidbody.AddForceAtPosition(-forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        //    Debug.Log("ttttt");
        //}

        if (collision.gameObject.CompareTag("Gift")) //tính s? ?i?m khi ch?i game
        {
            score += 1;
            scoreText.text = score.ToString() + " Gift";
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlaneOver")) //tính s? ?i?m khi game over
        {
            GameOver.Setup(score, "GAME OVER");
            txtScore.SetActive(false);

        }
        if (collision.gameObject.CompareTag("PlaneWin")) //ve dich
        {
            GameOver.Setup(score, "WIN GAME");
            txtScore.SetActive(false);
        }

    }
}