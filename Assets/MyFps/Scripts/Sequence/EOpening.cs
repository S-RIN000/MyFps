using UnityEngine;
using System.Collections;
using TMPro;

namespace MyFps
{
    /// <summary>
    /// 플레이02씬의 오프닝 연출
    /// 페이드인 효과, 배경음 플레이, 시퀀스 텍스트 초기화
    /// </summary>
    public class EOpening : MonoBehaviour
    {
        #region Variables
        //씬 페이더
        public SceneFader fader;

        //플레이어 오브젝트
        public GameObject thePlayer;

        //시퀀스 텍스트
        public TextMeshProUGUI sequenceText;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            StartCoroutine(SequencePlay());
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            //0. 플레이 캐릭터 비활성화
            thePlayer.SetActive(false);

            //1. 페이드인 연출(1초)
            fader.FadeStart();

            //2. 텍스트 초기화
            sequenceText.text = "";

            //3. 배경음 플레이
            AudioManager.Instance.PlayBGM("BGM01");
            
            //4. 1초 대기 후 플레이 캐릭터 활성화
            yield return new WaitForSeconds(1f);
            thePlayer.SetActive(true);
        }
        #endregion
    }
}