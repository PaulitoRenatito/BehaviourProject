using UnityEngine;

namespace Animations
{
    public class AnimatorController : MonoBehaviour
    {
        
        private static readonly int SpeedParameterHash = Animator.StringToHash("Speed");
        [SerializeField] private Animator animator;
        
        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }
        
        public void SetSpeed(float speed) => animator.SetFloat(SpeedParameterHash, speed);
    }
}
