using UnityEngine;

namespace MyFps
{
    //손에 든 무기 타입 enum 정의
    public enum WeaponType
    {
        None = 0,   //무기 없음
        Pistol,
        HealMatic,

    }

    //퍼즐 아이템 enum 정의
    public enum PuzzleItem
    {
        None = 0,
        Key01,
        LeftEye,
        RightEye,
        MaxPuzzleItem
    }

    /// <summary>
    /// 플레이어의 데이터를 관리하는 클래스
    /// 모든 씬에서 계속 데이터를 유지 관리 
    /// </summary>
    public class PlayerState : PersistantSingleton<PlayerState>
    {
        #region Variables
        //탄환 갯수
        private int ammoCount;

        //소지 무기 타입
        private WeaponType weaponType;

        //퍼즐 아이템 획득여부
        [SerializeField]
        private bool[] puzzleItems;

        #endregion

        #region Property
        public int AmmoCount { get { return ammoCount; } }
        public WeaponType WeaponType { get { return weaponType; } }
        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();

            //플레이어 데이터 초기화, 치팅
            ammoCount = 0;
            weaponType = WeaponType.None;

            puzzleItems = new bool[(int)PuzzleItem.MaxPuzzleItem];

            //TO DO : cheating(=>2번씬에서 총 보이게)
            weaponType = WeaponType.Pistol;
        }
        #endregion

        #region Custom Method
        //ammoCount 추가하기
        public void AddAmmo(int amount)
        {
            ammoCount += amount;
            Debug.Log($"AmmoCount : {ammoCount}");
        }

        //ammoCount 사용하기
        public bool UseAmmo(int amount = 1)
        {
            if (ammoCount < amount)
            {
                Debug.Log("You need to reload");
                return false;
            }

            ammoCount -= amount;
            Debug.Log($"AmmoCount : {ammoCount}");
            return true;
        }

        //무기 획득(교체)
        public void SetWeaponType(WeaponType type)
        {
            weaponType = type;
        }

        //[]매개변수로 입력 받은 퍼즐 아이템 획득 여부
        public bool HavePuzzleItem(PuzzleItem puzzleItem)
        {
            //아이템이 없다
            if (puzzleItem == PuzzleItem.None || puzzleItem == PuzzleItem.MaxPuzzleItem)
            {
                Debug.Log("out of range");
                return false;
            }
            return puzzleItems[(int)puzzleItem];
        }

        //[]매개변수로 입력 받은 퍼즐 아이템 획득, 성공/실패 처리
        public bool GetInPuzzleItem(PuzzleItem puzzleItem)
        {
            //획득 실패
            if(puzzleItem == PuzzleItem.None || puzzleItem == PuzzleItem.MaxPuzzleItem)
                return false;

            //획득 성공
            puzzleItems[(int)puzzleItem] = true;
            return true;
        }
        //탄환, 무기 초기화
        public void ResetGame()
        {
            ammoCount = 0;
            weaponType = WeaponType.None;
        }
        #endregion
    }
}