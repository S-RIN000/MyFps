using TMPro;
using UnityEngine;

namespace MyFps
{
    public class ActionUI : MonoBehaviour
    {
        #region Variables
        //인터렉티브 UI
        [Header("Interactive UI")]
        //크로스헤어
        public GameObject extraCross;

        //액션 UI
        public GameObject actionUI;
        public TextMeshProUGUI actionText;
        #endregion

        #region Custom Method
        public virtual void ShowActionUI(string action)
        {
            extraCross.SetActive(true);
            actionUI.SetActive(true);
            actionText.text = action;
        }

        public virtual void HideActionUI()
        {
            extraCross.SetActive(false);
            actionUI.SetActive(false);
            actionText.text = "";
        }
        #endregion
    }
}
