using UnityEngine;
using System.Collections;
using TMPro;

namespace MyFps
{
    /// <summary>
    /// 첫번째 트리거에 걸리면 트리거 시퀀스 실행
    /// </summary>
    public class BFirstTrigger : MonoBehaviour
    {
        #region Variables
        //플레이어 오브젝트
        public GameObject thePlayer;

        //화살표 오브젝트
        public GameObject theMarker;

        //시퀀스 텍스트
        public TextMeshProUGUI sequenceText;

        //시나리오 텍스트
        [SerializeField]
        private string sequence = "Looks like a weapon on that table";
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SequencePlay());
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            //0. 플레이 캐릭터 비활성화
            thePlayer.SetActive(false);
            //1. 대사 출력
            sequenceText.text = sequence;
            //2. 2초 딜레이
            yield return new WaitForSeconds(2f);
            //3. 화살표 활성화
            theMarker.SetActive(true);
            //4. 1초 딜레이
            yield return new WaitForSeconds(1f);

            //초기화
            sequenceText.text = "";     //텍스트 초기화
            this.transform.GetComponent<BoxCollider>().enabled = false;     //충돌체 비활성화

            //5. 플레이어 캐릭터 활성화
            thePlayer.SetActive(true);
        }
        #endregion
    }
}
