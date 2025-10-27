using UnityEngine;
using UnityEngine.Playables;

public class DialoguePlayableAsset : PlayableAsset
{
    public ExposedReference<Dialogue> manager;
    public int startIndex;
    public int endIndex;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialoguePlayable>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.manager = manager.Resolve(graph.GetResolver());
        behaviour.startIndex = startIndex;
        behaviour.endIndex = endIndex;
        return playable;
    }

}
