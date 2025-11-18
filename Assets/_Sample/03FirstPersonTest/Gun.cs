using UnityEngine;

namespace MySample
{
    /// <summary>
    /// F키를 누르면 탄환 발사
    /// </summary>
    public class Gun : MonoBehaviour
    {
        #region Variables
        public Transform firePoint;
        public GameObject bulletPrefab;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Shoot();
            }
        }
        #endregion

        #region Custom Method
        void Shoot()
        {
            GameObject bullotGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            Bullet bullet = bullotGo.GetComponent<Bullet>();
            if(bullet != null )
            {
                bullet.MoveForce();
            }

            //지금처럼 유도탄이 아닌, 직선으로 발사되는 탄환이라면 이 식이 들어가야한다 (노리던 타겟이 사라져서 탄환 잉여가 남아있는 것 방지용)
            Destroy(bullotGo, 3f);
        }
        #endregion
    }
}
