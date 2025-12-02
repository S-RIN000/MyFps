using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// ∆€¡Ò æ∆¿Ã≈€ ¡›±‚
    /// </summary>
    public class PickupPuzzleItem : PickupItem
    {
        #region Vriables
        //»πµÊ«“ ∆€¡Ò æ∆¿Ã≈€
        [SerializeField]
        private PuzzleItem puzzleItem = PuzzleItem.None;
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            //∆€¡Ò æ∆¿Ã≈€ »πµÊ
            bool isGain = PlayerState.Instance.GetInPuzzleItem(puzzleItem);

            if(isGain)
            {
                //æ∆¿Ã≈€ ≈≥
                Destroy(gameObject);
            }
        }
        #endregion
    }
}
