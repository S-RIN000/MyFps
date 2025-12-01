using MyFps;
using UnityEngine;

namespace MyFps
{
    public class PickupKey : PickupItem
    {
        protected override void DoAction()
        {
            Debug.Log("아이템 획득을 구현해야 합니다");
            PlayerState.Instance.GetInPuzzleItem(PuzzleItem.Key01);

            //아이템 킬
            Destroy(gameObject);
        }
    }
}
