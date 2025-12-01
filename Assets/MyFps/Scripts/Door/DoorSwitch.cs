using UnityEngine;
using System.Collections;
using TMPro;

namespace MyFps
{
    /// <summary>
    /// 등록된 문의 열기, 닫기 구현
    /// 인터렉티브 액션으로 이벤트 구현, 인터렉티브 상속 받는다 
    /// </summary>
    public class DoorSwitch : Interactive
    {
       
        #region Variables
        public Door door;       //문닫기, 열기 할 문 게임 오브젝트

        public Renderer renderer;           //자식 컴퍼넌트라서 겟컴퍼넌트x public o
        private Material originMaterial;    //오리진 컬러
        public Material closeMaterial;      //빨간색

        public TextMeshProUGUI sequenceText;    //열쇠 부족 체크 텍스트

        [SerializeField]
        private PuzzleItem needKey = PuzzleItem.None;   //퍼즐 아이템 변수
        #endregion

        #region Unity Event Method
        private void OnEnable()
        {
            door.OnActivate += DoorOpen;
            door.OnDeactivate += DoorClose;
        }
        private void OnDisable()
        {
            door.OnActivate -= DoorOpen;
            door.OnDeactivate -= DoorClose;
        }
        protected void Start()
        {
            //초기화
            originMaterial = renderer.material;
        }
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(Toggle());
        }

        IEnumerator Toggle()
        {
            //열쇠 체크
            if (needKey == PuzzleItem.None || PlayerState.Instance.havePuzzleItem(needKey))
            {
                //문 열고 닫기
                if (door.IsActive)
                {
                    door.Deactivate();
                }
                else
                {
                    door.Activate();
                }
            }
            else
            {
                sequenceText.text = "You Need Key";
                //Debug.Log("You need key");
                yield return new WaitForSeconds(2f);
                sequenceText.text = "";
            }

            //충돌체 복구 (1초)
            yield return new WaitForSeconds(1f);
            collider.enabled = true;
        }

        void DoorOpen()
        {            
                action = "Close the Door";
                renderer.material = closeMaterial;       
        }
        void DoorClose()
        {
                action = "Open the Door";
                renderer.material = originMaterial;       
        }
        #endregion
    }
}