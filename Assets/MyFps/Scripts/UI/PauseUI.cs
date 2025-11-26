using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// PausedUI를 관리하는 클래스
    /// </summary>
    public class PauseUI : MonoBehaviour
    {
        #region Variables
        public GameObject pausedUI;

        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "MainMenu";

        private GameObject thePlayer;

        //키 설정 UI
        public GameObject settingUI;

        private float lastClickTime = 0f; // 마지막 클릭 시간
public float clickCooldown = 0.3f; // 쿨타임 (0.3초)
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            thePlayer = FindFirstObjectByType<PlayerMove>().gameObject;
        }
        private void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.Escape) && thePlayer.activeSelf == true)
            {
                Toggle();
            }
            
        }
        #endregion

        #region Custom Method
        private void Toggle()
        {
            pausedUI.SetActive(!pausedUI.activeSelf);

            if (pausedUI.activeSelf)
            {
                Time.timeScale = 0.0f;
                //플레이어 인풋기능 제거
                thePlayer.GetComponent<CharacterInput>().enabled = false;

                //마우스 커서 초기화(in UI 화면)
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                thePlayer.GetComponent<CharacterInput>().enabled = true;

                //마우스 커서 초기화(in 플레이 화면)
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                settingUI.SetActive(false);
            }    
        }

        public void Continue()
        {
            Toggle();
        }
        public void MainMenu()
        {
            //Time.timeScale = 0.0f;
            //fader.FadeTo(loadToScene);
            Debug.Log("Go To Menu");
        }

        public void SettingUI()
        {
            settingUI.SetActive(!settingUI.activeSelf);
        }
        #endregion
    }
}
