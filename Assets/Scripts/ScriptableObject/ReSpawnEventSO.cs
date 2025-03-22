using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ReSpawnEventSO")]
public class ReSpawnEventSO : ScriptableObject
{
    public UnityAction<Vector3> ReSpawn;

    public void RaisedRespawnEvent(Vector3 posToGo)
    {
        ReSpawn?.Invoke(posToGo);
    }
}