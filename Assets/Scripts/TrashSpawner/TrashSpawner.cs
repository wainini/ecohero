using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private TrashSpawnRandomizer trashRandomizer;
    [SerializeField] public SpawnMode spawnMode;

    [SerializeField] private int trashToSpawnOnStart;
    [SerializeField] private int maxTrash;

    [Header("Spawn Terus")]
    [SerializeField] private float spawnCooldown;
    private float spawnCooldownTimer;

    private List<PickableItem> spawnedTrash = new List<PickableItem>();

    private void Start()
    {
        for(int i = 0; i < trashToSpawnOnStart; i++)
        {
            SpawnAndAddToList();
        }
    }

    private void FixedUpdate()
    {
        if(spawnMode == SpawnMode.SpawnTerus && spawnedTrash.Count < maxTrash)
        {
            spawnCooldownTimer -= Time.deltaTime;

            if(spawnCooldownTimer <= 0)
            {
                SpawnAndAddToList();
                spawnCooldownTimer = spawnCooldown;
            }
        }        
    }

    private void SpawnAndAddToList()
    {
        PickableItem trash = trashRandomizer.SpawnRandomTrash();
        trash.OnPickUp += RemoveTrashFromList;
        spawnedTrash.Add(trash);
    }

    private void RemoveTrashFromList(PickableItem trash)
    {
        trash.OnPickUp -= RemoveTrashFromList;
        spawnedTrash.Remove(trash);

        if(spawnMode == SpawnMode.KaloPlayerNgambilBaruSpawn)
        {
            SpawnAndAddToList();
        }
        else if(spawnMode == SpawnMode.KaloTrashAbisBaruSpawn && spawnedTrash.Count == 0)
        {
            for (int i = 0; i < trashToSpawnOnStart; i++)
            {
                SpawnAndAddToList();
            }
        }

    }


}

public enum SpawnMode
{
    SpawnTerus,
    KaloPlayerNgambilBaruSpawn,
    KaloTrashAbisBaruSpawn
}
