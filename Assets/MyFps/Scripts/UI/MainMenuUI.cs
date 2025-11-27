using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 메인메뉴 씬을 관리하는 클래스
    /// 메인메뉴 버튼 기능, 신페이더 기능
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        #region Vriables
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "PlayScene01";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //페이드 인 시작
            fader.FadeStart();

            //배경음 플레이
            AudioManager.Instance.Play("MenuMusic");

            //초기화

        }
        #endregion

        #region Custom Method
        //버튼 대응 함수 구현
        public void NewGame()
        {
            //버튼 효과음
            AudioManager.Instance.Play("ButtonHit");

            //탄환, 무기 초기화
            if (PlayerState.Instance != null)
            {
                PlayerState.Instance.ResetGame();
            }

            //Debug.Log("NewGame");
            //플레이 씬으로 이동
            fader.FadeTo(loadToScene);
        }

        public void LoadGame()
        {
            Debug.Log("LoadGame");
        }

        public void Options()
        {
            AudioManager.Instance.PlayBGM("SHAmb");
            Debug.Log("Options");
        }

        public void Credits()
        {
            Debug.Log("Credits");
        }

        public void QuitGame()
        {
            Debug.Log("QuitGame");
            Application.Quit();
        }
        #endregion
    }
}