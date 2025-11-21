using MyFps;
using MySample;
using UnityEngine;

namespace MySample2
{
    public enum ShootType
    {
        Shoot_Projectile,
        Shoot_Raycast
    }

    public class ShootTest : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;       //무기 애니메이션
        public Transform firePoint;     //파이어 포인트

        //슛타입
        [SerializeField]
        private ShootType shootType = ShootType.Shoot_Projectile;

        //발사체 프리팹
        public GameObject bulletPrefab;

        //무기 사거리
        [SerializeField]
        private float attackRange = 200f;
        //연사 방지 (딜레이시간 : 0.7초)

        //무기 공격력
        [SerializeField]
        private float attackDamage = 5f;

        //이펙트 효과
        public GameObject hitImpactPrefab;
        //public AudioSource pistolShoot;

        [SerializeField]
        private float impacForce = 10f;

        //

        //애니메이션 파라미터
        private const string IsShoot = "IsShoot";
        #endregion

        #region Property
        public bool IsFire
        {
            get
            {
                return animator.GetBool(IsShoot);
            }
        }
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //발사 버튼 입력처리, 연사 방지
            if (Input.GetButtonDown("Fire") && IsFire == false)
            {
                if(shootType == ShootType.Shoot_Projectile)
                {
                    ShootProjectile();
                }
                else
                {
                    ShootRaycast();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            /*
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackRange);

            if(isHit)
            {
                //충돌체까지 레이저 그리기 
                Gizmos.DrawRay(firePoint.position, firePoint.forward * hit.distance);
            }
            else
            {
                //attackRange까지 레이저 그리기
                Gizmos.DrawRay(firePoint.position, firePoint.forward * attackRange);
            }
            */
            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, attackRange);

            if (isHit)
            {
                //충돌체까지 레이저 그리기 
                Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            }
            else
            {
                //attackRange까지 레이저 그리기
                Gizmos.DrawRay(transform.position, transform.forward * attackRange);
            }
        }
        #endregion

        #region Custom Method
        //Projectile 발사 처리
        private void ShootProjectile()
        {
            GameObject bullotGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Bullet bullet = bullotGo.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.MoveForce();
            }

            //지금처럼 유도탄이 아닌, 직선으로 발사되는 탄환이라면 이 식이 들어가야한다 (노리던 타겟이 사라져서 탄환 잉여가 남아있는 것 방지용)
            Destroy(bullotGo, 3f);
        }

        //Raycast 발사 처리
        private void ShootRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                //이펙트 효과(VFX)
                if (hitImpactPrefab)
                {
                    GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    //이펙트 킬 예약
                    Destroy(effectGo, 2f);
                }

                //임팩트 물리 효과 - 리디지바디 체크 (맞을때 움찔)
                if(hit.rigidbody) //hit 한 곳에 리디지바디가 있으면
                {
                    hit.rigidbody.AddForce(-hit.normal * impacForce, ForceMode.Impulse);
                }

                //적(들)에게 데미지 주기
                /*
                Zombie zombie = hit.transform.GetComponent<Zombie>();
                if (zombie)
                {
                    zombie.TakeDamage(attackDamage);
                }

                Slime slime = hit.transform.GetComponent<Slime>();
                if (slime)
                {
                    slime.TakeDamage(attackDamage);
                }

                Skeleton skeleton = hit.transform.GetComponent<Skeleton>();
                if (skeleton)
                {
                    skeleton.TakeDamage(attackDamage);
                }
                */

                /*
                Monster monster = hit.transform.GetComponent<Monster>();
                if(monster)
                {
                    monster.TakeDamage(attackDamage);
                }
                */

                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }
            }

            //애니메이션
            animator.SetBool(IsShoot, true);

            //이펙트 효과(SFX)
            /*
            if (pistolShoot)
            {
                pistolShoot.Play();
            }
            */
        }
        #endregion
    }
}
