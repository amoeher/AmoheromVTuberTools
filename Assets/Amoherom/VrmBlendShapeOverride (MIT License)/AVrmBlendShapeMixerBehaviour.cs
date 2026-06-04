using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UniVRM10;

namespace Amoherom
{
    public partial class VrmBlendShapeMixerBehaviour : PlayableBehaviour
    {
        public TimelineClip[] Clips { get; set; }
        public PlayableDirector Director { get; set; }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var instance = playerData as Vrm10Instance;
            if (instance == null || Clips == null || Director == null) return;

            var time = Director.time;
            var isEditMode = !Application.isPlaying;
            var mesh = instance.GetComponentInChildren<SkinnedMeshRenderer>();
            if (mesh == null || mesh.sharedMesh == null) return;

            // --- Accumulators -------

            // --- VRM10 --------------
            float 
                happy = 0, 
                angry = 0, 
                sad = 0, 
                relaxed = 0, 
                surprised = 0,
                aa = 0, 
                ih = 0, 
                ou = 0, 
                ee = 0, 
                oh = 0,
                blink = 0, 
                blinkL = 0, 
                blinkR = 0,
                neutral = 0, 
                browAngry = 0, 
                browFun = 0, 
                browJoy = 0, 
                browSorrow = 0, 
                browSurprised = 0,
                eyeNatural = 0, 
                eyeAngry = 0, 
                eyeFun = 0, 
                eyeJoy = 0, 
                eyeJoyRight = 0, 
                eyeJoyLeft = 0, 
                eyeSorrow = 0, 
                eyeSurprised = 0, 
                eyeSpread = 0, 
                eyeIrisHide = 0, 
                eyeHighlightHide = 0,
                mouthClose = 0, 
                mouthUp = 0, 
                mouthDown = 0, 
                mouthSmall = 0, 
                mouthLarge = 0, 
                mouthNeutral = 0, 
                mouthFun = 0, 
                mouthJoy = 0, 
                mouthAngry = 0, 
                mouthSorrow = 0, 
                mouthSurprised = 0, 
                mouthSkinFung = 0, 
                mouthSkinFungRight = 0, 
                mouthSkinFungLeft = 0,
                HAHide = 0, 
                HAFung1 = 0, 
                HAFung1Low = 0, 
                HAFung1Up = 0, 
                HAFung2 = 0, 
                HAFung2Low = 0, 
                HAFung2Up = 0, 
                HAFung3 = 0, 
                HAFung3Up = 0, 
                HAFung3Low = 0, 
                HAShort = 0, 
                HAShortUp = 0, 
                HAShortLow = 0;


            // ARKit (add more if needed, same pattern)
            float browInnerUp = 0,
                browDownLeft = 0, 
                browDownRight = 0,
                browOuterUpLeft = 0, 
                browOuterUpRight = 0,
                eyeLookUpLeft = 0, 
                eyeLookUpRight = 0, 
                eyeLookDownLeft = 0, 
                eyeLookDownRight = 0, 
                eyeLookInLeft = 0, 
                eyeLookInRight = 0, 
                eyeLookOutLeft = 0, 
                eyeLookOutRight = 0,
                eyeBlinkLeft = 0, 
                eyeBlinkRight = 0, 
                eyeSquintLeft = 0, 
                eyeSquintRight = 0, 
                eyeWideLeft = 0, 
                eyeWideRight = 0, 
                cheekPuff = 0, 
                cheekSquintLeft = 0, 
                cheekSquintRight = 0, 
                noseSneerLeft = 0, 
                noseSneerRight = 0,
                jawOpen = 0, 
                jawForward = 0, 
                jawLeft = 0, 
                jawRight = 0,
                mouthFunnel = 0,
                mouthPucker = 0, 
                mouthLeft = 0, 
                mouthRight = 0, 
                mouthRollUpper = 0, 
                mouthRollLower = 0, 
                mouthShrugUpper = 0, 
                mouthShrugLower = 0, 
                mouthCloseArkit = 0,
                mouthSmileLeft = 0, 
                mouthSmileRight = 0,
                mouthFrownLeft = 0,
                mouthFrownRight = 0,
                mouthDimpleLeft = 0,
                mouthDimpleRight = 0,
                mouthUpperUpLeft = 0,
                mouthUpperUpRight = 0,
                mouthLowerDownLeft = 0,
                mouthLowerDownRight = 0,
                mouthPressLeft = 0, 
                mouthPressRight = 0, 
                mouthStretchLeft = 0, 
                mouthStretchRight = 0, 
                tongueOut = 0;


            bool hasLip = false, hasFace = false, hasArkit = false;

            for (int i = 0; i < Clips.Length; i++)
            {
                var clip = Clips[i];
                var clipAsset = clip.asset as VrmBlendShapeClip;
                if (clipAsset == null) continue;

                float w = playable.GetInputWeight(i);
                float t = (float)((time - clip.start) / clip.duration);
                if (t < 0f || t > 1f || w <= 0f) continue;

                foreach (var e in clipAsset.behaviour.blendShapes)
                {
                    float v = w * e.value;
                    switch (e.preset)
                    {
                        // VRM lip
                        case VrmBlendShapeBehaviour.ExpressionPreset.aa: aa += v; hasLip = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.ih: ih += v; hasLip = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.ou: ou += v; hasLip = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.ee: ee += v; hasLip = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.oh: oh += v; hasLip = true; break;

                        // VRM expressions
                        case VrmBlendShapeBehaviour.ExpressionPreset.angry: angry += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.blink: blink += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.blinkLeft: blinkL += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.blinkRight: blinkR += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.relaxed: relaxed += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.happy: happy += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.sad: sad += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.neutral: neutral += v; hasFace = true; break;
                        case VrmBlendShapeBehaviour.ExpressionPreset.surprised: surprised += v; hasFace = true; break;

                        // Other VRM Blends
                        case VrmBlendShapeBehaviour.ExpressionPreset.BrowAngry: browAngry += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowFun: browFun += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowJoy: browJoy += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowSorrow: browSorrow += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowSurprised: browSurprised += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeNatural: eyeNatural += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeAngry: eyeAngry += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeFun: eyeFun += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeJoy: eyeJoy += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeJoyRight: eyeJoyRight += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeJoyLeft: eyeJoyLeft += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeSorrow: eyeSorrow += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeSurprised: eyeSurprised += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeSpread: eyeSpread += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeIrisHide: eyeIrisHide += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeHighlightHide: eyeHighlightHide += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthClose: mouthClose += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthUp: mouthUp += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthDown: mouthDown += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSmall: mouthSmall += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthLarge: mouthLarge += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthNeutral: mouthNeutral += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthFun: mouthFun += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthJoy: mouthJoy += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthAngry: mouthAngry += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSorrow: mouthSorrow += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSurprised: mouthSurprised += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSkinFung: mouthSkinFung += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSkinFungRight: mouthSkinFungRight += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSkinFungLeft: mouthSkinFungLeft += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAHide: HAHide += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung1: HAFung1 += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung1Low: HAFung1Low += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung1Up: HAFung1Up += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung2: HAFung2 += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung2Low: HAFung2Low += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung2Up: HAFung2Up += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung3: HAFung3 += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung3Up: HAFung3Up += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung3Low: HAFung3Low += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAShort: HAShort += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAShortUp: HAShortUp += v; hasFace = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAShortLow: HAShortLow += v; hasFace = true; break;

                        // ARKit
                        case VrmBlendShapeBehaviour.ExpressionPreset.browInnerUp: browInnerUp += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browDownLeft: browDownLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browDownRight: browDownRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browOuterUpLeft: browOuterUpLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browOuterUpRight: browOuterUpRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookUpLeft: eyeLookUpLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookUpRight: eyeLookUpRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookDownLeft: eyeLookDownLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookDownRight: eyeLookDownRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookInLeft: eyeLookInLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookInRight: eyeLookInRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookOutLeft: eyeLookOutLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookOutRight: eyeLookOutRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeBlinkLeft: eyeBlinkLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeBlinkRight: eyeBlinkRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeSquintLeft: eyeSquintLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeSquintRight: eyeSquintRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeWideLeft: eyeWideLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeWideRight: eyeWideRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.cheekPuff: cheekPuff += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.cheekSquintLeft: cheekSquintLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.cheekSquintRight: cheekSquintRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.noseSneerLeft: noseSneerLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.noseSneerRight: noseSneerRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawOpen: jawOpen += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawForward: jawForward += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawLeft: jawLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawRight: jawRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthFunnel: mouthFunnel += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthPucker: mouthPucker += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthLeft: mouthLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthRight: mouthRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthRollUpper: mouthRollUpper += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthRollLower: mouthRollLower += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthShrugUpper: mouthShrugUpper += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthShrugLower: mouthShrugLower += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthClose_arkit: mouthCloseArkit += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthSmileLeft: mouthSmileLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthSmileRight: mouthSmileRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthFrownLeft: mouthFrownLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthFrownRight: mouthFrownRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthDimpleLeft: mouthDimpleLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthDimpleRight: mouthDimpleRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthUpperUpLeft: mouthUpperUpLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthUpperUpRight: mouthUpperUpRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthLowerDownLeft: mouthLowerDownLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthLowerDownRight: mouthLowerDownRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthPressLeft: mouthPressLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthPressRight: mouthPressRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthStretchLeft: mouthStretchLeft += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthStretchRight: mouthStretchRight += v; hasArkit = true; break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.tongueOut: tongueOut += v; hasArkit = true; break;
                    }
                }
            }

            // --- Apply (ONE PASS ONLY) --------

            // Lip sync
            if (!isEditMode)
            {
                instance.Runtime.Expression.SetWeight(ExpressionKey.Aa, hasLip ? aa : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Ih, hasLip ? ih : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Ou, hasLip ? ou : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Ee, hasLip ? ee : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Oh, hasLip ? oh : 0f);
            }
            else
            {
                SetMeshWeight(mesh, "Fcl_MTH_A", hasLip ? aa : 0f);
                SetMeshWeight(mesh, "Fcl_MTH_I", hasLip ? ih : 0f);
                SetMeshWeight(mesh, "Fcl_MTH_U", hasLip ? ou : 0f);
                SetMeshWeight(mesh, "Fcl_MTH_E", hasLip ? ee : 0f);
                SetMeshWeight(mesh, "Fcl_MTH_O", hasLip ? oh : 0f);
            }

            // Base VRM expressions
            if (!isEditMode)
            {
                instance.Runtime.Expression.SetWeight(ExpressionKey.Angry, hasFace ? angry : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Blink, hasFace ? blink : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.BlinkLeft, hasFace ? blinkL : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.BlinkRight, hasFace ? blinkR : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Relaxed, hasFace ? relaxed : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Happy, hasFace ? happy : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Sad, hasFace ? sad : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Neutral, hasFace ? neutral : 0f);
                instance.Runtime.Expression.SetWeight(ExpressionKey.Surprised, hasFace ? surprised : 0f);
            }
            else
            {
                SetMeshWeight(mesh, "Fcl_ALL_Angry", hasFace ? angry : 0f);
                SetMeshWeight(mesh, "Fcl_EYE_Close", hasFace ? blink : 0f);
                SetMeshWeight(mesh, "Fcl_EYE_Close_L", hasFace ? blinkL : 0f);
                SetMeshWeight(mesh, "Fcl_EYE_Close_R", hasFace ? blinkR : 0f);
                SetMeshWeight(mesh, "Fcl_ALL_Fun", hasFace ? relaxed : 0f);
                SetMeshWeight(mesh, "Fcl_ALL_Joy", hasFace ? happy : 0f);
                SetMeshWeight(mesh, "Fcl_ALL_Sorrow", hasFace ? sad : 0f);
                SetMeshWeight(mesh, "Fcl_ALL_Neutral", hasFace ? neutral : 0f);
                SetMeshWeight(mesh, "Fcl_ALL_Surprised", hasFace ? surprised : 0f);
            }

            // ARKit apply
            if (!isEditMode)
            {
                // use UniVRM Expression runtime in play mode
                var arkitApplier = instance.GetComponent<VrmArkitLateApplier>();
                if (arkitApplier == null) arkitApplier = instance.gameObject.AddComponent<VrmArkitLateApplier>();
                arkitApplier.SetArkitValues(
                    hasArkit ? browInnerUp : 0f,
                    hasArkit ? browDownLeft : 0f,
                    hasArkit ? browDownRight : 0f,
                    hasArkit ? browOuterUpLeft : 0f,
                    hasArkit ? browOuterUpRight : 0f,
                    hasArkit ? eyeLookUpLeft : 0f,
                    hasArkit ? eyeLookUpRight : 0f,
                    hasArkit ? eyeLookDownLeft : 0f,
                    hasArkit ? eyeLookDownRight : 0f,
                    hasArkit ? eyeLookInLeft : 0f,
                    hasArkit ? eyeLookInRight : 0f,
                    hasArkit ? eyeLookOutLeft : 0f,
                    hasArkit ? eyeLookOutRight : 0f,
                    hasArkit ? eyeBlinkLeft : 0f,
                    hasArkit ? eyeBlinkRight : 0f,
                    hasArkit ? eyeSquintLeft : 0f,
                    hasArkit ? eyeSquintRight : 0f,
                    hasArkit ? eyeWideLeft : 0f,
                    hasArkit ? eyeWideRight : 0f,
                    hasArkit ? cheekPuff : 0f,
                    hasArkit ? cheekSquintLeft : 0f,
                    hasArkit ? cheekSquintRight : 0f,
                    hasArkit ? noseSneerLeft : 0f,
                    hasArkit ? noseSneerRight : 0f,
                    hasArkit ? jawOpen : 0f,
                    hasArkit ? jawForward : 0f,
                    hasArkit ? jawLeft : 0f,
                    hasArkit ? jawRight : 0f,
                    hasArkit ? mouthFunnel : 0f,
                    hasArkit ? mouthPucker : 0f,
                    hasArkit ? mouthLeft : 0f,
                    hasArkit ? mouthRight : 0f,
                    hasArkit ? mouthRollUpper : 0f,
                    hasArkit ? mouthRollLower : 0f,
                    hasArkit ? mouthShrugUpper : 0f,
                    hasArkit ? mouthShrugLower : 0f,
                    hasArkit ? mouthCloseArkit : 0f,
                    hasArkit ? mouthSmileLeft : 0f,
                    hasArkit ? mouthSmileRight : 0f,
                    hasArkit ? mouthFrownLeft : 0f,
                    hasArkit ? mouthFrownRight : 0f,
                    hasArkit ? mouthDimpleLeft : 0f,
                    hasArkit ? mouthDimpleRight : 0f,
                    hasArkit ? mouthUpperUpLeft : 0f,
                    hasArkit ? mouthUpperUpRight : 0f,
                    hasArkit ? mouthLowerDownLeft : 0f,
                    hasArkit ? mouthLowerDownRight : 0f,
                    hasArkit ? mouthPressLeft : 0f,
                    hasArkit ? mouthPressRight : 0f,
                    hasArkit ? mouthStretchLeft : 0f,
                    hasArkit ? mouthStretchRight : 0f,
                    hasArkit ? tongueOut : 0f

                );
            }
            else
            {
                // edit mode still can use direct mesh
                SetMeshWeight(mesh, "browInnerUp", hasArkit ? browInnerUp : 0f);
                SetMeshWeight(mesh, "browDownLeft", hasArkit ? browDownLeft : 0f);
                SetMeshWeight(mesh, "browDownRight", hasArkit ? browDownRight : 0f);
                SetMeshWeight(mesh, "browOuterUpLeft", hasArkit ? browOuterUpLeft : 0f);
                SetMeshWeight(mesh, "browOuterUpRight", hasArkit ? browOuterUpRight : 0f);
                SetMeshWeight(mesh, "eyeLookUpLeft", hasArkit ? eyeLookUpLeft : 0f);
                SetMeshWeight(mesh, "eyeLookUpRight", hasArkit ? eyeLookUpRight : 0f);
                SetMeshWeight(mesh, "eyeLookDownLeft", hasArkit ? eyeLookDownLeft : 0f);
                SetMeshWeight(mesh, "eyeLookDownRight", hasArkit ? eyeLookDownRight : 0f);
                SetMeshWeight(mesh, "eyeLookInLeft", hasArkit ? eyeLookInLeft : 0f);
                SetMeshWeight(mesh, "eyeLookInRight", hasArkit ? eyeLookInRight : 0f);
                SetMeshWeight(mesh, "eyeLookOutLeft", hasArkit ? eyeLookOutLeft : 0f);
                SetMeshWeight(mesh, "eyeLookOutRight", hasArkit ? eyeLookOutRight : 0f);
                SetMeshWeight(mesh, "eyeBlinkLeft", hasArkit ? eyeBlinkLeft : 0f);
                SetMeshWeight(mesh, "eyeBlinkRight", hasArkit ? eyeBlinkRight : 0f);
                SetMeshWeight(mesh, "eyeSquintLeft", hasArkit ? eyeSquintLeft : 0f);
                SetMeshWeight(mesh, "eyeSquintRight", hasArkit ? eyeSquintRight : 0f);
                SetMeshWeight(mesh, "eyeWideLeft", hasArkit ? eyeWideLeft : 0f);
                SetMeshWeight(mesh, "eyeWideRight", hasArkit ? eyeWideRight : 0f);
                SetMeshWeight(mesh, "cheekPuff", hasArkit ? cheekPuff : 0f);
                SetMeshWeight(mesh, "cheekSquintLeft", hasArkit ? cheekSquintLeft : 0f);
                SetMeshWeight(mesh, "cheekSquintRight", hasArkit ? cheekSquintRight : 0f);
                SetMeshWeight(mesh, "noseSneerLeft", hasArkit ? noseSneerLeft : 0f);
                SetMeshWeight(mesh, "noseSneerRight", hasArkit ? noseSneerRight : 0f);
                SetMeshWeight(mesh, "jawOpen", hasArkit ? jawOpen : 0f);
                SetMeshWeight(mesh, "jawForward", hasArkit ? jawForward : 0f);
                SetMeshWeight(mesh, "jawLeft", hasArkit ? jawLeft : 0f);
                SetMeshWeight(mesh, "jawRight", hasArkit ? jawRight : 0f);
                SetMeshWeight(mesh, "mouthFunnel", hasArkit ? mouthFunnel : 0f);
                SetMeshWeight(mesh, "mouthPucker", hasArkit ? mouthPucker : 0f);
                SetMeshWeight(mesh, "mouthLeft", hasArkit ? mouthLeft : 0f);
                SetMeshWeight(mesh, "mouthRight", hasArkit ? mouthRight : 0f);
                SetMeshWeight(mesh, "mouthRollUpper", hasArkit ? mouthRollUpper : 0f);
                SetMeshWeight(mesh, "mouthRollLower", hasArkit ? mouthRollLower : 0f);
                SetMeshWeight(mesh, "mouthShrugUpper", hasArkit ? mouthShrugUpper : 0f);
                SetMeshWeight(mesh, "mouthShrugLower", hasArkit ? mouthShrugLower : 0f);
                SetMeshWeight(mesh, "mouthClose", hasArkit ? mouthCloseArkit : 0f);
                SetMeshWeight(mesh, "mouthSmileLeft", hasArkit ? mouthSmileLeft : 0f);
                SetMeshWeight(mesh, "mouthSmileRight", hasArkit ? mouthSmileRight : 0f);
                SetMeshWeight(mesh, "mouthFrownLeft", hasArkit ? mouthFrownLeft : 0f);
                SetMeshWeight(mesh, "mouthFrownRight", hasArkit ? mouthFrownRight : 0f);
                SetMeshWeight(mesh, "mouthDimpleLeft", hasArkit ? mouthDimpleLeft : 0f);
                SetMeshWeight(mesh, "mouthDimpleRight", hasArkit ? mouthDimpleRight : 0f);
                SetMeshWeight(mesh, "mouthUpperUpLeft", hasArkit ? mouthUpperUpLeft : 0f);
                SetMeshWeight(mesh, "mouthUpperUpRight", hasArkit ? mouthUpperUpRight : 0f);
                SetMeshWeight(mesh, "mouthLowerDownLeft", hasArkit ? mouthLowerDownLeft : 0f);
                SetMeshWeight(mesh, "mouthLowerDownRight", hasArkit ? mouthLowerDownRight : 0f);
                SetMeshWeight(mesh, "mouthPressLeft", hasArkit ? mouthPressLeft : 0f);
                SetMeshWeight(mesh, "mouthPressRight", hasArkit ? mouthPressRight : 0f);
                SetMeshWeight(mesh, "mouthStretchLeft", hasArkit ? mouthStretchLeft : 0f);
                SetMeshWeight(mesh, "mouthStretchRight", hasArkit ? mouthStretchRight : 0f);
                SetMeshWeight(mesh, "tongueOut", hasArkit ? tongueOut : 0f);

            }

            // VRM blend shapes
            SetMeshWeight(mesh, "Fcl_BRW_Angry", hasFace ? browAngry : 0f);
            SetMeshWeight(mesh, "Fcl_BRW_Fun", hasFace ? browFun : 0f);
            SetMeshWeight(mesh, "Fcl_BRW_Joy", hasFace ? browJoy : 0f);
            SetMeshWeight(mesh, "Fcl_BRW_Sorrow", hasFace ? browSorrow : 0f);
            SetMeshWeight(mesh, "Fcl_BRW_Surprised", hasFace ? browSurprised : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Natural", hasFace ? eyeNatural : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Angry", hasFace ? eyeAngry : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Fun", hasFace ? eyeFun : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Joy", hasFace ? eyeJoy : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Joy_R", hasFace ? eyeJoyRight : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Joy_L", hasFace ? eyeJoyLeft : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Sorrow", hasFace ? eyeSorrow : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Surprised", hasFace ? eyeSurprised : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_Spread", hasFace ? eyeSpread : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_IrisHide", hasFace ? eyeIrisHide : 0f);
            SetMeshWeight(mesh, "Fcl_EYE_HighlightHide", hasFace ? eyeHighlightHide : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_Neutral", hasFace ? mouthNeutral : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_Fun", hasFace ? mouthFun : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_Joy", hasFace ? mouthJoy : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_Angry", hasFace ? mouthAngry : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_Sorrow", hasFace ? mouthSorrow : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_Surprised", hasFace ? mouthSurprised : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_SkinFung", hasFace ? mouthSkinFung : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_SkinFung_R", hasFace ? mouthSkinFungRight : 0f);
            SetMeshWeight(mesh, "Fcl_MTH_SkinFung_L", hasFace ? mouthSkinFungLeft : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Hide", hasFace ? HAHide : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung1", hasFace ? HAFung1 : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung1_Low", hasFace ? HAFung1Low : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung1_Up", hasFace ? HAFung1Up : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung2", hasFace ? HAFung2 : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung2_Low", hasFace ? HAFung2Low : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung2_Up", hasFace ? HAFung2Up : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung3", hasFace ? HAFung3 : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung3_Up", hasFace ? HAFung3Up : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Fung3_Low", hasFace ? HAFung3Low : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Short", hasFace ? HAShort : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Short_Up", hasFace ? HAShortUp : 0f);
            SetMeshWeight(mesh, "Fcl_HA_Short_Low", hasFace ? HAShortLow : 0f);

        }

        private static void SetMeshWeight(SkinnedMeshRenderer mesh, string blendShapeName, float normalized)
        {
            int idx = mesh.sharedMesh.GetBlendShapeIndex(blendShapeName);
            if (idx >= 0) mesh.SetBlendShapeWeight(idx, Mathf.Clamp01(normalized) * 100f);
        }
    }
}