using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UniVRM10;

namespace Amoherom
{
    public class VrmBlendShapeMixerBehaviour : PlayableBehaviour
    {

        public TimelineClip[] Clips { get; set; }
        public PlayableDirector Director { get; set; }

        public override void OnGraphStart(Playable playable)
        {
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var instance = playerData as UniVRM10.Vrm10Instance;
            if (instance == null) return;

            var time = Director.time;

            // 「あいうえお」
            var value_A = 0f;
            var value_I = 0f;
            var value_U = 0f;
            var value_E = 0f;
            var value_O = 0f;

            // 「喜怒哀楽」
            var value_Angry = 0f;
            var value_Blink = 0f;
            var value_Blink_L = 0f;
            var value_Blink_R = 0f;
            var value_Fun = 0f;
            var value_Joy = 0f;
            var value_Sorrow = 0f;
            var value_Netural = 0f;

            // --- New values for additional presets ---
            var value_Surprised = 0f;
            var value_BrowAngry = 0f;
            var value_BrowFun = 0f;
            var value_BrowJoy = 0f;
            var value_BrowSorrow = 0f;
            var value_BrowSurprised = 0f;
            var value_EyeNatural = 0f;
            var value_EyeAngry = 0f;
            var value_EyeFun = 0f;
            var value_EyeJoy = 0f;
            var value_EyeJoyRight = 0f;
            var value_EyeJoyLeft = 0f;
            var value_EyeSorrow = 0f;
            var value_EyeSurprised = 0f;
            var value_EyeSpread = 0f;
            var value_EyeIrisHide = 0f;
            var value_Highlight_Hide = 0f;
            var value_MouthClose = 0f;
            var value_MouthUp = 0f;
            var value_MouthDown = 0f;
            var value_MouthSmall = 0f;
            var value_MouthLarge = 0f;
            var value_MouthNeutral = 0f;
            var value_MouthFun = 0f;
            var value_MouthJoy = 0f;
            var value_MouthAngry = 0f;
            var value_MouthSorrow = 0f;
            var value_MouthSurprised = 0f;
            var value_MouthSkinFung = 0f;
            var value_MouthSkinFungRight = 0f;
            var value_MouthSkinFungLeft = 0f;
            var value_HAHide = 0f;
            var value_HAFung1 = 0f;
            var value_HAFung1Low = 0f;
            var value_HAFung1Up = 0f;
            var value_HAFung2 = 0f;
            var value_HAFung2Low = 0f;
            var value_HAFung2Up = 0f;
            var value_HAFung3 = 0f;
            var value_HAFung3Up = 0f;
            var value_HAFung3Low = 0f;
            var value_HAShort = 0f;
            var value_HAShortUp = 0f;
            var value_HAShortLow = 0f;

            // ARKit blendshapes
            var value_browInnerUp = 0f;
            var value_browDownLeft = 0f;
            var value_browDownRight = 0f;
            var value_browOuterUpLeft = 0f;
            var value_browOuterUpRight = 0f;
            var value_eyeLookUpLeft = 0f;
            var value_eyeLookUpRight = 0f;
            var value_eyeLookDownLeft = 0f;
            var value_eyeLookDownRight = 0f;
            var value_eyeLookInLeft = 0f;
            var value_eyeLookInRight = 0f;
            var value_eyeLookOutLeft = 0f;
            var value_eyeLookOutRight = 0f;
            var value_eyeBlinkLeft = 0f;
            var value_eyeBlinkRight = 0f;
            var value_eyeSquintLeft = 0f;
            var value_eyeSquintRight = 0f;
            var value_eyeWideLeft = 0f;
            var value_eyeWideRight = 0f;
            var value_cheekPuff = 0f;
            var value_cheekSquintLeft = 0f;
            var value_cheekSquintRight = 0f;
            var value_noseSneerLeft = 0f;
            var value_noseSneerRight = 0f;
            var value_jawOpen = 0f;
            var value_jawForward = 0f;
            var value_jawLeft = 0f;
            var value_jawRight = 0f;
            var value_mouthFunnel = 0f;
            var value_mouthPucker = 0f;
            var value_mouthLeft = 0f;
            var value_mouthRight = 0f;
            var value_mouthRollUpper = 0f;
            var value_mouthRollLower = 0f;
            var value_mouthShrugUpper = 0f;
            var value_mouthShrugLower = 0f;
            var value_mouthClose_ARKit = 0f;
            var value_mouthSmileLeft = 0f;
            var value_mouthSmileRight = 0f;
            var value_mouthFrownLeft = 0f;
            var value_mouthFrownRight = 0f;
            var value_mouthDimpleLeft = 0f;
            var value_mouthDimpleRight = 0f;
            var value_mouthUpperUpLeft = 0f;
            var value_mouthUpperUpRight = 0f;
            var value_mouthLowerDownLeft = 0f;
            var value_mouthLowerDownRight = 0f;
            var value_mouthPressLeft = 0f;
            var value_mouthPressRight = 0f;
            var value_mouthStretchLeft = 0f;
            var value_mouthStretchRight = 0f;
            var value_tongueOut = 0f;

            var isLipSync = false;
            var isFacial = false;

            for (int i = 0; i < Clips.Length; i++)
            {
                var clip = Clips[i];
                var clipAsset = clip.asset as VrmBlendShapeClip;
                var behaviour = clipAsset.behaviour;
                var clipWeight = playable.GetInputWeight(i);
                var clipProgress = (float)((time - clip.start) / clip.duration);

                if (clipProgress >= 0.0f && clipProgress <= 1.0f)
                {
                    foreach (var blendEntry in behaviour.blendShapes)
                    {
                        switch (blendEntry.preset)
                        {
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.aa:
                                value_A += clipWeight * blendEntry.value;
                                isLipSync = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.ih:
                                value_I += clipWeight * blendEntry.value;
                                isLipSync = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.ou:
                                value_U += clipWeight * blendEntry.value;
                                isLipSync = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.ee:
                                value_E += clipWeight * blendEntry.value;
                                isLipSync = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.oh:
                                value_O += clipWeight * blendEntry.value;
                                isLipSync = true;
                                break;

                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.angry:
                                value_Angry += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.blink:
                                value_Blink += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.blinkLeft:
                                value_Blink_L += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.blinkRight:
                                value_Blink_R += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.relaxed:
                                value_Fun += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.happy:
                                value_Joy += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.sad:
                                value_Sorrow += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.neutral:
                                value_Netural += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case (VrmBlendShapeBehaviour.ExpressionPreset)ExpressionPreset.surprised:
                                value_Surprised += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowAngry:
                                value_BrowAngry += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowFun:
                                value_BrowFun += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowJoy:
                                value_BrowJoy += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowSorrow:
                                value_BrowSorrow += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.BrowSurprised:
                                value_BrowSurprised += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeNatural:
                                value_EyeNatural += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeAngry:
                                value_EyeAngry += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeFun:
                                value_EyeFun += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeJoy:
                                value_EyeJoy += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeJoyRight:
                                value_EyeJoyRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeJoyLeft:
                                value_EyeJoyLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeSorrow:
                                value_EyeSorrow += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeSurprised:
                                value_EyeSurprised += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeSpread:
                                value_EyeSpread += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeIrisHide:
                                value_EyeIrisHide += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.EyeHighlightHide:
                                value_Highlight_Hide += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthClose:
                                value_MouthClose += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthUp:
                                value_MouthUp += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthDown:
                                value_MouthDown += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSmall:
                                value_MouthSmall += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthLarge:
                                value_MouthLarge += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthNeutral:
                                value_MouthNeutral += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthFun:
                                value_MouthFun += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthJoy:
                                value_MouthJoy += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthAngry:
                                value_MouthAngry += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSorrow:
                                value_MouthSorrow += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSurprised:
                                value_MouthSurprised += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSkinFung:
                                value_MouthSkinFung += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSkinFungRight:
                                value_MouthSkinFungRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.MouthSkinFungLeft:
                                value_MouthSkinFungLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAHide:
                                value_HAHide += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung1:
                                value_HAFung1 += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung1Low:
                                value_HAFung1Low += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung1Up:
                                value_HAFung1Up += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung2:
                                value_HAFung2 += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung2Low:
                                value_HAFung2Low += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung2Up:
                                value_HAFung2Up += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung3:
                                value_HAFung3 += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung3Up:
                                value_HAFung3Up += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAFung3Low:
                                value_HAFung3Low += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAShort:
                                value_HAShort += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAShortUp:
                                value_HAShortUp += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.HAShortLow:
                                value_HAShortLow += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;

                            // ARKit blendshapes
                            case VrmBlendShapeBehaviour.ExpressionPreset.browInnerUp:
                                value_browInnerUp += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browDownLeft:
                                value_browDownLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browDownRight:
                                value_browDownRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browOuterUpLeft:
                                value_browOuterUpLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.browOuterUpRight:
                                value_browOuterUpRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookUpLeft:
                                value_eyeLookUpLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookUpRight:
                                value_eyeLookUpRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookDownLeft:
                                value_eyeLookDownLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookDownRight:
                                value_eyeLookDownRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookInLeft:
                                value_eyeLookInLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookInRight:
                                value_eyeLookInRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookOutLeft:
                                value_eyeLookOutLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeLookOutRight:
                                value_eyeLookOutRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeBlinkLeft:
                                value_eyeBlinkLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeBlinkRight:
                                value_eyeBlinkRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeSquintLeft:
                                value_eyeSquintLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeSquintRight:
                                value_eyeSquintRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeWideLeft:
                                value_eyeWideLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.eyeWideRight:
                                value_eyeWideRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.cheekPuff:
                                value_cheekPuff += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.cheekSquintLeft:
                                value_cheekSquintLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.cheekSquintRight:
                                value_cheekSquintRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.noseSneerLeft:
                                value_noseSneerLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.noseSneerRight:
                                value_noseSneerRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawOpen:
                                value_jawOpen += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawForward:
                                value_jawForward += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawLeft:
                                value_jawLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.jawRight:
                                value_jawRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthFunnel:
                                value_mouthFunnel += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthPucker:
                                value_mouthPucker += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthLeft:
                                value_mouthLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthRight:
                                value_mouthRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthRollUpper:
                                value_mouthRollUpper += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthRollLower:
                                value_mouthRollLower += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthShrugUpper:
                                value_mouthShrugUpper += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthShrugLower:
                                value_mouthShrugLower += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthClose:
                                value_mouthClose_ARKit += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthSmileLeft:
                                value_mouthSmileLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthSmileRight:
                                value_mouthSmileRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthFrownLeft:
                                value_mouthFrownLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthFrownRight:
                                value_mouthFrownRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthDimpleLeft:
                                value_mouthDimpleLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthDimpleRight:
                                value_mouthDimpleRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthUpperUpLeft:
                                value_mouthUpperUpLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthUpperUpRight:
                                value_mouthUpperUpRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthLowerDownLeft:
                                value_mouthLowerDownLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthLowerDownRight:
                                value_mouthLowerDownRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthPressLeft:
                                value_mouthPressLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthPressRight:
                                value_mouthPressRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthStretchLeft:
                                value_mouthStretchLeft += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.mouthStretchRight:
                                value_mouthStretchRight += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                            case VrmBlendShapeBehaviour.ExpressionPreset.tongueOut:
                                value_tongueOut += clipWeight * blendEntry.value;
                                isFacial = true;
                                break;
                        }
                    }
                }
            }

            // Apply accumulated values after all clips have been processed
            var isEditMode = !Application.isPlaying;
            var meshRender = instance.GetComponent<SkinnedMeshRenderer>();

            if (!isEditMode)
            {
                meshRender = instance.GetComponentsInChildren<SkinnedMeshRenderer>()[0];
            }

            meshRender = instance.GetComponentInChildren<SkinnedMeshRenderer>();

            if (meshRender == null)
            {
                Debug.LogWarning("VrmBlendShapeMixerBehaviour: ProcessFrame - SkinnedMeshRenderer not found on instance.");
            }

            if (isLipSync)
            {
                if (!isEditMode)
                {
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Aa, value_A);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Ih, value_I);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Ou, value_U);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Ee, value_E);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Oh, value_O);
                }
                else
                {
                    if (meshRender != null)
                    {
                        int indexAa = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_A");
                        int indexIh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_I");
                        int indexOu = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_U");
                        int indexEe = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_E");
                        int indexOh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_O");

                        if (indexAa >= 0) meshRender.SetBlendShapeWeight(indexAa, value_A * 100f);
                        if (indexIh >= 0) meshRender.SetBlendShapeWeight(indexIh, value_I * 100f);
                        if (indexOu >= 0) meshRender.SetBlendShapeWeight(indexOu, value_U * 100f);
                        if (indexEe >= 0) meshRender.SetBlendShapeWeight(indexEe, value_E * 100f);
                        if (indexOh >= 0) meshRender.SetBlendShapeWeight(indexOh, value_O * 100f);
                    }
                }
            }
            else
            {
                // No active lip-sync clip --> always reset lip sync expressions to 0
                if (!isEditMode)
                {
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Aa, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Ih, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Ou, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Ee, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Oh, 0f);
                }
                else if (meshRender != null)
                {
                    int indexAa = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_A");
                    int indexIh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_I");
                    int indexOu = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_U");
                    int indexEe = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_E");
                    int indexOh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_O");
                    if (indexAa >= 0) meshRender.SetBlendShapeWeight(indexAa, 0f);
                    if (indexIh >= 0) meshRender.SetBlendShapeWeight(indexIh, 0f);
                    if (indexOu >= 0) meshRender.SetBlendShapeWeight(indexOu, 0f);
                    if (indexEe >= 0) meshRender.SetBlendShapeWeight(indexEe, 0f);
                    if (indexOh >= 0) meshRender.SetBlendShapeWeight(indexOh, 0f);
                }
            }
            if (isFacial)
            {
                if (!isEditMode)
                {
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Angry, value_Angry);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Blink, value_Blink);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.BlinkLeft, value_Blink_L);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.BlinkRight, value_Blink_R);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Relaxed, value_Fun);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Happy, value_Joy);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Sad, value_Sorrow);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Neutral, value_Netural);

                    // --- Additional blendshapes: set directly on SkinnedMeshRenderer in play mode ---
                    if (meshRender != null)
                    {
                        int indexSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Surprised");
                        int indexBrowAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Angry");
                        int indexBrowFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Fun");
                        int indexBrowJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Joy");
                        int indexBrowSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Sorrow");
                        int indexBrowSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Suprized");
                        int indexEyeNatural = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Natural");
                        int indexEyeAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Angry");
                        int indexEyeFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Fun");
                        int indexEyeJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy");
                        int indexEyeJoyRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_R");
                        int indexEyeJoyLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_L");
                        int indexEyeSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Sorrow");
                        int indexEyeSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Suprized");
                        int indexEyeSpread = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Spread");
                        int indexEyeIrisHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Iris_Hide");
                        int indexEyeHighlightHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Highlight_Hide");
                        int indexMouthClose = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Close");
                        int indexMouthUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Up");
                        int indexMouthDown = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Down");
                        int indexMouthSmall = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Small");
                        int indexMouthLarge = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Large");
                        int indexMouthNeutral = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Neutral");
                        int indexMouthFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Fun");
                        int indexMouthJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Joy");
                        int indexMouthAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Angry");
                        int indexMouthSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Sorrow");
                        int indexMouthSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Suprized");
                        int indexMouthSkinFung = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung");
                        int indexMouthSkinFungRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_R");
                        int indexMouthSkinFungLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_L");
                        int indexHAHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Hide");
                        int indexHAFung1 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1");
                        int indexHAFung1Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Low");
                        int indexHAFung1Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Up");
                        int indexHAFung2 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2");
                        int indexHAFung2Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Low");
                        int indexHAFung2Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Up");
                        int indexHAFung3 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3");
                        int indexHAFung3Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Up");
                        int indexHAFung3Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Low");
                        int indexHAShort = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short");
                        int indexHAShortUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Up");
                        int indexHAShortLow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Low");

                        if (indexSurprised >= 0) meshRender.SetBlendShapeWeight(indexSurprised, value_Surprised * 100f);
                        if (indexBrowAngry >= 0) meshRender.SetBlendShapeWeight(indexBrowAngry, value_BrowAngry * 100f);
                        if (indexBrowFun >= 0) meshRender.SetBlendShapeWeight(indexBrowFun, value_BrowFun * 100f);
                        if (indexBrowJoy >= 0) meshRender.SetBlendShapeWeight(indexBrowJoy, value_BrowJoy * 100f);
                        if (indexBrowSorrow >= 0) meshRender.SetBlendShapeWeight(indexBrowSorrow, value_BrowSorrow * 100f);
                        if (indexBrowSurprised >= 0) meshRender.SetBlendShapeWeight(indexBrowSurprised, value_BrowSurprised * 100f);
                        if (indexEyeNatural >= 0) meshRender.SetBlendShapeWeight(indexEyeNatural, value_EyeNatural * 100f);
                        if (indexEyeAngry >= 0) meshRender.SetBlendShapeWeight(indexEyeAngry, value_EyeAngry * 100f);
                        if (indexEyeFun >= 0) meshRender.SetBlendShapeWeight(indexEyeFun, value_EyeFun * 100f);
                        if (indexEyeJoy >= 0) meshRender.SetBlendShapeWeight(indexEyeJoy, value_EyeJoy * 100f);
                        if (indexEyeJoyRight >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyRight, value_EyeJoyRight * 100f);
                        if (indexEyeJoyLeft >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyLeft, value_EyeJoyLeft * 100f);
                        if (indexEyeSorrow >= 0) meshRender.SetBlendShapeWeight(indexEyeSorrow, value_EyeSorrow * 100f);
                        if (indexEyeSurprised >= 0) meshRender.SetBlendShapeWeight(indexEyeSurprised, value_EyeSurprised * 100f);
                        if (indexEyeSpread >= 0) meshRender.SetBlendShapeWeight(indexEyeSpread, value_EyeSpread * 100f);
                        if (indexEyeIrisHide >= 0) meshRender.SetBlendShapeWeight(indexEyeIrisHide, value_EyeIrisHide * 100f);
                        if (indexEyeHighlightHide >= 0) meshRender.SetBlendShapeWeight(indexEyeHighlightHide, value_Highlight_Hide * 100f);
                        if (indexMouthClose >= 0) meshRender.SetBlendShapeWeight(indexMouthClose, value_MouthClose * 100f);
                        if (indexMouthUp >= 0) meshRender.SetBlendShapeWeight(indexMouthUp, value_MouthUp * 100f);
                        if (indexMouthDown >= 0) meshRender.SetBlendShapeWeight(indexMouthDown, value_MouthDown * 100f);
                        if (indexMouthSmall >= 0) meshRender.SetBlendShapeWeight(indexMouthSmall, value_MouthSmall * 100f);
                        if (indexMouthLarge >= 0) meshRender.SetBlendShapeWeight(indexMouthLarge, value_MouthLarge * 100f);
                        if (indexMouthNeutral >= 0) meshRender.SetBlendShapeWeight(indexMouthNeutral, value_MouthNeutral * 100f);
                        if (indexMouthFun >= 0) meshRender.SetBlendShapeWeight(indexMouthFun, value_MouthFun * 100f);
                        if (indexMouthJoy >= 0) meshRender.SetBlendShapeWeight(indexMouthJoy, value_MouthJoy * 100f);
                    if (indexMouthAngry >= 0) meshRender.SetBlendShapeWeight(indexMouthAngry, value_MouthAngry * 100f);
                        if (indexMouthSorrow >= 0) meshRender.SetBlendShapeWeight(indexMouthSorrow, value_MouthSorrow * 100f);
                        if (indexMouthSurprised >= 0) meshRender.SetBlendShapeWeight(indexMouthSurprised, value_MouthSurprised * 100f);
                        if (indexMouthSkinFung >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFung, value_MouthSkinFung * 100f);
                        if (indexMouthSkinFungRight >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungRight, value_MouthSkinFungRight * 100f);
                        if (indexMouthSkinFungLeft >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungLeft, value_MouthSkinFungLeft * 100f);
                        if (indexHAHide >= 0) meshRender.SetBlendShapeWeight(indexHAHide, value_HAHide * 100f);
                        if (indexHAFung1 >= 0) meshRender.SetBlendShapeWeight(indexHAFung1, value_HAFung1 * 100f);
                        if (indexHAFung1Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Low, value_HAFung1Low * 100f);
                        if (indexHAFung1Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Up, value_HAFung1Up * 100f);
                        if (indexHAFung2 >= 0) meshRender.SetBlendShapeWeight(indexHAFung2, value_HAFung2 * 100f);
                        if (indexHAFung2Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Low, value_HAFung2Low * 100f);
                        if (indexHAFung2Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Up, value_HAFung2Up * 100f);
                        if (indexHAFung3 >= 0) meshRender.SetBlendShapeWeight(indexHAFung3, value_HAFung3 * 100f);
                        if (indexHAFung3Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Up, value_HAFung3Up * 100f);
                        if (indexHAFung3Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Low, value_HAFung3Low * 100f);
                        if (indexHAShort >= 0) meshRender.SetBlendShapeWeight(indexHAShort, value_HAShort * 100f);
                        if (indexHAShortUp >= 0) meshRender.SetBlendShapeWeight(indexHAShortUp, value_HAShortUp * 100f);
                        if (indexHAShortLow >= 0) meshRender.SetBlendShapeWeight(indexHAShortLow, value_HAShortLow * 100f);
                    }

                }
                else
                {
                    if (meshRender != null)
                    {
                        // --- Standard mappings ---
                        int indexAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Angry");
                        int indexBlink = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Close");
                        int indexBlinkL = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Close_L");
                        int indexBlinkR = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Close_R");
                        int indexFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Fun");
                        int indexJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Joy");
                        int indexSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Sorrow");
                        int indexNeutral = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Neutral");
                        if (indexAngry >= 0) meshRender.SetBlendShapeWeight(indexAngry, value_Angry * 100f);
                        if (indexBlink >= 0) meshRender.SetBlendShapeWeight(indexBlink, value_Blink * 100f);
                        if (indexBlinkL >= 0) meshRender.SetBlendShapeWeight(indexBlinkL, value_Blink_L * 100f);
                        if (indexBlinkR >= 0) meshRender.SetBlendShapeWeight(indexBlinkR, value_Blink_R * 100f);
                        if (indexFun >= 0) meshRender.SetBlendShapeWeight(indexFun, value_Fun * 100f);
                        if (indexJoy >= 0) meshRender.SetBlendShapeWeight(indexJoy, value_Joy * 100f);
                        if (indexSorrow >= 0) meshRender.SetBlendShapeWeight(indexSorrow, value_Sorrow * 100f);
                        if (indexNeutral >= 0) meshRender.SetBlendShapeWeight(indexNeutral, value_Netural * 100f);

                        // --- Additional blendshapes ---
                        int indexSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Surprised");
                        int indexBrowAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Angry");
                        int indexBrowFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Fun");
                        int indexBrowJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Joy");
                        int indexBrowSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Sorrow");
                        int indexBrowSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Suprized");
                        int indexEyeNatural = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Natural");
                        int indexEyeAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Angry");
                        int indexEyeFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Fun");
                        int indexEyeJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy");
                        int indexEyeJoyRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_R");
                        int indexEyeJoyLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_L");
                        int indexEyeSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Sorrow");
                        int indexEyeSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Suprized");
                        int indexEyeSpread = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Spread");
                        int indexEyeIrisHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Iris_Hide");
                        int indexEyeHighlightHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Highlight_Hide");
                        int indexMouthClose = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Close");
                        int indexMouthUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Up");
                        int indexMouthDown = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Down");
                        int indexMouthSmall = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Small");
                        int indexMouthLarge = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Large");
                        int indexMouthNeutral = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Neutral");
                        int indexMouthFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Fun");
                        int indexMouthJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Joy");
                        int indexMouthAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Angry");
                        int indexMouthSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Sorrow");
                        int indexMouthSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Suprized");
                        int indexMouthSkinFung = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung");
                        int indexMouthSkinFungRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_R");
                        int indexMouthSkinFungLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_L");
                        int indexAa = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_A");
                        int indexIh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_I");
                        int indexOu = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_U");
                        int indexEe = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_E");
                        int indexOh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_O");
                        int indexHAHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Hide");
                        int indexHAFung1 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1");
                        int indexHAFung1Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Low");
                        int indexHAFung1Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Up");
                        int indexHAFung2 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2");
                        int indexHAFung2Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Low");
                        int indexHAFung2Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Up");
                        int indexHAFung3 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3");
                        int indexHAFung3Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Up");
                        int indexHAFung3Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Low");
                        int indexHAShort = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short");
                        int indexHAShortUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Up");
                        int indexHAShortLow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Low");

                        if (indexSurprised >= 0) meshRender.SetBlendShapeWeight(indexSurprised, value_Surprised * 100f);
                        if (indexBrowAngry >= 0) meshRender.SetBlendShapeWeight(indexBrowAngry, value_BrowAngry * 100f);
                        if (indexBrowFun >= 0) meshRender.SetBlendShapeWeight(indexBrowFun, value_BrowFun * 100f);
                        if (indexBrowJoy >= 0) meshRender.SetBlendShapeWeight(indexBrowJoy, value_BrowJoy * 100f);
                        if (indexBrowSorrow >= 0) meshRender.SetBlendShapeWeight(indexBrowSorrow, value_BrowSorrow * 100f);
                        if (indexBrowSurprised >= 0) meshRender.SetBlendShapeWeight(indexBrowSurprised, value_BrowSurprised * 100f);
                        if (indexEyeNatural >= 0) meshRender.SetBlendShapeWeight(indexEyeNatural, value_EyeNatural * 100f);
                        if (indexEyeAngry >= 0) meshRender.SetBlendShapeWeight(indexEyeAngry, value_EyeAngry * 100f);
                        if (indexEyeFun >= 0) meshRender.SetBlendShapeWeight(indexEyeFun, value_EyeFun * 100f);
                        if (indexEyeJoy >= 0) meshRender.SetBlendShapeWeight(indexEyeJoy, value_EyeJoy * 100f);
                        if (indexEyeJoyRight >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyRight, value_EyeJoyRight * 100f);
                        if (indexEyeJoyLeft >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyLeft, value_EyeJoyLeft * 100f);
                        if (indexEyeSorrow >= 0) meshRender.SetBlendShapeWeight(indexEyeSorrow, value_EyeSorrow * 100f);
                        if (indexEyeSurprised >= 0) meshRender.SetBlendShapeWeight(indexEyeSurprised, value_EyeSurprised * 100f);
                        if (indexEyeSpread >= 0) meshRender.SetBlendShapeWeight(indexEyeSpread, value_EyeSpread * 100f);
                        if (indexEyeIrisHide >= 0) meshRender.SetBlendShapeWeight(indexEyeIrisHide, value_EyeIrisHide * 100f);
                        if (indexEyeHighlightHide >= 0) meshRender.SetBlendShapeWeight(indexEyeHighlightHide, value_Highlight_Hide * 100f);
                        if (indexMouthClose >= 0) meshRender.SetBlendShapeWeight(indexMouthClose, value_MouthClose * 100f);
                        if (indexMouthUp >= 0) meshRender.SetBlendShapeWeight(indexMouthUp, value_MouthUp * 100f);
                        if (indexMouthDown >= 0) meshRender.SetBlendShapeWeight(indexMouthDown, value_MouthDown * 100f);
                        if (indexMouthSmall >= 0) meshRender.SetBlendShapeWeight(indexMouthSmall, value_MouthSmall * 100f);
                        if (indexMouthLarge >= 0) meshRender.SetBlendShapeWeight(indexMouthLarge, value_MouthLarge * 100f);
                        if (indexMouthNeutral >= 0) meshRender.SetBlendShapeWeight(indexMouthNeutral, value_MouthNeutral * 100f);
                        if (indexMouthFun >= 0) meshRender.SetBlendShapeWeight(indexMouthFun, value_MouthFun * 100f);
                        if (indexMouthJoy >= 0) meshRender.SetBlendShapeWeight(indexMouthJoy, value_MouthJoy * 100f);
                    if (indexMouthAngry >= 0) meshRender.SetBlendShapeWeight(indexMouthAngry, value_MouthAngry * 100f);
                        if (indexMouthSorrow >= 0) meshRender.SetBlendShapeWeight(indexMouthSorrow, value_MouthSorrow * 100f);
                        if (indexMouthSurprised >= 0) meshRender.SetBlendShapeWeight(indexMouthSurprised, value_MouthSurprised * 100f);
                        if (indexMouthSkinFung >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFung, value_MouthSkinFung * 100f);
                        if (indexMouthSkinFungRight >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungRight, value_MouthSkinFungRight * 100f);
                        if (indexMouthSkinFungLeft >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungLeft, value_MouthSkinFungLeft * 100f);
                        if (indexHAHide >= 0) meshRender.SetBlendShapeWeight(indexHAHide, value_HAHide * 100f);
                        if (indexHAFung1 >= 0) meshRender.SetBlendShapeWeight(indexHAFung1, value_HAFung1 * 100f);
                        if (indexHAFung1Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Low, value_HAFung1Low * 100f);
                        if (indexHAFung1Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Up, value_HAFung1Up * 100f);
                        if (indexHAFung2 >= 0) meshRender.SetBlendShapeWeight(indexHAFung2, value_HAFung2 * 100f);
                        if (indexHAFung2Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Low, value_HAFung2Low * 100f);
                        if (indexHAFung2Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Up, value_HAFung2Up * 100f);
                        if (indexHAFung3 >= 0) meshRender.SetBlendShapeWeight(indexHAFung3, value_HAFung3 * 100f);
                        if (indexHAFung3Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Up, value_HAFung3Up * 100f);
                        if (indexHAFung3Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Low, value_HAFung3Low * 100f);
                        if (indexHAShort >= 0) meshRender.SetBlendShapeWeight(indexHAShort, value_HAShort * 100f);
                        if (indexHAShortUp >= 0) meshRender.SetBlendShapeWeight(indexHAShortUp, value_HAShortUp * 100f);
                        if (indexHAShortLow >= 0) meshRender.SetBlendShapeWeight(indexHAShortLow, value_HAShortLow * 100f);
                        if (indexAa >= 0) meshRender.SetBlendShapeWeight(indexAa, value_A * 100f);
                        if (indexIh >= 0) meshRender.SetBlendShapeWeight(indexIh, value_I * 100f);
                        if (indexOu >= 0) meshRender.SetBlendShapeWeight(indexOu, value_U * 100f);
                        if (indexEe >= 0) meshRender.SetBlendShapeWeight(indexEe, value_E * 100f);
                        if (indexOh >= 0) meshRender.SetBlendShapeWeight(indexOh, value_O * 100f);
                    }
                }
            }
            else
            {
                // No active facial clip --> reset all facial expressions to 0
                if (!isEditMode)
                {
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Angry, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Blink, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.BlinkLeft, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.BlinkRight, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Relaxed, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Happy, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Sad, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Neutral, 0f);
                    instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Surprised, 0f);
                }
            }



            if (!isEditMode)
            {
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Angry, value_Angry);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Blink, value_Blink);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.BlinkLeft, value_Blink_L);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.BlinkRight, value_Blink_R);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Relaxed, value_Fun);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Happy, value_Joy);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Sad, value_Sorrow);
                instance.Runtime.Expression.SetWeight(UniVRM10.ExpressionKey.Neutral, value_Netural);

                // --- Additional blendshapes:play mode ---
                if (meshRender != null)
                {
                    int indexSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Surprised");
                    int indexBrowAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Angry");
                    int indexBrowFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Fun");
                    int indexBrowJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Joy");
                    int indexBrowSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Sorrow");
                    int indexBrowSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Suprized");
                    int indexEyeNatural = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Natural");
                    int indexEyeAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Angry");
                    int indexEyeFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Fun");
                    int indexEyeJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy");
                    int indexEyeJoyRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_R");
                    int indexEyeJoyLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_L");
                    int indexEyeSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Sorrow");
                    int indexEyeSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Suprized");
                    int indexEyeSpread = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Spread");
                    int indexEyeIrisHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Iris_Hide");
                    int indexEyeHighlightHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Highlight_Hide");
                    int indexMouthClose = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Close");
                    int indexMouthUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Up");
                    int indexMouthDown = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Down");
                    int indexMouthSmall = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Small");
                    int indexMouthLarge = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Large");
                    int indexMouthNeutral = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Neutral");
                    int indexMouthFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Fun");
                    int indexMouthJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Joy");
                        int indexMouthAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Angry");
                    int indexMouthSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Sorrow");
                    int indexMouthSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Suprized");
                    int indexMouthSkinFung = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung");
                    int indexMouthSkinFungRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_R");
                    int indexMouthSkinFungLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_L");
                    int indexHAHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Hide");
                    int indexHAFung1 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1");
                    int indexHAFung1Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Low");
                    int indexHAFung1Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Up");
                    int indexHAFung2 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2");
                    int indexHAFung2Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Low");
                    int indexHAFung2Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Up");
                    int indexHAFung3 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3");
                    int indexHAFung3Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Up");
                    int indexHAFung3Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Low");
                    int indexHAShort = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short");
                    int indexHAShortUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Up");
                    int indexHAShortLow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Low");

                    if (indexSurprised >= 0) meshRender.SetBlendShapeWeight(indexSurprised, value_Surprised * 100f);
                    if (indexBrowAngry >= 0) meshRender.SetBlendShapeWeight(indexBrowAngry, value_BrowAngry * 100f);
                    if (indexBrowFun >= 0) meshRender.SetBlendShapeWeight(indexBrowFun, value_BrowFun * 100f);
                    if (indexBrowJoy >= 0) meshRender.SetBlendShapeWeight(indexBrowJoy, value_BrowJoy * 100f);
                    if (indexBrowSorrow >= 0) meshRender.SetBlendShapeWeight(indexBrowSorrow, value_BrowSorrow * 100f);
                    if (indexBrowSurprised >= 0) meshRender.SetBlendShapeWeight(indexBrowSurprised, value_BrowSurprised * 100f);
                    if (indexEyeNatural >= 0) meshRender.SetBlendShapeWeight(indexEyeNatural, value_EyeNatural * 100f);
                    if (indexEyeAngry >= 0) meshRender.SetBlendShapeWeight(indexEyeAngry, value_EyeAngry * 100f);
                    if (indexEyeFun >= 0) meshRender.SetBlendShapeWeight(indexEyeFun, value_EyeFun * 100f);
                    if (indexEyeJoy >= 0) meshRender.SetBlendShapeWeight(indexEyeJoy, value_EyeJoy * 100f);
                    if (indexEyeJoyRight >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyRight, value_EyeJoyRight * 100f);
                    if (indexEyeJoyLeft >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyLeft, value_EyeJoyLeft * 100f);
                    if (indexEyeSorrow >= 0) meshRender.SetBlendShapeWeight(indexEyeSorrow, value_EyeSorrow * 100f);
                    if (indexEyeSurprised >= 0) meshRender.SetBlendShapeWeight(indexEyeSurprised, value_EyeSurprised * 100f);
                    if (indexEyeSpread >= 0) meshRender.SetBlendShapeWeight(indexEyeSpread, value_EyeSpread * 100f);
                    if (indexEyeIrisHide >= 0) meshRender.SetBlendShapeWeight(indexEyeIrisHide, value_EyeIrisHide * 100f);
                    if (indexEyeHighlightHide >= 0) meshRender.SetBlendShapeWeight(indexEyeHighlightHide, value_Highlight_Hide * 100f);
                    if (indexMouthClose >= 0) meshRender.SetBlendShapeWeight(indexMouthClose, value_MouthClose * 100f);
                    if (indexMouthUp >= 0) meshRender.SetBlendShapeWeight(indexMouthUp, value_MouthUp * 100f);
                    if (indexMouthDown >= 0) meshRender.SetBlendShapeWeight(indexMouthDown, value_MouthDown * 100f);
                    if (indexMouthSmall >= 0) meshRender.SetBlendShapeWeight(indexMouthSmall, value_MouthSmall * 100f);
                    if (indexMouthLarge >= 0) meshRender.SetBlendShapeWeight(indexMouthLarge, value_MouthLarge * 100f);
                    if (indexMouthNeutral >= 0) meshRender.SetBlendShapeWeight(indexMouthNeutral, value_MouthNeutral * 100f);
                    if (indexMouthFun >= 0) meshRender.SetBlendShapeWeight(indexMouthFun, value_MouthFun * 100f);
                    if (indexMouthJoy >= 0) meshRender.SetBlendShapeWeight(indexMouthJoy, value_MouthJoy * 100f);
                    if (indexMouthAngry >= 0) meshRender.SetBlendShapeWeight(indexMouthAngry, value_MouthAngry * 100f);
                    if (indexMouthSorrow >= 0) meshRender.SetBlendShapeWeight(indexMouthSorrow, value_MouthSorrow * 100f);
                    if (indexMouthSurprised >= 0) meshRender.SetBlendShapeWeight(indexMouthSurprised, value_MouthSurprised * 100f);
                    if (indexMouthSkinFung >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFung, value_MouthSkinFung * 100f);
                    if (indexMouthSkinFungRight >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungRight, value_MouthSkinFungRight * 100f);
                    if (indexMouthSkinFungLeft >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungLeft, value_MouthSkinFungLeft * 100f);
                    if (indexHAHide >= 0) meshRender.SetBlendShapeWeight(indexHAHide, value_HAHide * 100f);
                    if (indexHAFung1 >= 0) meshRender.SetBlendShapeWeight(indexHAFung1, value_HAFung1 * 100f);
                    if (indexHAFung1Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Low, value_HAFung1Low * 100f);
                    if (indexHAFung1Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Up, value_HAFung1Up * 100f);
                    if (indexHAFung2 >= 0) meshRender.SetBlendShapeWeight(indexHAFung2, value_HAFung2 * 100f);
                    if (indexHAFung2Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Low, value_HAFung2Low * 100f);
                    if (indexHAFung2Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Up, value_HAFung2Up * 100f);
                    if (indexHAFung3 >= 0) meshRender.SetBlendShapeWeight(indexHAFung3, value_HAFung3 * 100f);
                    if (indexHAFung3Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Up, value_HAFung3Up * 100f);
                    if (indexHAFung3Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Low, value_HAFung3Low * 100f);
                    if (indexHAShort >= 0) meshRender.SetBlendShapeWeight(indexHAShort, value_HAShort * 100f);
                    if (indexHAShortUp >= 0) meshRender.SetBlendShapeWeight(indexHAShortUp, value_HAShortUp * 100f);
                    if (indexHAShortLow >= 0) meshRender.SetBlendShapeWeight(indexHAShortLow, value_HAShortLow * 100f);
                }

            }
            else
            {
                if (meshRender != null)
                {
                    // --- Standard mappings ---
                    int indexAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Angry");
                    int indexBlink = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Close");
                    int indexBlinkL = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Close_L");
                    int indexBlinkR = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Close_R");
                    int indexFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Fun");
                    int indexJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Joy");
                    int indexSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Sorrow");
                    int indexNeutral = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Neutral");
                    if (indexAngry >= 0) meshRender.SetBlendShapeWeight(indexAngry, value_Angry * 100f);
                    if (indexBlink >= 0) meshRender.SetBlendShapeWeight(indexBlink, value_Blink * 100f);
                    if (indexBlinkL >= 0) meshRender.SetBlendShapeWeight(indexBlinkL, value_Blink_L * 100f);
                    if (indexBlinkR >= 0) meshRender.SetBlendShapeWeight(indexBlinkR, value_Blink_R * 100f);
                    if (indexFun >= 0) meshRender.SetBlendShapeWeight(indexFun, value_Fun * 100f);
                    if (indexJoy >= 0) meshRender.SetBlendShapeWeight(indexJoy, value_Joy * 100f);
                    if (indexSorrow >= 0) meshRender.SetBlendShapeWeight(indexSorrow, value_Sorrow * 100f);
                    if (indexNeutral >= 0) meshRender.SetBlendShapeWeight(indexNeutral, value_Netural * 100f);

                    // --- Additional blendshapes ---
                    int indexSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_ALL_Surprised");
                    int indexBrowAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Angry");
                    int indexBrowFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Fun");
                    int indexBrowJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Joy");
                    int indexBrowSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Sorrow");
                    int indexBrowSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_BRW_Suprized");
                    int indexEyeNatural = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Natural");
                    int indexEyeAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Angry");
                    int indexEyeFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Fun");
                    int indexEyeJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy");
                    int indexEyeJoyRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_R");
                    int indexEyeJoyLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Joy_L");
                    int indexEyeSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Sorrow");
                    int indexEyeSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Suprized");
                    int indexEyeSpread = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Spread");
                    int indexEyeIrisHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Iris_Hide");
                    int indexEyeHighlightHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_EYE_Highlight_Hide");
                    int indexMouthClose = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Close");
                    int indexMouthUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Up");
                    int indexMouthDown = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Down");
                    int indexMouthSmall = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Small");
                    int indexMouthLarge = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Large");
                    int indexMouthNeutral = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Neutral");
                    int indexMouthFun = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Fun");
                    int indexMouthJoy = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Joy");
                        int indexMouthAngry = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Angry");
                    int indexMouthSorrow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Sorrow");
                    int indexMouthSurprised = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_Suprized");
                    int indexMouthSkinFung = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung");
                    int indexMouthSkinFungRight = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_R");
                    int indexMouthSkinFungLeft = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_SkinFung_L");
                    int indexAa = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_A");
                    int indexIh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_I");
                    int indexOu = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_U");
                    int indexEe = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_E");
                    int indexOh = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_MTH_O");
                    int indexHAHide = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Hide");
                    int indexHAFung1 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1");
                    int indexHAFung1Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Low");
                    int indexHAFung1Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung1_Up");
                    int indexHAFung2 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2");
                    int indexHAFung2Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Low");
                    int indexHAFung2Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung2_Up");
                    int indexHAFung3 = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3");
                    int indexHAFung3Up = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Up");
                    int indexHAFung3Low = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Fung3_Low");
                    int indexHAShort = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short");
                    int indexHAShortUp = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Up");
                    int indexHAShortLow = meshRender.sharedMesh.GetBlendShapeIndex("Fcl_HA_Short_Low");

                    if (indexSurprised >= 0) meshRender.SetBlendShapeWeight(indexSurprised, value_Surprised * 100f);
                    if (indexBrowAngry >= 0) meshRender.SetBlendShapeWeight(indexBrowAngry, value_BrowAngry * 100f);
                    if (indexBrowFun >= 0) meshRender.SetBlendShapeWeight(indexBrowFun, value_BrowFun * 100f);
                    if (indexBrowJoy >= 0) meshRender.SetBlendShapeWeight(indexBrowJoy, value_BrowJoy * 100f);
                    if (indexBrowSorrow >= 0) meshRender.SetBlendShapeWeight(indexBrowSorrow, value_BrowSorrow * 100f);
                    if (indexBrowSurprised >= 0) meshRender.SetBlendShapeWeight(indexBrowSurprised, value_BrowSurprised * 100f);
                    if (indexEyeNatural >= 0) meshRender.SetBlendShapeWeight(indexEyeNatural, value_EyeNatural * 100f);
                    if (indexEyeAngry >= 0) meshRender.SetBlendShapeWeight(indexEyeAngry, value_EyeAngry * 100f);
                    if (indexEyeFun >= 0) meshRender.SetBlendShapeWeight(indexEyeFun, value_EyeFun * 100f);
                    if (indexEyeJoy >= 0) meshRender.SetBlendShapeWeight(indexEyeJoy, value_EyeJoy * 100f);
                    if (indexEyeJoyRight >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyRight, value_EyeJoyRight * 100f);
                    if (indexEyeJoyLeft >= 0) meshRender.SetBlendShapeWeight(indexEyeJoyLeft, value_EyeJoyLeft * 100f);
                    if (indexEyeSorrow >= 0) meshRender.SetBlendShapeWeight(indexEyeSorrow, value_EyeSorrow * 100f);
                    if (indexEyeSurprised >= 0) meshRender.SetBlendShapeWeight(indexEyeSurprised, value_EyeSurprised * 100f);
                    if (indexEyeSpread >= 0) meshRender.SetBlendShapeWeight(indexEyeSpread, value_EyeSpread * 100f);
                    if (indexEyeIrisHide >= 0) meshRender.SetBlendShapeWeight(indexEyeIrisHide, value_EyeIrisHide * 100f);
                    if (indexEyeHighlightHide >= 0) meshRender.SetBlendShapeWeight(indexEyeHighlightHide, value_Highlight_Hide * 100f);
                    if (indexMouthClose >= 0) meshRender.SetBlendShapeWeight(indexMouthClose, value_MouthClose * 100f);
                    if (indexMouthUp >= 0) meshRender.SetBlendShapeWeight(indexMouthUp, value_MouthUp * 100f);
                    if (indexMouthDown >= 0) meshRender.SetBlendShapeWeight(indexMouthDown, value_MouthDown * 100f);
                    if (indexMouthSmall >= 0) meshRender.SetBlendShapeWeight(indexMouthSmall, value_MouthSmall * 100f);
                    if (indexMouthLarge >= 0) meshRender.SetBlendShapeWeight(indexMouthLarge, value_MouthLarge * 100f);
                    if (indexMouthNeutral >= 0) meshRender.SetBlendShapeWeight(indexMouthNeutral, value_MouthNeutral * 100f);
                    if (indexMouthFun >= 0) meshRender.SetBlendShapeWeight(indexMouthFun, value_MouthFun * 100f);
                    if (indexMouthJoy >= 0) meshRender.SetBlendShapeWeight(indexMouthJoy, value_MouthJoy * 100f);
                    if (indexMouthAngry >= 0) meshRender.SetBlendShapeWeight(indexMouthAngry, value_MouthAngry * 100f);
                    if (indexMouthSorrow >= 0) meshRender.SetBlendShapeWeight(indexMouthSorrow, value_MouthSorrow * 100f);
                    if (indexMouthSurprised >= 0) meshRender.SetBlendShapeWeight(indexMouthSurprised, value_MouthSurprised * 100f);
                    if (indexMouthSkinFung >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFung, value_MouthSkinFung * 100f);
                    if (indexMouthSkinFungRight >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungRight, value_MouthSkinFungRight * 100f);
                    if (indexMouthSkinFungLeft >= 0) meshRender.SetBlendShapeWeight(indexMouthSkinFungLeft, value_MouthSkinFungLeft * 100f);
                    if (indexHAHide >= 0) meshRender.SetBlendShapeWeight(indexHAHide, value_HAHide * 100f);
                    if (indexHAFung1 >= 0) meshRender.SetBlendShapeWeight(indexHAFung1, value_HAFung1 * 100f);
                    if (indexHAFung1Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Low, value_HAFung1Low * 100f);
                    if (indexHAFung1Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung1Up, value_HAFung1Up * 100f);
                    if (indexHAFung2 >= 0) meshRender.SetBlendShapeWeight(indexHAFung2, value_HAFung2 * 100f);
                    if (indexHAFung2Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Low, value_HAFung2Low * 100f);
                    if (indexHAFung2Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung2Up, value_HAFung2Up * 100f);
                    if (indexHAFung3 >= 0) meshRender.SetBlendShapeWeight(indexHAFung3, value_HAFung3 * 100f);
                    if (indexHAFung3Up >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Up, value_HAFung3Up * 100f);
                    if (indexHAFung3Low >= 0) meshRender.SetBlendShapeWeight(indexHAFung3Low, value_HAFung3Low * 100f);
                    if (indexHAShort >= 0) meshRender.SetBlendShapeWeight(indexHAShort, value_HAShort * 100f);
                    if (indexHAShortUp >= 0) meshRender.SetBlendShapeWeight(indexHAShortUp, value_HAShortUp * 100f);
                    if (indexHAShortLow >= 0) meshRender.SetBlendShapeWeight(indexHAShortLow, value_HAShortLow * 100f);

                    // ARKit blendshapes
                    int index_browInnerUp = meshRender.sharedMesh.GetBlendShapeIndex("browInnerUp");
                    int index_browDownLeft = meshRender.sharedMesh.GetBlendShapeIndex("browDown_L");
                    int index_browDownRight = meshRender.sharedMesh.GetBlendShapeIndex("browDown_R");
                    int index_browOuterUpLeft = meshRender.sharedMesh.GetBlendShapeIndex("browOuterUp_L");
                    int index_browOuterUpRight = meshRender.sharedMesh.GetBlendShapeIndex("browOuterUp_R");
                    int index_eyeLookUpLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookUp_L");
                    int index_eyeLookUpRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookUp_R");
                    int index_eyeLookDownLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookDown_L");
                    int index_eyeLookDownRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookDown_R");
                    int index_eyeLookInLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookIn_L");
                    int index_eyeLookInRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookIn_R");
                    int index_eyeLookOutLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookOut_L");
                    int index_eyeLookOutRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeLookOut_R");
                    int index_eyeBlinkLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeBlink_L");
                    int index_eyeBlinkRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeBlink_R");
                    int index_eyeSquintLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeSquint_L");
                    int index_eyeSquintRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeSquint_R");
                    int index_eyeWideLeft = meshRender.sharedMesh.GetBlendShapeIndex("eyeWide_L");
                    int index_eyeWideRight = meshRender.sharedMesh.GetBlendShapeIndex("eyeWide_R");
                    int index_cheekPuff = meshRender.sharedMesh.GetBlendShapeIndex("cheekPuff");
                    int index_cheekSquintLeft = meshRender.sharedMesh.GetBlendShapeIndex("cheekSquint_L");
                    int index_cheekSquintRight = meshRender.sharedMesh.GetBlendShapeIndex("cheekSquint_R");
                    int index_noseSneerLeft = meshRender.sharedMesh.GetBlendShapeIndex("noseSneer_L");
                    int index_noseSneerRight = meshRender.sharedMesh.GetBlendShapeIndex("noseSneer_R");
                    int index_jawOpen = meshRender.sharedMesh.GetBlendShapeIndex("jawOpen");
                    int index_jawForward = meshRender.sharedMesh.GetBlendShapeIndex("jawForward");
                    int index_jawLeft = meshRender.sharedMesh.GetBlendShapeIndex("jawLeft");
                    int index_jawRight = meshRender.sharedMesh.GetBlendShapeIndex("jawRight");
                    int index_mouthFunnel = meshRender.sharedMesh.GetBlendShapeIndex("mouthFunnel");
                    int index_mouthPucker = meshRender.sharedMesh.GetBlendShapeIndex("mouthPucker");
                    int index_mouthLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthLeft");
                    int index_mouthRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthRight");
                    int index_mouthRollUpper = meshRender.sharedMesh.GetBlendShapeIndex("mouthRollUpper");
                    int index_mouthRollLower = meshRender.sharedMesh.GetBlendShapeIndex("mouthRollLower");
                    int index_mouthShrugUpper = meshRender.sharedMesh.GetBlendShapeIndex("mouthShrugUpper");
                    int index_mouthShrugLower = meshRender.sharedMesh.GetBlendShapeIndex("mouthShrugLower");
                    int index_mouthClose_ARKit = meshRender.sharedMesh.GetBlendShapeIndex("mouthClose");
                    int index_mouthSmileLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthSmile_L");
                    int index_mouthSmileRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthSmile_R");
                    int index_mouthFrownLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthFrown_L");
                    int index_mouthFrownRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthFrown_R");
                    int index_mouthDimpleLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthDimple_L");
                    int index_mouthDimpleRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthDimple_R");
                    int index_mouthUpperUpLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthUpperUp_L");
                    int index_mouthUpperUpRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthUpperUp_R");
                    int index_mouthLowerDownLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthLowerDown_L");
                    int index_mouthLowerDownRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthLowerDown_R");
                    int index_mouthPressLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthPress_L");
                    int index_mouthPressRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthPress_R");
                    int index_mouthStretchLeft = meshRender.sharedMesh.GetBlendShapeIndex("mouthStretch_L");
                    int index_mouthStretchRight = meshRender.sharedMesh.GetBlendShapeIndex("mouthStretch_R");
                    int index_tongueOut = meshRender.sharedMesh.GetBlendShapeIndex("tongueOut");

                    if (index_browInnerUp >= 0) meshRender.SetBlendShapeWeight(index_browInnerUp, value_browInnerUp * 100f);
                    if (index_browDownLeft >= 0) meshRender.SetBlendShapeWeight(index_browDownLeft, value_browDownLeft * 100f);
                    if (index_browDownRight >= 0) meshRender.SetBlendShapeWeight(index_browDownRight, value_browDownRight * 100f);
                    if (index_browOuterUpLeft >= 0) meshRender.SetBlendShapeWeight(index_browOuterUpLeft, value_browOuterUpLeft * 100f);
                    if (index_browOuterUpRight >= 0) meshRender.SetBlendShapeWeight(index_browOuterUpRight, value_browOuterUpRight * 100f);
                    if (index_eyeLookUpLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeLookUpLeft, value_eyeLookUpLeft * 100f);
                    if (index_eyeLookUpRight >= 0) meshRender.SetBlendShapeWeight(index_eyeLookUpRight, value_eyeLookUpRight * 100f);
                    if (index_eyeLookDownLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeLookDownLeft, value_eyeLookDownLeft * 100f);
                    if (index_eyeLookDownRight >= 0) meshRender.SetBlendShapeWeight(index_eyeLookDownRight, value_eyeLookDownRight * 100f);
                    if (index_eyeLookInLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeLookInLeft, value_eyeLookInLeft * 100f);
                    if (index_eyeLookInRight >= 0) meshRender.SetBlendShapeWeight(index_eyeLookInRight, value_eyeLookInRight * 100f);
                    if (index_eyeLookOutLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeLookOutLeft, value_eyeLookOutLeft * 100f);
                    if (index_eyeLookOutRight >= 0) meshRender.SetBlendShapeWeight(index_eyeLookOutRight, value_eyeLookOutRight * 100f);
                    if (index_eyeBlinkLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeBlinkLeft, value_eyeBlinkLeft * 100f);
                    if (index_eyeBlinkRight >= 0) meshRender.SetBlendShapeWeight(index_eyeBlinkRight, value_eyeBlinkRight * 100f);
                    if (index_eyeSquintLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeSquintLeft, value_eyeSquintLeft * 100f);
                    if (index_eyeSquintRight >= 0) meshRender.SetBlendShapeWeight(index_eyeSquintRight, value_eyeSquintRight * 100f);
                    if (index_eyeWideLeft >= 0) meshRender.SetBlendShapeWeight(index_eyeWideLeft, value_eyeWideLeft * 100f);
                    if (index_eyeWideRight >= 0) meshRender.SetBlendShapeWeight(index_eyeWideRight, value_eyeWideRight * 100f);
                    if (index_cheekPuff >= 0) meshRender.SetBlendShapeWeight(index_cheekPuff, value_cheekPuff * 100f);
                    if (index_cheekSquintLeft >= 0) meshRender.SetBlendShapeWeight(index_cheekSquintLeft, value_cheekSquintLeft * 100f);
                    if (index_cheekSquintRight >= 0) meshRender.SetBlendShapeWeight(index_cheekSquintRight, value_cheekSquintRight * 100f);
                    if (index_noseSneerLeft >= 0) meshRender.SetBlendShapeWeight(index_noseSneerLeft, value_noseSneerLeft * 100f);
                    if (index_noseSneerRight >= 0) meshRender.SetBlendShapeWeight(index_noseSneerRight, value_noseSneerRight * 100f);
                    if (index_jawOpen >= 0) meshRender.SetBlendShapeWeight(index_jawOpen, value_jawOpen * 100f);
                    if (index_jawForward >= 0) meshRender.SetBlendShapeWeight(index_jawForward, value_jawForward * 100f);
                    if (index_jawLeft >= 0) meshRender.SetBlendShapeWeight(index_jawLeft, value_jawLeft * 100f);
                    if (index_jawRight >= 0) meshRender.SetBlendShapeWeight(index_jawRight, value_jawRight * 100f);
                    if (index_mouthFunnel >= 0) meshRender.SetBlendShapeWeight(index_mouthFunnel, value_mouthFunnel * 100f);
                    if (index_mouthPucker >= 0) meshRender.SetBlendShapeWeight(index_mouthPucker, value_mouthPucker * 100f);
                    if (index_mouthLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthLeft, value_mouthLeft * 100f);
                    if (index_mouthRight >= 0) meshRender.SetBlendShapeWeight(index_mouthRight, value_mouthRight * 100f);
                    if (index_mouthRollUpper >= 0) meshRender.SetBlendShapeWeight(index_mouthRollUpper, value_mouthRollUpper * 100f);
                    if (index_mouthRollLower >= 0) meshRender.SetBlendShapeWeight(index_mouthRollLower, value_mouthRollLower * 100f);
                    if (index_mouthShrugUpper >= 0) meshRender.SetBlendShapeWeight(index_mouthShrugUpper, value_mouthShrugUpper * 100f);
                    if (index_mouthShrugLower >= 0) meshRender.SetBlendShapeWeight(index_mouthShrugLower, value_mouthShrugLower * 100f);
                    if (index_mouthClose_ARKit >= 0) meshRender.SetBlendShapeWeight(index_mouthClose_ARKit, value_mouthClose_ARKit * 100f);
                    if (index_mouthSmileLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthSmileLeft, value_mouthSmileLeft * 100f);
                    if (index_mouthSmileRight >= 0) meshRender.SetBlendShapeWeight(index_mouthSmileRight, value_mouthSmileRight * 100f);
                    if (index_mouthFrownLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthFrownLeft, value_mouthFrownLeft * 100f);
                    if (index_mouthFrownRight >= 0) meshRender.SetBlendShapeWeight(index_mouthFrownRight, value_mouthFrownRight * 100f);
                    if (index_mouthDimpleLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthDimpleLeft, value_mouthDimpleLeft * 100f);
                    if (index_mouthDimpleRight >= 0) meshRender.SetBlendShapeWeight(index_mouthDimpleRight, value_mouthDimpleRight * 100f);
                    if (index_mouthUpperUpLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthUpperUpLeft, value_mouthUpperUpLeft * 100f);
                    if (index_mouthUpperUpRight >= 0) meshRender.SetBlendShapeWeight(index_mouthUpperUpRight, value_mouthUpperUpRight * 100f);
                    if (index_mouthLowerDownLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthLowerDownLeft, value_mouthLowerDownLeft * 100f);
                    if (index_mouthLowerDownRight >= 0) meshRender.SetBlendShapeWeight(index_mouthLowerDownRight, value_mouthLowerDownRight * 100f);
                    if (index_mouthPressLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthPressLeft, value_mouthPressLeft * 100f);
                    if (index_mouthPressRight >= 0) meshRender.SetBlendShapeWeight(index_mouthPressRight, value_mouthPressRight * 100f);
                    if (index_mouthStretchLeft >= 0) meshRender.SetBlendShapeWeight(index_mouthStretchLeft, value_mouthStretchLeft * 100f);
                    if (index_mouthStretchRight >= 0) meshRender.SetBlendShapeWeight(index_mouthStretchRight, value_mouthStretchRight * 100f);
                    if (index_tongueOut >= 0) meshRender.SetBlendShapeWeight(index_tongueOut, value_tongueOut * 100f);
                }
            }
        }


    }




}