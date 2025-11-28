using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 아이템 줍기 - 탄환 7개 지급
    /// </summary>
    public class PickupAmmo : PickUp
    {
        #region Variables
        //탄환 지급 갯수
        [SerializeField]
        private int giveAmmo = 7;

        protected override bool OnPickup()
        {
            PlayerState.Instance.AddAmmo(giveAmmo);
            return true;
        }
        #endregion
    }
}
