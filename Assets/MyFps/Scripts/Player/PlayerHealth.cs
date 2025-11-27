using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using TMPro;

namespace MyFps
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Variables
        //체력
        private float health;
        [SerializeField]
        private float maxHealth = 20f;

        //healthUI
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI maxHealthText;

        //죽음체크
        private bool isDeath = false;

        //데미지 입을 때 등록된 함수 호출
        public UnityAction onDamage;
        //죽었을 때 호출되는 함수 호출
        public UnityAction onDie;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            health = maxHealth;
        }
        private void Update()
        {
            if (health > 0.1f)
            {
                healthText.text = health.ToString();
            }
            else if(health <0.1)
            {
                healthText.text = "0";
            }

            maxHealthText.text = maxHealth.ToString();
        }
        #endregion

        #region Custom Method
        public void TakeDamage(float damage)
        {
            health -= damage;
            Debug.Log($"Player Health : {health}");

            //데미지 이펙트
            //player.OnDamage();
            onDamage?.Invoke();

            if (health <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //죽음 처리
        private void Die()
        {
            //Debug.Log("게임오버");
            onDie?.Invoke();
        }
        #endregion
    }
}
