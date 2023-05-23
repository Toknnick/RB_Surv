using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Space(12)]
    [SerializeField] private List<GameObject> traps;
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private List<GameObject> blocks;
    [Space(12)]
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<GameObject> walls;
    [Header("Левый верхний угол")]
    [SerializeField] private Vector3 startPosition;
    [Header("Количество в блоках")]
    [SerializeField] private int width = 60;
    [Space(12)]
    [SerializeField] private int minCountOfObjects;
    [SerializeField] private int maxCountOfObjects;
    [Space(12)]
    [SerializeField] private GameObject containerOfBlocks;
    [SerializeField] private GameObject containerOfTraps;
    [SerializeField] private GameObject containerOfObstacles;
    [SerializeField] private GameObject containerOfSpawners;

    private List<Transform> spawnPoints = new();
    private bool isSpawnning = false;
    private bool isSpawnningByWidth = false;

    private void Awake()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        List<Transform> spawnedObjects = new();
        StartCoroutine(SpawnObjects(obstacles, Random.Range(minCountOfObjects, maxCountOfObjects), spawnedObjects, containerOfObstacles));
        yield return new WaitWhile(() => isSpawnning);

        spawnedObjects = new();
        StartCoroutine(SpawnObjects(traps, Random.Range(minCountOfObjects, maxCountOfObjects), spawnedObjects, containerOfTraps));
        yield return new WaitWhile(() => isSpawnning);

        List<GameObject> spawnPoints = new()
        {
            spawnPoint
        };
        StartCoroutine(SpawnObjects(spawnPoints, 9, this.spawnPoints, containerOfSpawners));
        yield return new WaitWhile(() => isSpawnning);

        StartCoroutine(SpawnWalls());
        StartCoroutine(SpawnGround());
        //StartCoroutine(SpawnSpawnPoints());
        yield return new WaitWhile(() => isSpawnning);
        gameManager = Instantiate(gameManager, new Vector3(0f, 6.7f, -4.2f), Quaternion.identity);
        GameManager.instance.SpawnManager.GetSpawnPonints(this.spawnPoints);
    }

    private IEnumerator SpawnGround()
    {
        isSpawnning = true;
        Vector3 pos = startPosition;
        float lenght = width / 10;

        while (lenght > 0)
        {
            StartCoroutine(SpawnByWidth(pos));
            yield return new WaitWhile(() => isSpawnningByWidth);
            yield return null;

            lenght--;
            pos.x = startPosition.x;
            pos.y = startPosition.y;
            pos.z -= 10;
        }
        containerOfBlocks.transform.position = new(-5, 0, -5);
        isSpawnning = false;
    }

    private IEnumerator SpawnObjects(List<GameObject> gameObjects, int countOfObstacles, List<Transform> spawnedObjects, GameObject container)
    {
        int nowCountOfAttemps = 0;
        isSpawnning = true;
        GameObject preefab;

        while (spawnedObjects.Count < countOfObstacles)
        {
            if (nowCountOfAttemps > 200)
                break;

            nowCountOfAttemps++;
            preefab = gameObjects[Random.Range(0, gameObjects.Count)];
            yield return null;
            Vector3 randomPosition = new(Random.Range(startPosition.x - 2, startPosition.x - width + 2), preefab.transform.localScale.y / 2,
                                         Random.Range(startPosition.z - 2, startPosition.z - width + 2));
            Collider[] colliders = Physics.OverlapSphere(randomPosition, preefab.transform.localScale.magnitude);

            // Проверка наложения объектов
            if (colliders.Length == 0)
            {
                if (CheckObjectOverlap(randomPosition, spawnedObjects))
                {
                    GameObject spawnedObject = Instantiate(preefab, randomPosition, Quaternion.identity, container.transform);
                    spawnedObjects.Add(spawnedObject.transform);
                    yield return null;
                }
            }
        }

        isSpawnning = false;
    }

    private bool CheckObjectOverlap(Vector3 position, List<Transform> spawnedObjects)
    {
        if (spawnedObjects.Count != 0)
        {
            foreach (Transform obj in spawnedObjects)
            {
                // Проверяем наложение границ объектов
                if (Vector3.Distance(obj.transform.position, position) > 4)
                    return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private IEnumerator SpawnWalls()
    {
        isSpawnning = true;
        Instantiate(walls[0], new Vector3(startPosition.x, 0f, startPosition.z - width / 2), Quaternion.identity);
        yield return null;
        Instantiate(walls[1], new Vector3(startPosition.x - width / 2, 0f, -startPosition.z), Quaternion.identity).transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        yield return null;
        Instantiate(walls[0], new Vector3(-startPosition.x, 0f, startPosition.z - width / 2), Quaternion.identity);
        yield return null;
        Instantiate(walls[1], new Vector3(startPosition.x - width / 2, 0f, startPosition.z), Quaternion.identity).transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        yield return null;
        isSpawnning = false;
    }

    private IEnumerator SpawnByWidth(Vector3 pos)
    {
        isSpawnningByWidth = true;
        bool isSpawnningConnectingBlocks = false;
        int defaultWidth = width / 10;
        int numberOfBlock;
        GameObject block;
        yield return null;

        while (defaultWidth > 0)
        {
            yield return null;
            numberOfBlock = Random.Range(0, blocks.Count);
            block = Instantiate(blocks[numberOfBlock], pos, Quaternion.identity, containerOfBlocks.transform);
            defaultWidth--;
            pos.x -= 10;

            if (block.CompareTag("ConnectingBlock") && isSpawnningConnectingBlocks == false)
            {
                yield return null;
                isSpawnningConnectingBlocks = true;
                Instantiate(blocks[numberOfBlock], pos, Quaternion.identity, containerOfBlocks.transform);
                pos.x -= 10;
                defaultWidth--;
            }
            else
            {
                yield return null;
                isSpawnningConnectingBlocks = false;
                numberOfBlock = Random.Range(0, blocks.Count);
                Instantiate(blocks[numberOfBlock], pos, Quaternion.identity, containerOfBlocks.transform);
                pos.x -= 10;
                defaultWidth--;
            }
        }

        isSpawnningByWidth = false;
    }
}
