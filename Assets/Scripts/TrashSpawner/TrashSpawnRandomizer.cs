using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnRandomizer : MonoBehaviour
{
    [SerializeField] private LevelDataSO levelDataSO;
    [SerializeField] private Collider2D spawnAreaCollider;
    [SerializeField] private LayerMask layerTrashCantSpawnOn;
    [Tooltip("Determine the Sprite Size of the trash. Is used to make sure the sprite doesn't go into walls and objects")] 
    [SerializeField] private float trashCircleSize = 1.5f;

    public PickableItem SpawnRandomTrash()
    {
        PickableItem trashToSpawn = GetRandomTrashFromPool();
        Vector2 randomPosition = GetRandomSpawnPos();

        return Instantiate(trashToSpawn, randomPosition, Quaternion.identity, this.transform);    
    }

    private PickableItem GetRandomTrashFromPool()
    {
        float totalWeight = 0;

        foreach(TrashPoolItem trash in levelDataSO.TrashPool)
        {
            totalWeight += trash.SpawnChanceWeight;
        }

        float randNum = Random.Range(0f, totalWeight);

        float cumulativeWeight = 0f;

        foreach(TrashPoolItem trash in levelDataSO.TrashPool)
        {
            cumulativeWeight += trash.SpawnChanceWeight;

            if(randNum <= cumulativeWeight)
            {
                return trash.Trash;
            }
        }

        return null;
    }

    private Vector2 GetRandomSpawnPos(int maxAttemps = 1000)
    {
        Vector2 spawnPos = Vector2.zero;
        bool isSpawnValid = false;
        int attempCount = 0;

        while(!isSpawnValid && attempCount < maxAttemps)
        {
            spawnPos = GetRandomPositionInCollider();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, trashCircleSize);

            bool isInvalidCollision = false;
            foreach(Collider2D coll in colliders)
            {
                if (((1 << coll.gameObject.layer) & layerTrashCantSpawnOn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if(!isInvalidCollision)
            {
                isSpawnValid = true;
            }

            attempCount++;
        }
        if(!isSpawnValid)
        {
            Debug.LogWarning("Couldn't find a valid spawn position within max attemps");
        }
        
        return spawnPos;
    }

    private Vector2 GetRandomPositionInCollider()
    {
        Bounds bounds = spawnAreaCollider.bounds;

        Vector2 minBounds = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 maxBounds = new Vector2(bounds.max.x, bounds.max.y);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }
}
