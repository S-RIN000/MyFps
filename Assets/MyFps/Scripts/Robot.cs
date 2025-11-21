using UnityEngine;

namespace MyFps
{
    //로봇 상태 정의
    public enum RobotState
    {
        R_Idle,     //0
        R_Walk,     //1
        R_Attack,   //2
        R_Death     //3
    }

    /// <summary>
    /// 로봇을 관리하는 클래스 
    /// 애니메이션, 체력, 이동 . . .
    /// </summary>
    public class Robot : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;

        //public AudioSource audioSource;     //enemy 등장 사운드

        //로봇의 현재 상태
        [SerializeField]
        private RobotState robotState;
        //바로 이전 상태
        private RobotState beforeState;

        private float health;
        [SerializeField]
        private float maxHealth = 20f;      //체력

        //죽음체크
        private bool isDeath = false;

        [SerializeField]
        private float attackDamage = 5f;    //공격력
        [SerializeField]
        private float attackRange = 2.0f;   //공격 범위
        [SerializeField]
        private float attackDelay = 2f;

        //플레이어 오브젝트
        public Transform thePlayer;

        [SerializeField]
        private float moveSpeed = 5f;       //이동 속도
        private float rotateSpeed = 5;      //회전 속도

        //애니메이션 파라미터
        private const string EnemyState = "EnemyState";

        /*
        [SerializeField]
        private float moveSpeed = 5f;       //이동 속도
        
        [SerializeField]
        private float attackDelay = 2f;     //공격 간격
        
        [SerializeField]
        private float attackDamage = 5f;    //공격력
        
        [SerializeField]
        private float attackRange = 1.5f;   //공격 범위
        */
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            SetState(RobotState.R_Idle);
            health = 20f;
        }
        private void Update()
        {
            //상태 구현
            switch (robotState)
            {
                case RobotState.R_Idle:
                    break;
                case RobotState.R_Walk:
                    break;
                case RobotState.R_Attack:
                    break;
                case RobotState.R_Death:
                    break;
            }

            if (thePlayer == null)
                return;

            //타겟까지 이동하기
            Vector3 dir = thePlayer.position - transform.position;
            //남은 거리
            float distance = Vector3.Distance(thePlayer.position, transform.position);
            //이번 프레임에 이동한 거리
            float distanceThisFrame = Time.deltaTime * moveSpeed * rotateSpeed;
            if(distance <= attackRange)
            {
                AttackDamage(attackDamage);
                return;
            }
                transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);

            //플레이어 방향으로 바라보기
            transform.LookAt(thePlayer);
        }
        
        #endregion

        #region Custom Method
        //로봇의 상태 변경
        private void SetState(RobotState newState)
        {
            //현재 상태 체크 (현재상태와 새로 들어온 상태가 같으면 적용할 필요 없음)
            if(newState == robotState)
                { return; }

            //이전 상태 저장
            beforeState = robotState;

            //새로운 상태로 변경
            robotState = newState;

            //새로운 상태 변경에 따른 구현 내용
            animator.SetInteger(EnemyState, (int)robotState);
        }

        //데미지 주기
        public void TakeDamage(float damage)
        {
            health -= damage;
            Debug.Log($"로봇 체력 : {health}");

            //죽음 체크 - 두 번 죽이지 마라
            if(health <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //플레이어에게 데미지 주기
        public void AttackDamage(float damage)
        {
            Debug.Log($"attackDamage : {attackDamage}");
        }

        //죽음 처리
        private void Die()
        {
            isDeath = true;

            //Death 상태 변경
            SetState(RobotState.R_Death);
        }
        #endregion
    }
}
