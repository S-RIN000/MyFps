using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 마우스 움직임에 따른 플레이어 시야 구현
    /// </summary>
    public class MouseLook : MonoBehaviour
    {
        #region Variables
        //카메라 트래킹 오브젝트
        public Transform cameraRoot;

        //참조
        private CharacterInput _input;

        //회전
        [SerializeField]
        private float rotationSpeed = 1.0f;

        //마우스 움직임 보정값
        [SerializeField]
        private float sensivity = 100f;

        private float cameraTargetPitch = 0f;   //카메라 회전 연산 값 (위, 아래)
        private float rotationVelocity;         //카메라 회전 속도 (좌, 우)

        //
        private float topClamp = 45f;
        private float bottomClamp = -90f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            _input = GetComponent<CharacterInput>();
        }
        private void Start()
        {
            //마우스 커서 초기화
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void LateUpdate()
        {
            CameraRotate();
        }
        #endregion

        #region Custom Method
        private void CameraRotate()
        {
            //입력값 체크
            if (_input.Look.sqrMagnitude < 0.01f)
                return;

            cameraTargetPitch -= _input.Look.y * rotationSpeed * Time.deltaTime * sensivity;
            rotationVelocity = _input.Look.x * rotationSpeed * Time.deltaTime * sensivity;

            //위, 아래 회전 연산
            cameraTargetPitch = ClampAngle(cameraTargetPitch, bottomClamp, topClamp);
            cameraRoot.localRotation = Quaternion.Euler(cameraTargetPitch, 0f, 0f);

            //좌우 연산
            transform.Rotate(Vector3.up * rotationVelocity);
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
        #endregion
    }
}
