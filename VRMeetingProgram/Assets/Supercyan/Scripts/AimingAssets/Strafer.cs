using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utiliy;

public class Strafer : MonoBehaviour, IInitializable, IPunObservable
{
    // ---
    private Vector3 direction;
    private PhotonView pv;
    private Transform tr;
    private Animator anim;
    private Vector3 currPos;
    private Quaternion currRot;
    private bool currGrounded = true;

    public void Initialize(GameObject character)
    {
        m_animator = character.GetComponent<Animator>();
        m_rigidBody = character.GetComponent<Rigidbody>();
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;

    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    public Animator Animator { set { m_animator = value; } }
    public Rigidbody Rigidbody { set { m_rigidBody = value; } }

    public bool IsDead { get; set; }
    public bool IsZombie { get; set; }

    public bool CanMove = true;

    RaycastHit hit;

    #region "Collision"

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
            if (m_collisions.Count == 0) { m_isGrounded = false; }
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
    #endregion

    IEnumerator Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        yield return new WaitForSeconds(0.5f);

        if (pv.IsMine)
        {
            Camera.main.GetComponentInParent<CameraMovement>().objectTofollow = transform.Find("AimPoint").transform;
            //Camera.main.GetComponent<SmoothFollow>().target = transform.Find("AimPoint").transform;
        }
        else {
            m_rigidBody.isKinematic = true;
        }
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            if (!m_jumpInput && Input.GetKey(KeyCode.Space) && CanMove)
            {
                if (Physics.Raycast(transform.position, -transform.up, out hit, 0.6f))
                {
                    m_jumpInput = true;
                }

            }
        }
        
    }

    private void FixedUpdate()
    {
        if (pv.IsMine)
        {
            m_animator.SetBool("Grounded", m_isGrounded);

            if (!IsDead) { DirectUpdate(); }

            m_jumpInput = false;
        }
        
    }

    private void DirectUpdate()
    {
        if (!CanMove) { return; }

        if(pv.IsMine) 
        {
            
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            // 메인 카메라의 transform에 접근
            Transform camera = Camera.main.transform;
            //Vector3 forward = transform.TransformDirection(Vector3.forward);
            //Vector3 right = transform.TransformDirection(Vector3.right);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                v *= m_walkScale;
                h *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);


            // transform.right -> x축
            direction = camera.right * m_currentH + camera.forward * m_currentV;
            //Vector3 direction = forward * v + right * h;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                m_rigidBody.MovePosition(m_rigidBody.position + m_currentDirection * m_moveSpeed * Time.deltaTime);
            }

            Vector3 animationDirection = Quaternion.Inverse(transform.rotation) * direction;
            m_animator.SetFloat("MoveHorizontal", animationDirection.x);
            m_animator.SetFloat("MoveVertical", animationDirection.z);

            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput && !IsZombie)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }
        }
        else // pv.IsMine이 아닐 때
        {
            Vector3 animationDirection = Quaternion.Inverse(transform.rotation) * direction;
            m_animator.SetFloat("MoveHorizontal", animationDirection.x);
            m_animator.SetFloat("MoveVertical", animationDirection.z);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //sending Datas...
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
            //currGrounded = true; 
        } else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
            //currGrounded = (bool)stream.ReceiveNext();
        }
    }
}
