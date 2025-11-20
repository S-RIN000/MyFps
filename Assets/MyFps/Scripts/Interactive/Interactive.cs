using TMPro;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 인터렉티브 오브젝트를 관리하는 클래스들의 부모 추상 클래스 
    /// </summary>
    public abstract class Interactive : MonoBehaviour
    {
        //추상 메서드
        #region abstract
        protected abstract void DoAction();
        #endregion

        #region Variables
        //참조
        protected BoxCollider collider;

        //인터렉티브 UI
        [Header ("Interactive UI")]
        //크로스헤어
        public GameObject extraCross;

        //액션 UI
        public GameObject actionUI;
        public TextMeshProUGUI actionText;

        //액션 텍스트
        [SerializeField]
        protected string action = "Do Action";

        //인터렉티브 액션
        #endregion

        #region Unity Event Method
        protected virtual void Awake()
        {
            //참조
            collider = GetComponent<BoxCollider>();
        }
        protected virtual void OnMouseOver()
        {
            //일정거리 이상이 되면 UI숨김
            if (PlayerCasting.distanceFromTarget > 2f)
            {
                HideActionUI();
                return;
            }
            ShowActionUI();

            //만약 Action 버튼을 누르면
            if (Input.GetButtonDown("Action"))
            {
                //"Do Action" - 인터렉티브 액션
                DoAction();

                //충돌체 제거
                collider.enabled = false;
                HideActionUI();
            }
        }
        protected virtual void OnMouseExit()
        {
            HideActionUI();
        }
        #endregion

        #region Custom Method
        protected virtual void ShowActionUI()
        {
            extraCross.SetActive(true);
            actionUI.SetActive(true);
            actionText.text = action;
        }

        protected virtual void HideActionUI()
        {
            extraCross.SetActive(false);
            actionUI.SetActive(false);
            actionText.text = "";
        }
        #endregion
    }
}
