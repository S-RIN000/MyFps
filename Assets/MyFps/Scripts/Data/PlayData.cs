using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 플레이 데이터 정의
    /// </summary>
    public class PlayData
    {
        public int sceneNumber;     //씬 번호
        public int ammoCount;       //탄환 갯수
        public int weaponType;      //무기 종류

        //...

        //생성자
        public PlayData()
        {
            //초기화
            sceneNumber = PlayerState.Instance.SceneNumber;
            ammoCount = PlayerState.Instance.AmmoCount;
            weaponType = (int)PlayerState.Instance.WeaponType;
        }
    }
}
