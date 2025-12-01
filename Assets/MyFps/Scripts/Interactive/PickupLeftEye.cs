using UnityEngine;

namespace MyFps
{
    public class PickupLeftEye : PickupItem
    {
        protected override void DoAction()
        {
            Debug.Log("왼쪽 눈 줍기");
            PlayerState.Instance.GetInPuzzleItem(PuzzleItem.LeftEye);

            //아이템 킬
            Destroy(gameObject);
        }
    }
}
