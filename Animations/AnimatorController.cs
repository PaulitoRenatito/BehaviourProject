using System.Linq;
using UnityEngine;

namespace Animations
{
    public class AnimatorController : MonoBehaviour
    {
        
        private static readonly int SpeedParameterHash = Animator.StringToHash("Speed");
        [SerializeField] private Animator animator;
        
        private AnimationClip[] clips;
        
        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            
            clips = animator.runtimeAnimatorController.animationClips;
        }
        
        public void SetSpeed(float speed) => animator.SetFloat(SpeedParameterHash, speed);
        
        public void SetSpeed(float speed, float clipSpeed)
        {
            animator.SetFloat(SpeedParameterHash, speed);
            animator.speed = clipSpeed;
        }
        
        private AnimationClip GetAnimationClipByHash(int stateNameHash)
        {
            return clips.FirstOrDefault(clip => Animator.StringToHash(clip.name) == stateNameHash);
        }
    }
}
