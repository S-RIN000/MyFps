using UnityEngine;

namespace MyFps
{
    public class PickupRightEye : PickupItem
    {
        protected override void DoAction()
        {
            Debug.Log("오른쪽 눈 줍기");
            PlayerState.Instance.GetInPuzzleItem(PuzzleItem.RightEye);

            //아이템 킬
            Destroy(gameObject);
        }
    }
}
