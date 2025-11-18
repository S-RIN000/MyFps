using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 플레이어와 정면에 있는 오브젝트와의 거리 구하기
    /// </summary>
    public class PlayerCasting : MonoBehaviour
    {
        #region Variables
        //플레이어와 정면에 있는 오브젝트와의 거리
        public static float distanceFromTarget;

        [SerializeField]
        private float toTarget; //임시변수
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //오브젝트와의 거리 구하기 
            RaycastHit hit;     //hit 했을 때 hit 정보를 저장
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                //true = hit에 성공 => 정보 저장
                distanceFromTarget = hit.distance;
                toTarget = distanceFromTarget;
            }
        }
        //레이캐스트로 기즈모 그리기
        private void OnDrawGizmosSelected()
        {
            //레이캐스트 쏘는 거리
            float maxDistance = 100f;

            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance);

            Gizmos.color = Color.red;
            if(isHit)
            {
                Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            }
        }
        #endregion
    }
}
