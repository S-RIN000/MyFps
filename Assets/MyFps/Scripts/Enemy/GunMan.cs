using UnityEngine;
using UnityEngine.AI;

namespace MyFps
{
    //적 공통 상태 정의
    public enum Enemystate
    {
        E_Idle,     //대기
        E_Walk,     //패트롤
        E_Chase,    //추격
        E_Attack,   //공격        
        E_Death     //죽임
    }


    /// <summary>
    /// GunMan Enemy를 관리하는 클래스
    /// </summary>
    public class GunMan : MonoBehaviour, IDamageable
    {
        #region Variables
        //참조
        private Animator animator;
        private NavMeshAgent agent;
        private Transform thePlayer;

        //건맨의 현재 상태
        [SerializeField]
        private Enemystate currentState;
        //바로 이전 상태
        private Enemystate beforeState;

        //체력
        private float health;
        [SerializeField]
        private float maxHealth = 20f;      //최대 체력

        //죽음체크
        private bool isDeath = false;
        [SerializeField]
        private float destroyDelay = 6f;

        //상태 - 대기
        private float idleTimer = 2f;   //2 ~ 3f
        private float countdown = 0f;

        //상태 - 패트롤       
        [SerializeField]
        private bool isPatrol = false;         //패트롤 여부

        public Transform[] wayPoints;
        private int wayPointIndex = 0;

        //처음 생성 위치
        private Vector3 startPosition = Vector3.zero;

        //상태 - 추격
        [SerializeField]
        private float detectDistance = 10f;     //적이 추격거리 안에 들어오면 추격 시작

        //상태 공격
        [SerializeField]
        private float attackRange = 5f;         //적이 사거리 안에 들어오면 추격을 멈추고 공격 시작        
        [SerializeField]
        private float attackTimer = 2f;         //2초에 한번씩 발사
        [SerializeField]
        private float attackDamage = 5f;        //발사시 플레이어에게 attackDamage(5) 

        private bool isBack = false;

        //애니메이터 파라미터
        const string MoveSpeed = "MoveSpeed";
        const string IsDeath = "IsDeath";
        const string Fire = "Fire";

        #endregion

        #region Property

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            thePlayer = FindFirstObjectByType<Player>().transform;
        }

        private void Start()
        {
            //초기화
            health = maxHealth;
            wayPointIndex = 1;  //0번 위치에서 시작하기 때문에 ...
            startPosition = transform.position;
            
            SetState(Enemystate.E_Idle);       
        }

        private void Update()
        {
            //죽음 체크
            if (isDeath)
                return;

            //디텍팅
            float distance = Vector3.Distance(thePlayer.position, transform.position);
            if(distance <=attackRange && isBack == false)          //공격 거리 체크
            {
                SetState(Enemystate.E_Attack);
            }
            else if(distance <= detectDistance && isBack == false) //디텍팅 거리 체크
            {
                SetState(Enemystate.E_Chase);
            }

            //상태 처리
            switch(currentState)
            {
                case Enemystate.E_Idle:
                    if (isPatrol)
                    {
                        countdown += Time.deltaTime;
                        if (countdown >= idleTimer)
                        {
                            //타이머 기능
                            SetState(Enemystate.E_Walk);
                        }
                    }                   
                    break;

                case Enemystate.E_Walk: //패트롤               
                    //agent.remainingDistance : 도착지점까지 남은 거리
                    //wayPoints[1].position 으로 이동 -> 도착하면 대기상태로 전환 -> wayPoints[2].position 으로 이동 -> 도착하면 대기상태로 전환 -> ...     

                    //도착판정
                    if(agent.remainingDistance < 0.1f)
                    {
                        if (isPatrol)
                        {
                            wayPointIndex++;
                            if (wayPointIndex >= wayPoints.Length)
                            {
                                wayPointIndex = 0;
                            }
                        }
                        SetState(Enemystate.E_Idle);
                    }
                    break;

                case Enemystate.E_Chase:
                    agent.SetDestination(thePlayer.position);

                    if(distance > detectDistance)       //디텍팅 거리 체크, 타겟을 잃어버리면
                    {                                                
                        SetState(Enemystate.E_Walk);                        
                    }
                    break;

                case Enemystate.E_Attack:
                    countdown += Time.deltaTime;
                    if (countdown >= attackTimer)
                    {
                        //타이머 기능
                        Shoot();

                        //타이머 초기화
                        countdown = 0f;
                    }

                    if (distance > attackRange)
                    {
                        SetState(Enemystate.E_Walk);
                    }
                    break;
            }

            //애니메이터 파라미터 처리
            animator.SetFloat(MoveSpeed, agent.velocity.magnitude);
        }

        //디텍팅 거리 기즈모 그리기
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, detectDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, attackRange);
        }
        #endregion

        #region Custom Method
        //상태 전환
        public void SetState(Enemystate newState)
        {
            //현재 상태 체크
            if(currentState == newState) return;

            beforeState  = currentState;    //현재 상태를 이전 상태에 전환
            currentState = newState;        //현재 상태를 새로운 상태로 전환

            //agent 초기화
            agent.ResetPath();

            //상태별 초기값 설정
            switch (currentState)
            {
                case Enemystate.E_Idle:
                    //타이머 초기화
                    idleTimer = Random.Range(2f, 3f);
                    break;

                case Enemystate.E_Walk:
                    //이동 목표지점
                    if (isPatrol)
                    {                        
                        agent.SetDestination(wayPoints[wayPointIndex].position);
                    }
                    else
                    {
                        agent.SetDestination(startPosition);
                    }
                    break;

                case Enemystate.E_Attack:
                    //멈춤 - 이동 목표 지점을 현재위치로 지정
                    agent.SetDestination(this.transform.position);                    
                    break;

                case Enemystate.E_Death:
                    animator.SetBool(IsDeath,true);
                    break;
            }

            if (currentState == Enemystate.E_Chase || currentState == Enemystate.E_Attack)
            {
                animator.SetLayerWeight(1, 1.0f);
            }
            else
            {
                animator.SetLayerWeight(1, 0f);
            }
                //타이머 초기화 (상태가 바뀌면 무조건 초기화)
                countdown = 0f;
        }

        //데미지 주기
        public void TakeDamage(float damage)
        {
            health -= damage;
            
            //효과 : Vfx, Sfx, UI, 애니메이션 ...
            //체력이 0이 되면 죽는거지만 게임을 하는 유저는 위의 효과들로 알아차린다 (피드백)

            if(health <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //죽음
        private void Die()
        {
            isDeath = true;

            SetState(Enemystate.E_Death);

            //효과 : Vfx, Sfx, UI, 애니메이션 ...

            //보상

            //킬
            Destroy(gameObject, destroyDelay);
        }

        //공격
        private void Shoot()
        {
            //애니메이션
            animator.SetTrigger(Fire);

            //효과 : Vfx, Sfx, UI, 애니메이션 ...

            //타겟에게 데미지 주기
            IDamageable damageable = thePlayer.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }

        //문이 열릴때 호출되는 함수
        public void OnActive()
        {
            isBack = false;
        }

        //문이 닫힐 때 호출되는 함수
        //문이 닫히면 추격을 멈추고 돌아가라
        public void GoBack()
        {
            isBack = true;
            SetState(Enemystate.E_Walk);
        }

        #endregion
    }
}
