using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class GExitTrigger : MonoBehaviour
    {
        #region Variables
        //참조 : 충돌체
        private BoxCollider collider;

        //씬 이동
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "MainMenu";

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
            //배경음 종료
            AudioManager.Instance.StopBGM();

            yield return new WaitForSeconds(0.1f);

            fader.FadeTo(loadToScene);
            Debug.Log($"Go To {loadToScene}");
            //PlayerState.Instance.SetWeaponType(WeaponType.Pistol);
        }
        #endregion
        
    }
}
