using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// ammo UI 그리기
    /// 손에 총을 들고 있으면 show ammo UI, 손에 총이 없으면 hide ammo UI 
    /// </summary>
    public class DrawAmmoUI : MonoBehaviour
    {
        #region Variables

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //Ammo UI 그리기
            bool isShow = PlayerState.Instance.WeaponType != WeaponType.None;
            ShowAmmoUI(isShow);
        }
        #endregion

        #region Custom Method
        public void ShowAmmoUI(bool isShow)
        {
            gameObject.SetActive(isShow);
        }
        #endregion
    }
}
