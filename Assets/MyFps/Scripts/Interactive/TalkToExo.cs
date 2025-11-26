using UnityEngine;

namespace MyFps
{
    public class TalkToExo : Interactive
    {
        #region Vriables
        //대화창 UI
        public GameObject dialogueUI;
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            //대화창 나왔다가 사라지기
            dialogueUI.SetActive(true);
            
            Destroy(dialogueUI, 1.5f);
        }
        #endregion
    }
}
