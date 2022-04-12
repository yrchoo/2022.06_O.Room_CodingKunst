using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 2f;
    public float runSpeed = 3f;
    public float finalSpeed;
    public bool run;
    public float m_jumpForce = 4.0f;
    private bool m_wasGrounded;
    private bool m_isGrounded = true;

    Animator _animator;
    Camera _camera;
    CharacterController _controller;

    public bool toggleCameraRotation; // 둘러보기
    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Grounded", m_isGrounded);
        InputMovement();
        CheckToggleKey();
        CheckRun();
        JumpingAndLanding();
        m_wasGrounded = m_isGrounded;
    }

    void LateUpdate() {
        if(toggleCameraRotation != true) {
            Vector3 playerRotation = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), Time.deltaTime * smoothness);
        }
    }

    void CheckToggleKey()
    {
        if(Input.GetKey(KeyCode.LeftAlt)) {
            toggleCameraRotation = true;
        } else {
            toggleCameraRotation = false;
        }
    }

    void CheckRun()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {
            run = true;
        } else {
            run = false;
        }
    }
    void InputMovement()
    {
        float gravity = 20.0f;
        finalSpeed = (run) ? runSpeed : speed;
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
        Vector3 m_velocity = moveDirection.normalized;

        if (Input.GetButtonDown("Jump"))
        {
                m_velocity.y = m_jumpForce;
        }

        m_velocity.y -= gravity * Time.deltaTime;
        _controller.Move(m_velocity * finalSpeed * Time.deltaTime);
        
        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
        m_isGrounded = _controller.isGrounded;
    }
    void JumpingAndLanding()
    {
        if (!m_wasGrounded && m_isGrounded)
        {
            _animator.SetTrigger("Land");
            _animator.ResetTrigger("Jump");
        }
        if (!m_isGrounded && m_wasGrounded)
        {
            _animator.SetTrigger("Jump");
            _animator.ResetTrigger("Land");
        }
    }
}
