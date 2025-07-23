using Core.Other;
using UnityEngine;


namespace Content.Features.EnemyAnimator.Scripts
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform objectToFlip;
        [SerializeField] private float rotationRight = 0;
        [SerializeField] private float rotationLeft = 180;
        [SerializeField] private string movingBoolName = "";
        [SerializeField] private Rigidbody2D rb;

        private void LateUpdate()
        {
            AnimateEnemy();
        }

        private void AnimateEnemy()
        {
            if (animator == null || rb == null || objectToFlip == null)
            {
                return;
            }

            float horizontalSpeed = rb.velocity.x;
            
            animator.SetBool(movingBoolName, Mathf.Abs(horizontalSpeed) > ConstantsHolder.ENEMY_ANIMATOR_FLOAT_NUMBER);
            
            if (horizontalSpeed > ConstantsHolder.ENEMY_ANIMATOR_FLOAT_NUMBER)
                transform.rotation = Quaternion.Euler(0, rotationRight, 0);
            else if (horizontalSpeed < -ConstantsHolder.ENEMY_ANIMATOR_FLOAT_NUMBER)
                transform.rotation = Quaternion.Euler(0, rotationLeft, 0);
        }
    }
}