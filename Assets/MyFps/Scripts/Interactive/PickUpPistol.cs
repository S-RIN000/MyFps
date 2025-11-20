using TMPro;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 피스톨 아이템 획득하기
    /// </summary>
    public class PickUpPistol : Interactive
    {
        #region Variables
        //액션
        [Header("Interactive Action")]
        public GameObject fakeGun;
        public GameObject realGun;

        public GameObject theMarker;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            realGun.SetActive(false);
        }
        #endregion

        #region Custom Method
        //Interactive Action
        protected override void DoAction()
        {
            realGun.SetActive(true);
            fakeGun.SetActive(false);
            theMarker.SetActive(false);
        }

        #endregion
    }
}
