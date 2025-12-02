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
            bool isLeft = PlayerState.Instance.HavePuzzleItem(PuzzleItem.LeftEye);
            bool isRight = PlayerState.Instance.HavePuzzleItem(PuzzleItem.RightEye);

            //퍼즐 조각 맞추기
            if (isLeft)
            {
                leftEye.SetActive(true);               
            }

            if (isRight)
            {
                rightEye.SetActive(true);                
            }
       
            //모든 퍼즐 조각을 다 맞추었는지 체크
            if (isLeft && isRight)
            {
                collider.enabled = false;
                
                yield return new WaitForSeconds(0.8f);
                doorSwitch.SetActive(true);
            }
            else  //실패
            {
                sequenceText.text = "you need more eye";
                yield return new WaitForSeconds(0.8f);

                sequenceText.text = "";

                //모두 맞추는 것에 실패했을 때만 충돌체 복구 
                collider.enabled = true;
            }
           
        }
        #endregion
    }
}
