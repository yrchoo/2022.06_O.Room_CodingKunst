using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbody : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody m_rigidBody;
    private bool m_wasGrounded;
    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();
    public float m_moveSpeed = 2.0f;
    public float m_jumpForce = 4.0f;
    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }
    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);
        PlayerMove();
        JumpingAndLanding();
        m_wasGrounded = m_isGrounded;
    }
    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveHorizontal = Vector3.right * h;
        Vector3 moveVertical = Vector3.forward * v;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized;
        transform.LookAt(transform.position + velocity);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity *= 2.0f;
        }
        transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
        //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);
        m_animator.SetFloat("MoveSpeed", velocity.magnitude);

    }
    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;
        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }
        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }
        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }
        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0)
            {
                m_isGrounded = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }
}