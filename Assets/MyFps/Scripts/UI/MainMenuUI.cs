using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

        //UI
        public GameObject mainmenuUI;
        public GameObject optionUI;
        public GameObject creditUI;

        public GameObject loadGameButtonUI;
        //public Button loadGameButton;
        //public CanvasGroup loadButtonLayerGroup;

        //옵션 - 볼륨관리
        public AudioMixer audioMixer;

        //슬라이더
        public Slider bgmSlider;
        public Slider sfxSlider;

        //씬 번호
        private int sceneNumber = -1;

        //audioMixer, Playerprefs 파라미터
        private const string BGMVolume = "BGMVolume";
        private const string SFXVolume = "SFXVolume";
        private const string SceneNumber = "SceneNumber";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //저장된 데이터 불러와서 게임 데이터 초기화
            GameDataInit();
            
            //로드게임 버튼 셋팅
            if(sceneNumber < 0)
            {
                //loadGameButton.interactable = false;
                //loadButtonLayerGroup.alpha = 0.1f;
                loadGameButtonUI.SetActive(false);
            }
            else
            {
                loadGameButtonUI.SetActive(true);
            }

            //페이드 인 시작
            fader.FadeStart();

            //배경음 플레이
            AudioManager.Instance.Play("MenuMusic");

            //커서 초기화
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //초기화
        }
        private void Update()
        {
            
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

            //플레이 씬으로 이동
            fader.FadeTo(sceneNumber);
        }

        public void Options()
        {
            AudioManager.Instance.PlayBGM("SHAmb");
            //Debug.Log("Options");
            ShowOptionUI();
        }

        public void Credits()
        {
            //Debug.Log("Credits");            
            ShowCredit();
        }

        public void QuitGame()
        {
            //치팅 - 저장된 데이터 리셋
            PlayerPrefs.DeleteAll();

            Debug.Log("QuitGame");
            Application.Quit();
        }

        //옵션 보이기
        private void ShowOptionUI()
        {
            mainmenuUI.SetActive(false);
            optionUI.SetActive(true);
        }
        public void HideOptionUI()
        {
            //옵션 데이터 저장
            SaveOptions();

            //UI
            mainmenuUI.SetActive(true);
            optionUI.SetActive(false);

            //효과음
            AudioManager.Instance.Play("ButtonHit");
        }

        //옵션 배경음 볼륨 변경시 호출
        public void SetBGMVolume(float value)
        {
            //value값 저장
            //PlayerPrefs.SetFloat(BGMVolume, value);

            //믹서 적용
            audioMixer.SetFloat(BGMVolume, value);
        }
        //옵션 효과음 볼륨 변경시 호출
        public void SetSFXVolume(float value)
        {
            //value값 저장
            //PlayerPrefs.SetFloat(SFXVolume, value);

            //믹서 적용
            audioMixer.SetFloat(SFXVolume, value);
        }

        //옵션 데이터 저장하기
        private void SaveOptions()
        {
            Debug.Log("Save Option Data");

            Debug.Log($"bgmVolume : {bgmSlider.value}");
            Debug.Log($"sfxVolume : {sfxSlider.value}");

            //볼륨
            PlayerPrefs.SetFloat(BGMVolume, bgmSlider.value);            
            PlayerPrefs.SetFloat(SFXVolume, sfxSlider.value);

            //기타 옵션값
            //...
        }

        //옵션 데이터 불러오기
        public void LoadOptions()
        {
            Debug.Log("Load Option Data");

            //배경음 볼륨값
            float bgmVolume = PlayerPrefs.GetFloat(BGMVolume, 0f);
            Debug.Log($"bgmVolume : {bgmVolume}");
            
            audioMixer.SetFloat(BGMVolume, bgmVolume);  //믹서 적용            
            bgmSlider.value = bgmVolume;                //UI 적용          

            //효과음 볼륨값
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolume, 0f);
            Debug.Log($"sfxVolume : {sfxVolume}");
            
            audioMixer.SetFloat(SFXVolume, sfxVolume);  //믹서 적용            
            sfxSlider.value = sfxVolume;                //UI 적용      

            //기타 옵션값
            //...

        }

        //크레딧 보여주기
        public void ShowCredit()
        {
            mainmenuUI.SetActive(false);
            creditUI.SetActive(true);
        }

        private void GameDataInit()
        {
            //옵션 데이터
            LoadOptions();

            //플레이 데이터
            //sceneNumber = PlayerPrefs.GetInt(SceneNumber, -1);  //-1 : 저장된 데이터 없음, 씬번호는 0부터 시작함
            //PlayerPrefs.SetInt(SceneNumber, sceneNumber);
            //Debug.Log($"Save SceneNumber : {sceneNumber}");
            SaveLoad.SaveData();
        }
        #endregion
    }
}