using System;
using System.Diagnostics;

namespace Assets.scenes.common_scripts
{
    public static class AnimationProtocol
    {
        public static string GetName(AnimationStatesEnum anima)
        {
            switch (anima)
            {
                case AnimationStatesEnum.Idle:
                    return "idle";
                case AnimationStatesEnum.Borning:
                    return "born";
                case AnimationStatesEnum.Moving:
                    return "move";
                case AnimationStatesEnum.Attacking:
                    return "attack";
                //case AnimationStatesEnum.Damaged:
                //    return "damaged";
                case AnimationStatesEnum.Dying:
                    return "dying";
                default:
                    throw new ArgumentOutOfRangeException("anima", anima, null);
            }

            return string.Empty;
        }

        public static float GetTimeDuration(AnimationStatesEnum borning)
        {
            switch (borning)
            {
                case AnimationStatesEnum.Idle:
                    return 0f;
                case AnimationStatesEnum.Borning:
                    return 1f;
                case AnimationStatesEnum.Attacking:
                    return 1f;
                //case AnimationStatesEnum.Damaged:
                //    return 0.3f;
                case AnimationStatesEnum.Dying:
                    return 1f;
                default:
                    throw new Exception("GetTimeDuration(" + borning + ").");
            }
        }
    }


    public enum AnimationStatesEnum
    {
        Idle,
        Borning,
        Moving,
        Attacking,
        //Damaged,
        Dying,
    }
}
