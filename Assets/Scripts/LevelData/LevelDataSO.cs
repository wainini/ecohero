using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelData", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    [field: SerializeField] public List<TrashPoolItem> TrashPool;

}

[System.Serializable]
public class TrashPoolItem
{
    [field: SerializeField] public PickableItem Trash;
    [field: Range(0f,1f)] [field: SerializeField] public float SpawnChanceWeight { get; private set; }
}
