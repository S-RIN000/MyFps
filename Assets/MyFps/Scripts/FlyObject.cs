using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 충돌시 충돌 상대 속도가 1 이상이면 충돌 사운드 재생
    /// </summary>
    public class FlyObject : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            //상대속도가 1 이상이면
            if(collision.relativeVelocity.magnitude > 1.0f)
            {
                AudioManager.Instance.Play("DoorBang");
            }
        }
    }
}
