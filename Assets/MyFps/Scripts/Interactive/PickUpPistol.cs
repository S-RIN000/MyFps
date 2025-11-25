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

        public GameObject ammoUI;

        public WeaponType weaponType = WeaponType.Pistol;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            realGun.SetActive(false);
            //ammoUI.SetActive(false);
        }
        #endregion

        #region Custom Method
        //Interactive Action
        protected override void DoAction()
        {
            //오른손에 총 활성화
            realGun.SetActive(true);
            //테이블 위 가짜총 비활성화
            fakeGun.SetActive(false);
            //가이드 화살표 비활성화
            theMarker.SetActive(false);

            //현재 소지무기 세팅
            PlayerState.Instance.SetWeaponType(weaponType);

            //탄환 UI 활성화
            ammoUI.SetActive(true);

            //아이템 킬
            Destroy(gameObject);
        }

        #endregion
    }
}
