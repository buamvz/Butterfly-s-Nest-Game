using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.1f, 0.6f, 1f)] // optional, color in Timeline
[TrackClipType(typeof(DialoguePlayableAsset))] // what clips this track can hold
[TrackBindingType(typeof(Dialogue))] // what type of object this track binds to
public class DialogueTrack : TrackAsset
{
    // You can leave this empty unless you need custom mixer behaviour
}
