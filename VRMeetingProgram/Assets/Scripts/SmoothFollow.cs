using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Utiliy{
    public class SmoothFollow : MonoBehaviour
    {
        // The target we are following
        [SerializeField]
        public Transform target; 

        public float sensitivity = 50f;
        public float clampAngle = 70f;

        // The distance in the x-z plane to the target
        [SerializeField]
        private float distance = 10.0f;

        // The height we want the camera to be above the target
        [SerializeField]
        private float height = 5.0f;

        [SerializeField]
        private float rotationDamping;
        [SerializeField]
        private float heightDamping;

        private float rotX;
        private float rotY;


        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // 마우스
            Cursor.visible = false;
        }
        void Update()
        {
            rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime; // 마우스 상하
            //rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // 마우스 좌우

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // 좌우 범위
            Quaternion rot = Quaternion.Euler(rotX, rotY, 0); // z축 0
            transform.rotation = rot;
        }
        // Update is called once per frame
        void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis 
            // LerpAngle rotationDamping * Time.delta시간동안 a부터 b까지 변경되는 각도를 반환. 부드러운 회전을 위해서 사용.
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height // 고도를 낮추다.?
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // 
            transform.LookAt(target);
        }
    }
}

