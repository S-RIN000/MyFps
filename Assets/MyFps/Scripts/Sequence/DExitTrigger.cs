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

            yield return new WaitForSeconds(1f);

            //fader.FadeTo(loadToScene);
            Debug.Log($"Go To {loadToScene}");
        }
        #endregion 
    }
}
