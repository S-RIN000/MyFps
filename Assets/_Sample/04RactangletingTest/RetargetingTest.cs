using UnityEngine;

namespace Sample
{
    public class RetargetingTest : MonoBehaviour
    {
        #region Variables
        //ÂüÁ¶
        private Animator animator;

        private const string Jump = "Jump";
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger(Jump);
            }
        }
        #endregion
    }
}
