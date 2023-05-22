using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public event Action OnWaveChanged;
    public event Action<float, float> OnChangedWaveStatus;
    public event Action OnAllWave;

    public int WaveIndex { get; private set; }

    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<int> countOfEnemyInWave;
    [SerializeField] private float timeForNewSpawn;

    private List<Transform> spawnPoints;
    private PlayerAI playerAI;
    private int nowKilled;
    private bool isSpawnning = true;

    public void GetSpawnPonints(List<Transform> SpawnPoints)
    {
        spawnPoints = SpawnPoints;
    }

    private void OnEnable()
    {
        playerAI = GameManager.instance.PlayerAI;
        WaveIndex = 0;
        StartCoroutine(SpawnEnemiesLoop());
    }

    private void RestartCharges()
    {
        if (playerAI.Modificators.Count != 0)
            foreach (Modificator modificator in playerAI.Modificators.Cast<Modificator>())
                modificator.ResetCharge();
    }

    private IEnumerator SpawnEnemiesLoop()
    {
        yield return new WaitForSecondsRealtime(3f);

        while (true)
        {
            if (enemiesContainer.childCount == 0 && WaveIndex == countOfEnemyInWave.Count)
            {
                OnAllWave?.Invoke(); Debug.Log($"That's All");
                break;
            }
            else if (enemiesContainer.childCount == 0 && WaveIndex != countOfEnemyInWave.Count - 1)
            {
                OnWaveChanged?.Invoke();
                RestartCharges();
                nowKilled = 0;
                ChooseAndSpawnRandomWave();
                yield return null;
                yield return new WaitWhile(() => isSpawnning);

                if (WaveIndex != countOfEnemyInWave.Count)
                    WaveIndex++;
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private void ChooseAndSpawnRandomWave()
    {
        isSpawnning = true;
        List<Transform> positions = new();
        int minCountOfWaves = 1;
        int maxCountOfWaves = 14;
        int numberOfWave = Random.Range(minCountOfWaves, maxCountOfWaves);

        switch (numberOfWave)
        {
            case 1:
                StartCoroutine(SpawnSoloWave(spawnPoints[Random.Range(0, spawnPoints.Count - 2)]));
                break;
            case 2:
                StartCoroutine(SpawnWaves(GetPointsForSpawn(2)));
                break;
            case 3:
                StartCoroutine(SpawnWaves(GetPointsForSpawn(3)));
                break;
            case 4:
                StartCoroutine(SpawnWaves(GetPointsForSpawn(4)));
                break;
            case 5:
                StartCoroutine(SpawnWaves(GetPointsForSpawn(5)));
                break;
            case 6:
                StartCoroutine(SpawnWaves(GetPointsForSpawn(6)));
                break;
            case 7:
                positions.Add(spawnPoints[0]); positions.Add(spawnPoints[4]); StartCoroutine(SpawnWaves(positions));
                break;
            case 8:
                positions.Add(spawnPoints[1]); positions.Add(spawnPoints[4]); positions.Add(spawnPoints[6]); StartCoroutine(SpawnWaves(positions));
                break;
            case 9:
                positions.Add(spawnPoints[5]); positions.Add(spawnPoints[6]); positions.Add(spawnPoints[7]); StartCoroutine(SpawnWaves(positions));
                break;
            case 10:
                positions.Add(spawnPoints[1]); positions.Add(spawnPoints[2]); positions.Add(spawnPoints[3]); StartCoroutine(SpawnWaves(positions));
                break;
            case 11:
                positions.Add(spawnPoints[0]); positions.Add(spawnPoints[1]); positions.Add(spawnPoints[7]); StartCoroutine(SpawnWaves(positions));
                break;
            case 12:
                positions.Add(spawnPoints[3]); positions.Add(spawnPoints[4]); positions.Add(spawnPoints[5]); StartCoroutine(SpawnWaves(positions));
                break;
            case 13:
                StartCoroutine(SpawnSoloWave(spawnPoints[^1]));
                break;
            case 14:
                positions = spawnPoints; positions.RemoveAt(positions.Count - 1); StartCoroutine(SpawnWaves(positions));
                break;
        }
    }


    private List<Transform> GetPointsForSpawn(int countOfPositions)
    {
        List<Transform> positions = new();
        Transform pos;
        bool isHavePosition = false;

        while (positions.Count != countOfPositions)
            {
            isHavePosition = false;
            pos = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];

            for (int i = 0; i < positions.Count; i++)
            {
                if (pos == positions[i])
                    isHavePosition = true;
            }

            if (isHavePosition == false)
                positions.Add(pos);
        }

        return positions;
    }

    private IEnumerator SpawnWaves(List<Transform> positions)
    {
        int countOfEnemiesInThisWave = countOfEnemyInWave[WaveIndex];

        while (countOfEnemiesInThisWave > 0)
        {
            countOfEnemiesInThisWave--;
            SpawnEntity(positions[Random.Range(0, positions.Count - 1)].position + (Random.insideUnitSphere * 10));
            yield return new WaitForSecondsRealtime(timeForNewSpawn);
        }

        isSpawnning = false;
    }

    private IEnumerator SpawnSoloWave(Transform pos)
    {
        int countOfEnemiesInThisWave = countOfEnemyInWave[WaveIndex];

        while (countOfEnemiesInThisWave > 0)
        {
            countOfEnemiesInThisWave--;
            SpawnEntity(pos.position + (Random.insideUnitSphere * 10));
            yield return new WaitForSecondsRealtime(timeForNewSpawn);
        }

        isSpawnning = false;
    }

    private void SpawnEntity(Vector3 pos)
    {
        Entity enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], pos, Quaternion.identity, enemiesContainer).GetComponent<Entity>();
        enemy.CurrentHealth = enemy.MaxHealth;
        enemy.OnDie -= () => { };
        enemy.OnDie += () =>
        {
            PlayerData data = GameManager.instance.PlayerData;
            data.XP += enemy.GetComponent<EnemyData>().XPForKill;
            data.Gold += enemy.GetComponent<EnemyData>().GoldForKill;
            nowKilled++;
            OnChangedWaveStatus?.Invoke(countOfEnemyInWave[WaveIndex], nowKilled);
        };
    }
}
