using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 플레이어의 Weapon을 관리하는 클래스
    /// 무기 교체 ...
    /// </summary>
    public class PlayerWeaponManager : MonoBehaviour
    {
        #region Variables
        public GameObject pistol;
        public GameObject healMatic;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //현재 무장 무기 세팅
            SetCurrentWeapon(PlayerState.Instance.WeaponType);
        }
        #endregion

        #region Custom Method
        private void SetCurrentWeapon(WeaponType weaponType)
        {
            pistol.SetActive(false);
            healMatic.SetActive(false);

            if(weaponType == WeaponType.Pistol)
            {
                pistol.SetActive(true);
            }
            else if (weaponType == WeaponType.HealMatic)
            {
                healMatic.SetActive(true) ;
            }
        }
        #endregion
    }
}
