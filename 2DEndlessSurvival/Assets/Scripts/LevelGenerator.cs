using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform levelPartStart;
    [SerializeField] private Transform levelPart_1;
    [SerializeField] private Player player;
    [SerializeField] private EnemiesSO[] enemiesSOArray;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPartStart.Find("EndPosition").position;
        SpawnLevelPart();
    }

    private void Update()
    {
        if(Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position; 
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private void SpawnEnemies()
    {
    }
}
