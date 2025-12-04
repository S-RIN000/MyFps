using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

namespace MyFps
{
    /// <summary>
    /// 플레이씬의 오프닝 연출
    /// </summary>
    public class AOpening : MonoBehaviour
    {
        #region Variables
        //페이더 효과
        public SceneFader fader;

        //플레이어 오브젝트
        public GameObject thePlayer;
        
        //시퀀스 텍스트
        public TextMeshProUGUI sequenceText;

        //시나리오 텍스트
        [SerializeField]
        private string sequence01 = "...Where am I?";

        [SerializeField]
        private string sequence02 = "I need to get out of here";

        //사운드
        public AudioSource line01;      //시퀀스01
        public AudioSource line02;      //시퀀스02

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
        //오프닝 시퀀스 연출
        IEnumerator SequencePlay()
        {
            //0. 플레이 캐릭터 비활성화
            thePlayer.SetActive(false);
            //1. 페이드인 연출 (1초 대기 후 페이드인 효과) - 2초
            fader.FadeStart(2+3f);

            //2. 화면 하단에 시나리오 텍스트 화면 출력 (3초)
            sequenceText.text = sequence01;
            line01.Play();  
            //3. 3초 후에 시나리오 텍스트 삭제
            yield return new WaitForSeconds(3f);

            //4. 화면 하단에 시나리오 텍스트 화면 출력 (3초)
            sequenceText.text = sequence02;
            line02.Play();
            //3. 3초 후에 시나리오 텍스트 삭제
            yield return new WaitForSeconds(3f);

            sequenceText.text = "";
            
            //4. 플레이 캐릭터 활성화
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
                //저장
                //PlayerPrefs.SetInt(SceneNumber, sceneNumber);
                //Debug.Log($"Save SceneNumber : {sceneNumber}");
                SaveLoad.SaveData();
            }
        }
        #endregion
    }
}
