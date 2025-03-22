using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    public void RaiseLoadRequestEvent(GameSceneSO loacationToLoad,Vector3 posToGo,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(loacationToLoad, posToGo, fadeScreen);
    }
}