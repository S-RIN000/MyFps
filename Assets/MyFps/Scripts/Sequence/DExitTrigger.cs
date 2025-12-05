using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class DExitTrigger : MonoBehaviour
    {
        #region Variables
        //참조 : 충돌체
        private BoxCollider collider;

        //시퀀스
        public Door door;

        //사운드
        public AudioSource bgm02;

        //씬 이동
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "NextScene";
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조 : 충돌체
            collider = GetComponent<BoxCollider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SequencePlay());

            //충돌체 비활성화 (or Kill)
            collider.enabled = false;
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            //문열기
            door.Activate();

            //배경음 정지
            bgm02.Stop();

            //씬 종료시 구현 내용
            //게임 데이터 저장
            SavePlayData();           

            yield return new WaitForSeconds(0.1f);

            fader.FadeTo(loadToScene);
            //Debug.Log($"Go To {loadToScene}");
            //PlayerState.Instance.SetWeaponType(WeaponType.Pistol);
        }

        private void SavePlayData()
        {
            //저장할 데이터 세팅
            PlayerState.Instance.SetSceneName(loadToScene);
            SaveLoad.SaveData();
        }
        #endregion 
    }
}
