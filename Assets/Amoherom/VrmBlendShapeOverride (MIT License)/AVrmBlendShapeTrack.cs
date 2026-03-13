using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Amoherom
{
    [TrackColor(0.5f, 0.7f, 0.5f)]
    [TrackBindingType(typeof(UniVRM10.Vrm10Instance))]
    [TrackClipType(typeof(VrmBlendShapeClip))]
    public class VrmBlendShapeTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = ScriptPlayable<VrmBlendShapeMixerBehaviour>.Create(graph, inputCount);
            mixer.GetBehaviour().Clips = GetClips().ToArray();
            mixer.GetBehaviour().Director = go.GetComponent<PlayableDirector>();

            // ���O�ύX
            foreach (TimelineClip clip in m_Clips)
            {
                var playableAsset = clip.asset as VrmBlendShapeClip;
                var presets = playableAsset.behaviour.blendShapes;
                clip.displayName = presets.Count == 0
                    ? "Empty"
                    : string.Join("+", presets.ConvertAll(e => e.preset.ToString()));
            }
            return mixer;
        }
    }
}