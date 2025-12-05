using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    /// <summary>
    /// 플레이씬 시작하면 시작하자마자 현재 씬 번호를 파일 저장한다
    /// </summary>
    public class AutoSave : MonoBehaviour
    {

        //PlayerPrefs 파라미터
        private const string SceneNumber = "SceneNumber";

        private void Start()
        {
            //씬 시작하자마자 데이터 저장
            SaveData();
        }

        //데이터 저장하기
        private void SaveData()
        {
            PlayData playData = SaveLoad.LoadData();
            PlayerState.Instance.PlayerStatsIntialize(playData);

            //저장된 번호 가져오기
            //int saveNumber = PlayerPrefs.GetInt(SceneNumber, -1);
            int saveNumber = PlayerState.Instance.SceneNumber;

            //씬 번호 저장
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            if (saveNumber <= sceneNumber)
            {
                //저장
                //PlayerPrefs.SetInt(SceneNumber, sceneNumber);
                //Debug.Log($"Save SceneNumber : {sceneNumber}");
                PlayerState.Instance.SetSceneNumber(sceneNumber);
                SaveLoad.SaveData();
            }
            else
            {
                //새로 게임 시작 데이터를 강제로 세팅(?)
                PlayerState.Instance.PlayerStatsInit();
            }
        }
    }
}
