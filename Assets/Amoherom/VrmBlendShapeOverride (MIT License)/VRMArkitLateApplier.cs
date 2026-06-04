using UnityEngine;

namespace Amoherom
{
    [DefaultExecutionOrder(20000)]
    public class VrmArkitLateApplier : MonoBehaviour
    {
        private SkinnedMeshRenderer arkitMesh;

        // 52 ARKit weights (0..1)
        [Range(0, 1)] public float browInnerUp;
        [Range(0, 1)] public float browDownLeft;
        [Range(0, 1)] public float browDownRight;
        [Range(0, 1)] public float browOuterUpLeft;
        [Range(0, 1)] public float browOuterUpRight;
        [Range(0, 1)] public float eyeLookUpLeft;
        [Range(0, 1)] public float eyeLookUpRight;
        [Range(0, 1)] public float eyeLookDownLeft;
        [Range(0, 1)] public float eyeLookDownRight;
        [Range(0, 1)] public float eyeLookInLeft;
        [Range(0, 1)] public float eyeLookInRight;
        [Range(0, 1)] public float eyeLookOutLeft;
        [Range(0, 1)] public float eyeLookOutRight;
        [Range(0, 1)] public float eyeBlinkLeft;
        [Range(0, 1)] public float eyeBlinkRight;
        [Range(0, 1)] public float eyeSquintLeft;
        [Range(0, 1)] public float eyeSquintRight;
        [Range(0, 1)] public float eyeWideLeft;
        [Range(0, 1)] public float eyeWideRight;
        [Range(0, 1)] public float cheekPuff;
        [Range(0, 1)] public float cheekSquintLeft;
        [Range(0, 1)] public float cheekSquintRight;
        [Range(0, 1)] public float noseSneerLeft;
        [Range(0, 1)] public float noseSneerRight;
        [Range(0, 1)] public float jawOpen;
        [Range(0, 1)] public float jawForward;
        [Range(0, 1)] public float jawLeft;
        [Range(0, 1)] public float jawRight;
        [Range(0, 1)] public float mouthFunnel;
        [Range(0, 1)] public float mouthPucker;
        [Range(0, 1)] public float mouthLeft;
        [Range(0, 1)] public float mouthRight;
        [Range(0, 1)] public float mouthRollUpper;
        [Range(0, 1)] public float mouthRollLower;
        [Range(0, 1)] public float mouthShrugUpper;
        [Range(0, 1)] public float mouthShrugLower;
        [Range(0, 1)] public float mouthClose;
        [Range(0, 1)] public float mouthSmileLeft;
        [Range(0, 1)] public float mouthSmileRight;
        [Range(0, 1)] public float mouthFrownLeft;
        [Range(0, 1)] public float mouthFrownRight;
        [Range(0, 1)] public float mouthDimpleLeft;
        [Range(0, 1)] public float mouthDimpleRight;
        [Range(0, 1)] public float mouthUpperUpLeft;
        [Range(0, 1)] public float mouthUpperUpRight;
        [Range(0, 1)] public float mouthLowerDownLeft;
        [Range(0, 1)] public float mouthLowerDownRight;
        [Range(0, 1)] public float mouthPressLeft;
        [Range(0, 1)] public float mouthPressRight;
        [Range(0, 1)] public float mouthStretchLeft;
        [Range(0, 1)] public float mouthStretchRight;
        [Range(0, 1)] public float tongueOut;

        private void Awake()
        {
            foreach (var m in GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                if (m?.sharedMesh == null) continue;
                if (m.sharedMesh.GetBlendShapeIndex("cheekPuff") >= 0 &&
                    m.sharedMesh.GetBlendShapeIndex("mouthClose") >= 0)
                {
                    arkitMesh = m;
                    break;
                }
            }
        }

        private void LateUpdate() => Apply();
        private void OnWillRenderObject() => Apply();

        private void Apply()
        {
            if (!Application.isPlaying || arkitMesh == null || arkitMesh.sharedMesh == null) return;

            Set("browInnerUp", browInnerUp);
            Set("browDownLeft", browDownLeft);
            Set("browDownRight", browDownRight);
            Set("browOuterUpLeft", browOuterUpLeft);
            Set("browOuterUpRight", browOuterUpRight);
            Set("eyeLookUpLeft", eyeLookUpLeft);
            Set("eyeLookUpRight", eyeLookUpRight);
            Set("eyeLookDownLeft", eyeLookDownLeft);
            Set("eyeLookDownRight", eyeLookDownRight);
            Set("eyeLookInLeft", eyeLookInLeft);
            Set("eyeLookInRight", eyeLookInRight);
            Set("eyeLookOutLeft", eyeLookOutLeft);
            Set("eyeLookOutRight", eyeLookOutRight);
            Set("eyeBlinkLeft", eyeBlinkLeft);
            Set("eyeBlinkRight", eyeBlinkRight);
            Set("eyeSquintLeft", eyeSquintLeft);
            Set("eyeSquintRight", eyeSquintRight);
            Set("eyeWideLeft", eyeWideLeft);
            Set("eyeWideRight", eyeWideRight);
            Set("cheekPuff", cheekPuff);
            Set("cheekSquintLeft", cheekSquintLeft);
            Set("cheekSquintRight", cheekSquintRight);
            Set("noseSneerLeft", noseSneerLeft);
            Set("noseSneerRight", noseSneerRight);
            Set("jawOpen", jawOpen);
            Set("jawForward", jawForward);
            Set("jawLeft", jawLeft);
            Set("jawRight", jawRight);
            Set("mouthFunnel", mouthFunnel);
            Set("mouthPucker", mouthPucker);
            Set("mouthLeft", mouthLeft);
            Set("mouthRight", mouthRight);
            Set("mouthRollUpper", mouthRollUpper);
            Set("mouthRollLower", mouthRollLower);
            Set("mouthShrugUpper", mouthShrugUpper);
            Set("mouthShrugLower", mouthShrugLower);
            Set("mouthClose", mouthClose);
            Set("mouthSmileLeft", mouthSmileLeft);
            Set("mouthSmileRight", mouthSmileRight);
            Set("mouthFrownLeft", mouthFrownLeft);
            Set("mouthFrownRight", mouthFrownRight);
            Set("mouthDimpleLeft", mouthDimpleLeft);
            Set("mouthDimpleRight", mouthDimpleRight);
            Set("mouthUpperUpLeft", mouthUpperUpLeft);
            Set("mouthUpperUpRight", mouthUpperUpRight);
            Set("mouthLowerDownLeft", mouthLowerDownLeft);
            Set("mouthLowerDownRight", mouthLowerDownRight);
            Set("mouthPressLeft", mouthPressLeft);
            Set("mouthPressRight", mouthPressRight);
            Set("mouthStretchLeft", mouthStretchLeft);
            Set("mouthStretchRight", mouthStretchRight);
            Set("tongueOut", tongueOut);
        }

        private void Set(string blendShapeName, float value01)
        {
            int idx = arkitMesh.sharedMesh.GetBlendShapeIndex(blendShapeName);
            if (idx >= 0) arkitMesh.SetBlendShapeWeight(idx, Mathf.Clamp01(value01) * 100f);
        }

        public void SetArkitValues(
            float browInnerUp,
            float browDownLeft,
            float browDownRight,
            float browOuterUpLeft,
            float browOuterUpRight,
            float eyeLookUpLeft,
            float eyeLookUpRight,
            float eyeLookDownLeft,
            float eyeLookDownRight,
            float eyeLookInLeft,
            float eyeLookInRight,
            float eyeLookOutLeft,
            float eyeLookOutRight,
            float eyeBlinkLeft,
            float eyeBlinkRight,
            float eyeSquintLeft,
            float eyeSquintRight,
            float eyeWideLeft,
            float eyeWideRight,
            float cheekPuff,
            float cheekSquintLeft,
            float cheekSquintRight,
            float noseSneerLeft,
            float noseSneerRight,
            float jawOpen,
            float jawForward,
            float jawLeft,
            float jawRight,
            float mouthFunnel,
            float mouthPucker,
            float mouthLeft,
            float mouthRight,
            float mouthRollUpper,
            float mouthRollLower,
            float mouthShrugUpper,
            float mouthShrugLower,
            float mouthClose,
            float mouthSmileLeft,
            float mouthSmileRight,
            float mouthFrownLeft,
            float mouthFrownRight,
            float mouthDimpleLeft,
            float mouthDimpleRight,
            float mouthUpperUpLeft,
            float mouthUpperUpRight,
            float mouthLowerDownLeft,
            float mouthLowerDownRight,
            float mouthPressLeft,
            float mouthPressRight,
            float mouthStretchLeft,
            float mouthStretchRight,
            float tongueOut)
        {
            this.browInnerUp = browInnerUp;
            this.browDownLeft = browDownLeft;
            this.browDownRight = browDownRight;
            this.browOuterUpLeft = browOuterUpLeft;
            this.browOuterUpRight = browOuterUpRight;
            this.eyeLookUpLeft = eyeLookUpLeft;
            this.eyeLookUpRight = eyeLookUpRight;
            this.eyeLookDownLeft = eyeLookDownLeft;
            this.eyeLookDownRight = eyeLookDownRight;
            this.eyeLookInLeft = eyeLookInLeft;
            this.eyeLookInRight = eyeLookInRight;
            this.eyeLookOutLeft = eyeLookOutLeft;
            this.eyeLookOutRight = eyeLookOutRight;
            this.eyeBlinkLeft = eyeBlinkLeft;
            this.eyeBlinkRight = eyeBlinkRight;
            this.eyeSquintLeft = eyeSquintLeft;
            this.eyeSquintRight = eyeSquintRight;
            this.eyeWideLeft = eyeWideLeft;
            this.eyeWideRight = eyeWideRight;
            this.cheekPuff = cheekPuff;
            this.cheekSquintLeft = cheekSquintLeft;
            this.cheekSquintRight = cheekSquintRight;
            this.noseSneerLeft = noseSneerLeft;
            this.noseSneerRight = noseSneerRight;
            this.jawOpen = jawOpen;
            this.jawForward = jawForward;
            this.jawLeft = jawLeft;
            this.jawRight = jawRight;
            this.mouthFunnel = mouthFunnel;
            this.mouthPucker = mouthPucker;
            this.mouthLeft = mouthLeft;
            this.mouthRight = mouthRight;
            this.mouthRollUpper = mouthRollUpper;
            this.mouthRollLower = mouthRollLower;
            this.mouthShrugUpper = mouthShrugUpper;
            this.mouthShrugLower = mouthShrugLower;
            this.mouthClose = mouthClose;
            this.mouthSmileLeft = mouthSmileLeft;
            this.mouthSmileRight = mouthSmileRight;
            this.mouthFrownLeft = mouthFrownLeft;
            this.mouthFrownRight = mouthFrownRight;
            this.mouthDimpleLeft = mouthDimpleLeft;
            this.mouthDimpleRight = mouthDimpleRight;
            this.mouthUpperUpLeft = mouthUpperUpLeft;
            this.mouthUpperUpRight = mouthUpperUpRight;
            this.mouthLowerDownLeft = mouthLowerDownLeft;
            this.mouthLowerDownRight = mouthLowerDownRight;
            this.mouthPressLeft = mouthPressLeft;
            this.mouthPressRight = mouthPressRight;
            this.mouthStretchLeft = mouthStretchLeft;
            this.mouthStretchRight = mouthStretchRight;
            this.tongueOut = tongueOut;
        }
    }
}