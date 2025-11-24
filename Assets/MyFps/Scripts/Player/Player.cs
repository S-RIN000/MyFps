using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 플레이어를 관리(제어)하는 클래스
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region Variables
        //참조
        private PlayerHealth playerHealth;

        //데미지 이펙트
        public GameObject damageUI;

        //데미지 사운드
        public AudioSource hurt01;
        public AudioSource hurt02;
        public AudioSource hurt03;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            playerHealth = GetComponent<PlayerHealth>();
        }
        private void OnEnable()
        {
            //데미지/죽음 이벤트 함수에 등록
            playerHealth.onDamage += OnDamage;   
            playerHealth.onDie += OnDie;
        }
        private void OnDisable()
        {
            //데미지/죽음 이벤트 함수에서 제거
            playerHealth.onDamage -= OnDamage;
            playerHealth.onDie -= OnDie;
        }
        #endregion

        #region Custom Method
        //데미지 입을 때 호출되는 함수
        private void OnDamage()
        {
            StartCoroutine(DamageEffect());
        }

        IEnumerator DamageEffect()
        {
            //화면 전체 빨간색 플래시 효과
            damageUI.SetActive(true);


            //데미지 사운드 3개 중 1개 랜덤 발생
            

            yield return new WaitForSeconds(1.0f);
            damageUI.SetActive(false);
        }

        //죽었을 때 호출되는 함수
        public void OnDie()
        {
            //게임오버 씬으로 이동 
            Debug.Log("게임오버 씬으로 이동");
        }
        #endregion
    }
}
