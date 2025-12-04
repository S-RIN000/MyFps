using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //PlayerPrefs 파라미터
        private const string SceneNumber = "SceneNumber";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //시작하자마자 데이터 저장
            SaveData();

            //시작하자마자 오프닝 연출
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

        //데이터 저장하기
        private void SaveData()
        {
            //저장된 번호 가져오기
            int saveNumber = PlayerPrefs.GetInt(SceneNumber, -1);
           
            //씬 번호 저장
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            if (sceneNumber > saveNumber)
            {
                //PlayerPrefs.SetInt(SceneNumber, sceneNumber);
                //Debug.Log($"Save SceneNumber : {sceneNumber}");
                SaveLoad.SaveData();
            }
        }
        #endregion
    }
}