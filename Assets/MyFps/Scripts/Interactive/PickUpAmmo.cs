using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// AmmoBox 획득하기
    /// </summary>
    public class PickUpAmmo : Interactive
    {
        #region Variables
        [SerializeField]
        private int giveAmmo = 7;   //Ammo 지급 갯수

        public GameObject table;
        #endregion

        #region Unity Evemt Method
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            //Debug.Log("탄환 7개를 얻었습니다");
            PlayerState.Instance.AddAmmo(giveAmmo);

            //아이템 킬
            Destroy(gameObject);
            Destroy(table);
        }
        #endregion
    }
}
