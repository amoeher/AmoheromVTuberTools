using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UniVRM10;

namespace Amoherom
{

    [System.Serializable]
    public class VrmBlendShapeBehaviour : PlayableBehaviour
    {
        public enum ExpressionPreset
        {
            custom,
            happy,
            angry,
            sad,
            relaxed,
            surprised,
            aa, 
            ih, 
            ou, 
            ee, 
            oh, 
            blink,
            blinkLeft,
            blinkRight,
            lookUp,
            lookDown,
            lookLeft,
            lookRight,
            neutral,

            BrowAngry,
            BrowFun,
            BrowJoy,
            BrowSorrow,
            BrowSurprised,

            EyeNatural,
            EyeAngry,
            EyeFun,
            EyeJoy,
            EyeJoyRight,
            EyeJoyLeft,
            EyeSorrow,
            EyeSurprised,
            EyeSpread,
            EyeIrisHide,
            EyeHighlightHide,

            MouthClose,
            MouthUp,
            MouthDown,
            MouthSmall,
            MouthLarge,
            MouthNeutral,
            MouthFun,
            MouthJoy,
            MouthAngry,
            MouthSorrow,
            MouthSurprised,
            MouthSkinFung,
            MouthSkinFungRight,
            MouthSkinFungLeft,

            HAHide,
            HAFung1,
            HAFung1Low,
            HAFung1Up,
            HAFung2,
            HAFung2Low,
            HAFung2Up,
            HAFung3,
            HAFung3Up,
            HAFung3Low,
            HAShort,
            HAShortUp,
            HAShortLow,

            // ARKit Complete Blendshapes
            browInnerUp,
            browDownLeft,
            browDownRight,
            browOuterUpLeft,
            browOuterUpRight,
            eyeLookUpLeft,
            eyeLookUpRight,
            eyeLookDownLeft,
            eyeLookDownRight,
            eyeLookInLeft,
            eyeLookInRight,
            eyeLookOutLeft,
            eyeLookOutRight,
            eyeBlinkLeft,
            eyeBlinkRight,
            eyeSquintLeft,
            eyeSquintRight,
            eyeWideLeft,
            eyeWideRight,
            cheekPuff,
            cheekSquintLeft,
            cheekSquintRight,
            noseSneerLeft,
            noseSneerRight,
            jawOpen,
            jawForward,
            jawLeft,
            jawRight,
            mouthFunnel,
            mouthPucker,
            mouthLeft,
            mouthRight,
            mouthRollUpper,
            mouthRollLower,
            mouthShrugUpper,
            mouthShrugLower,
            mouthClose_arkit,
            mouthSmileLeft,
            mouthSmileRight,
            mouthFrownLeft,
            mouthFrownRight,
            mouthDimpleLeft,
            mouthDimpleRight,
            mouthUpperUpLeft,
            mouthUpperUpRight,
            mouthLowerDownLeft,
            mouthLowerDownRight,
            mouthPressLeft,
            mouthPressRight,
            mouthStretchLeft,
            mouthStretchRight,
            tongueOut,

        }

        [System.Serializable]
        public struct BlendShapeEntry
        {
            public ExpressionPreset preset;
            [Range(-1f, 1f)]
            public float value;
        }

        public List<BlendShapeEntry> blendShapes = new List<BlendShapeEntry>
    {
        new BlendShapeEntry { preset = ExpressionPreset.neutral, value = 1f }
    };
    }
}