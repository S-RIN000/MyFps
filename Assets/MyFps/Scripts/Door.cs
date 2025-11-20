using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 문(door) 열기/닫기
    /// </summary>
    public class Door : MonoBehaviour, ISwitchable
    {
        #region Variables
        //참조
        protected Animator animator;    //Door를 상속받으면 쓸 수 있게 접근제한자를 protected로 설정

        protected bool isActive;

        //사운드
        //public AudioSource audioSource;

        //애니메이터 파라미터
        const string IsOpen = "IsOpen";
        #endregion

        #region Property
        public bool IsActive
        {
            get { return isActive; }
            set 
            { 
                isActive = value; 
                animator.SetBool(IsOpen, value);

                //사운드 플레이
            }
        }
        #endregion

        #region Unity Event Method
        protected virtual void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }
        #endregion

        #region Custom Method
        public void Activate()
        {
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
        }
        #endregion
    }
}