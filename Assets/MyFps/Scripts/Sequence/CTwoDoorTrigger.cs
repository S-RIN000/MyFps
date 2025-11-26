using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class CTwoDoorTrigger : MonoBehaviour
    {
        #region Variables
        //참조 : 충돌체
        private BoxCollider collider;

        //시퀀스
        public Door door;

        //사운드
        public AudioSource bgm01;
        public AudioSource bgm02;

        public GameObject robot;

        //경고 UI
        public GameObject warningUI;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조 : 충돌체
            collider = GetComponent<BoxCollider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            SequencePlay();
            //충돌체 비활성화 (or Kill)
            collider.enabled = false;
            StartCoroutine(HideUI());
        }
        #endregion

        #region Custom Method
        private void SequencePlay()
        {
            bgm01.Stop();
            bgm02.Play();

            door.Activate();
            robot.SetActive(true);
        }

        IEnumerator HideUI()
        {
            warningUI.SetActive(true);

            yield return new WaitForSeconds(2.8f);
            warningUI.SetActive(false);

        }
        #endregion
    }
}
