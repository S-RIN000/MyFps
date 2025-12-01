using TMPro;
using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class Frame : Interactive
    {
        #region Variables
        public GameObject leftEye;
        public GameObject rightEye;

        public GameObject doorSwitch;

        public TextMeshProUGUI sequenceText;    //조각 부족 체크 텍스트
        #endregion

        #region Unity Event Method
       
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(MatchPuzzle());
        }

        IEnumerator MatchPuzzle()
        {
            if (PlayerState.Instance.havePuzzleItem(PuzzleItem.LeftEye))
            {
                leftEye.SetActive(true);
                collider.enabled = true;
            }

            if (PlayerState.Instance.havePuzzleItem(PuzzleItem.RightEye))
            {
                rightEye.SetActive(true);
                collider.enabled = true;
            }
            else
            {
                sequenceText.text = "you need more eye";
                yield return new WaitForSeconds(0.8f);

                sequenceText.text = "";
            }

            if (PlayerState.Instance.havePuzzleItem(PuzzleItem.LeftEye) && PlayerState.Instance.havePuzzleItem(PuzzleItem.RightEye))
            {
                collider.enabled = false;
                
                yield return new WaitForSeconds(0.8f);
                doorSwitch.SetActive(true);
            }
           
        }
        #endregion
    }
}
