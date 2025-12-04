using UnityEngine;

namespace MyFps
{
    public class CreditUI : MonoBehaviour
    {
        #region Variables
        public GameObject mainmenuUI;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                HideCreditUI();
            }
        }
        #endregion

        #region Custom Method
        private void HideCreditUI()
        {
            mainmenuUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
        #endregion
    }
}
