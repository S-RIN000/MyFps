using UnityEngine;
using System.IO;    //파일 열고 닫기
using System.Runtime.Serialization.Formatters.Binary;   //이진화 하기 위해 필요한 네임스페이스

namespace MyFps
{
    /// <summary>
    /// 게임 플레이 데이터 (파일 시스템으로) 저장, 불러오기 - 이진화 저장
    /// </summary>
    public static class SaveLoad
    {
        //파일 저장하기
        public static void SaveData()
        {
            //Debug.Log("Save Game Data");

            //파일 이름, 저장 경로 지정
            //▼ 유니티에서 지정한 경로
            string path = Application.persistentDataPath + "/pData.arr";
            
            //저장할 데이터를 이진화 준비
            BinaryFormatter formatter = new BinaryFormatter();

            //파일 접근 : 존재하면 파일 가져오고 없으면 새로 만들고 => FileMode.Create
            FileStream fs = new FileStream(path, FileMode.Create);  

            //저장할 데이터를 준비
            PlayData playData = new PlayData();
            Debug.Log($" save sceneName : {playData.sceneName}");

            //준비한 데이터를 이진화 저장
            formatter.Serialize(fs, playData);      //playData를 이진화해서 fs에 저장

            //파일 클로즈
            fs.Close();
        }


        //파일 불러오기
        public static PlayData LoadData()
        {
            //Debug.Log("Load Game Data");

            //파일에서 데이터를 로드해서 저장할 변수
            PlayData playData;

            //파일 이름, 저장 경로 지정
            //▼ 유니티에서 지정한 경로
            string path = Application.persistentDataPath + "/pData.arr";

            //지정된 경로에 파일이 있는지 없는지 체크
            if(File.Exists(path) == true)
            {
                //파일 있음
                //저장할 데이터를 역(이)진화 준비
                BinaryFormatter formatter = new BinaryFormatter();

                //파일 접근 : 
                FileStream fs = new FileStream(path, FileMode.Open);

                //이진화 해서 저장된 데이터를 역(이)진화해서 PlayData형식으로 읽어온다
                playData = formatter.Deserialize(fs) as PlayData;

                Debug.Log($" load sceneName : {playData.sceneName}");

                //파일 클로즈
                fs.Close();
            }
            else
            {
                //파일 없음    
                Debug.Log($"cannot found load file");
                playData = null;
            }

            return playData;
        }
    }
}
