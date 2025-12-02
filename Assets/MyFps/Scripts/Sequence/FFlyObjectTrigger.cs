using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 트리거에 걸리면 액티브 오브젝트를 이용하여 컵을 날린다 
    /// </summary>
    public class FFlyObjectTrigger : MonoBehaviour
    {
        #region Variables
        //참조 : 충돌체
        private Collider collider;

        public GameObject thePlayer;
        public GameObject activeObject;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            collider=GetComponent<Collider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SequencePlay());

            //충돌체 비활성화
            collider.enabled = false;
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            thePlayer.SetActive(false);
            activeObject.SetActive(true);
            yield return new WaitForSeconds(2f);

            activeObject.SetActive(false);
            thePlayer.SetActive(true);
        }
        #endregion
    }
}
