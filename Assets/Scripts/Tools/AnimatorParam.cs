using UnityEngine;

namespace Tools
{
    public static class AnimatorParam
    {
        //Machine
        public static readonly int LeverOn = Animator.StringToHash("LeverOn");
        public static readonly int Flip = Animator.StringToHash("Flip");
        //Player
        public static readonly int Move = Animator.StringToHash("Move");
        public static readonly int Direction = Animator.StringToHash("Direction");
    }
}