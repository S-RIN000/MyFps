using UnityEngine;
using TMPro;

namespace MyFps
{
    /// <summary>
    /// UI - ammoCount 갯수 보여주기
    /// </summary>
    public class DrawAmmoCount : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI ammoCountText;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //ammo UI
            ammoCountText.text = PlayerState.Instance.AmmoCount.ToString();
        }
        #endregion

        #region Custom Method
        #endregion
    }
}
