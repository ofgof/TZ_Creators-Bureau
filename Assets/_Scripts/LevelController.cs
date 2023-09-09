using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelConfig _config;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _bossPart;

    [SerializeField] private Vector3 _startSpawnPosition;

    public void Init()
    {
        CreateLevel();
        GameEvents.OnGameRestart += CreateLevel;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameRestart -= CreateLevel;
    }
    private void CreateLevel()
    {
        int levelLength = Random.Range(_config.minLevelLength, _config.maxLevelLength);
        var scale = _ground.transform.localScale;
        scale.z = levelLength;
        _ground.transform.localScale = scale;

        var bossPosition = _bossPart.transform.position;
        bossPosition.z = levelLength;
        _bossPart.transform.position = bossPosition;
        _bossPart.SetActive(true);

        var spawnPosition = _startSpawnPosition;
        for (int i = 0; i < (int)(levelLength / _config.distanceBetweenEnemy) - 3; i++)
        {
            spawnPosition.x = Random.Range(-3f, 3f);
            spawnPosition.z += _config.distanceBetweenEnemy;
            SpawnEnemy(spawnPosition);
        }
    }
    private void SpawnEnemy(Vector3 position)
    {
        var enemy = Instantiate(_enemyPrefab, transform);
        enemy.transform.position = position;
        enemy.GetComponent<Enemy>().Init();
    }
}
