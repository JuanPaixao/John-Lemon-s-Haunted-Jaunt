using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 movement;
    public bool hasHorizontalInput, hasVerticalInput, isWalking;
    public float horizontalMov, verticalMov, rotSpeed;
    private Animator _animator;
    public Quaternion rotation = Quaternion.identity;
    private Rigidbody _rb;
    private AudioSource _audioSource;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMov = Input.GetAxis("Horizontal");
        verticalMov = Input.GetAxis("Vertical");
        movement.Set(horizontalMov, 0f, verticalMov);
        movement.Normalize();
        hasHorizontalInput = !Mathf.Approximately(horizontalMov, 0f);
        hasVerticalInput = !Mathf.Approximately(verticalMov, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
        _animator.SetBool("IsWalking", isWalking);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, rotSpeed * Time.deltaTime, 0);
        rotation = Quaternion.LookRotation(desiredForward);

        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        OnAnimatorMove();
    }
    private void OnAnimatorMove()
    {
        _rb.MovePosition(_rb.position + movement * _animator.deltaPosition.magnitude);
        _rb.MoveRotation(rotation);
    }
}
