using UnityEngine;

namespace MySample
{
    /// <summary>
    /// Ãæµ¹Ã¼ÀÇ Collision Ãæµ¹ Ã¼Å©
    /// </summary>
    public class CollisionTest : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"OnCollisionEnter : {collision.gameObject.name}");
            //¿ÞÂÊÀ¸·Î Èû (200f)
            MoveObject moveObject = collision.gameObject.GetComponent<MoveObject>();
            if(moveObject)
            {
                moveObject.MoveLeft();
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            Debug.Log($"OnCollisionStay : {collision.gameObject.name}");
        }
        private void OnCollisionExit(Collision collision)
        {
            Debug.Log($"OnCollisionExit : {collision.gameObject.name}");
            //¿ÞÂÊÀ¸·Î Èû (200f)
            MoveObject moveObject = collision.gameObject.GetComponent<MoveObject>();
            if (moveObject)
            {
                moveObject.MoveLeft();
            }
        }
    }
}
