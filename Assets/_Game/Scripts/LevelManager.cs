using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject spidersandPrefab;
    [SerializeField] private GameObject bearPrefab;
    [SerializeField] public Transform topLeftCorner;
    [SerializeField] public Transform bottomRightCorner;
    [SerializeField] private GameObject nextLevelPrefab;
    public List<EnemyBaseController> enemies = new List<EnemyBaseController>();

    private float spawnTimer = 0f;
  
    public float currentLevel;
    //public GameManager gameManager;


    public static LevelManager Instance { get; private set; }
    public bool IsBoss = false;
    public bool IsCompleted = false;
    private int count = 0;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        currentLevel = 1;
        IsBoss = false;

    }

    void Update()
    {
        if (GameManager.instance.IsGame)
        {
            switch (currentLevel)
            {
                case 1:
                    if (!IsCompleted /*&& enemies.Count <= 15f*/)
                    {
                        spawnTimer -= Time.deltaTime;
                        if (spawnTimer <= 0f)
                        {
                            SpawnEnemy(spidersandPrefab);
                            count++;
                            spawnTimer = 5f;
                        }
                        if (count == 15)
                        {
                            IsCompleted = true;
                        }
                    }
                    if (enemies.Count == 0 && IsCompleted)
                    {

                        currentLevel++;
                        spawnTimer = 0f;
                        IsCompleted = false;
                    }

                    break;
                case 2:
                    if (!IsCompleted)
                    {
                        spawnTimer -= Time.deltaTime;
                        if (spawnTimer <= 0f)
                        {
                            SpawnEnemy(spiderPrefab);
                            count++;
                            spawnTimer = 5f;
                        }
                        if (count == 30)
                        {
                            IsCompleted = true;
                        }
                    }
                    if (enemies.Count == 0 && IsCompleted)
                    {

                        currentLevel++;
                        spawnTimer = 0f;
                        IsCompleted = false;
                    }

                    break;
                case 3:
                    if (!IsCompleted)
                    {
                        spawnTimer -= Time.deltaTime;
                        if (spawnTimer <= 0f)
                        {
                            SpawnEnemy(bearPrefab);
                            count++;
                            spawnTimer = 5f;
                        }
                        if (count == 40)
                        {
                            IsCompleted = true;
                        }
                    }
                    if (enemies.Count == 0 && IsCompleted)
                    {

                        currentLevel++;
                        spawnTimer = 0f;
                        IsCompleted = false;
                    }

                    break;
                case 4:
                    if (!IsBoss)
                    {
                        SpawnEnemy(bossPrefab);
                        IsBoss = true;
                    }
                    if (enemies.Count == 0)
                    {
                        currentLevel++;
                        spawnTimer = 0f;
                    }

                    break;
                case 5:
                    // EndGame
                    Debug.Log("End");
                    break;

            }
        }
        

            
    }
    private void LevelUp()
    {

    }

    private void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyPrefab = Instantiate(enemy, GetRandomPositionInSquare(), Quaternion.identity);
        EnemyBaseController enemyController = enemyPrefab.GetComponent<EnemyBaseController>();
        enemies.Add(enemyController);
    }


    public void DeathEnemy(EnemyBaseController enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DeathHero(HeroController hero)
    {
        Destroy(hero.gameObject);
    }

    public Vector3 GetRandomPositionInSquare()
    {

        float randomX = Random.Range(topLeftCorner.position.x, bottomRightCorner.position.x);
        float randomZ = Random.Range(topLeftCorner.position.z, bottomRightCorner.position.z);

        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

        return randomPosition;
    }
}
