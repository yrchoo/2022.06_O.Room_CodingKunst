using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public Transform objectTofollow;

    public float followSpeed = 10f;
    public float sensitivity = 50f;
    public float clampAngle = 70f;


    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    
    // Start is called before the first frame update
    void Start()
    {

        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized; // 정규화
        finalDistance = realCamera.localPosition.magnitude;
        Cursor.lockState = CursorLockMode.Locked; // 마우스
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime; // 마우스 상하
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // 마우스 좌우

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // 좌우 범위
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0); // z축 0
        transform.rotation = rot;
    }
    void LateUpdate() // ������Ʈ�� ���� ������ ����Ǵ� �Լ�
    {
        if (!objectTofollow)
                return;

        // 현재 위치, 목표 위치, 속도
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, finalDir, out hit)) // 카메라가 바라보는 대상일 경우
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        } else
        {
            finalDistance = maxDistance;
        }
        // 메인 카메라의 위치 설정
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
