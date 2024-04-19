using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public Transform Player;
    public GameObject monsterPrefab;
    public float spawnRadius = 10f;
    public int maxMonster;
    public int minMonster;
    public bool isGameOver = false;
    public int monsterCount;
    private List<Vector3> monsterSpawnPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        // 최소치 몬스터 생성
        StartCoroutine(this.CreateMonsterGroup());
        //StartCoroutine(this.CreateMonster());

    }

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if (monsterCount < minMonster)
            {
                yield return new WaitForSeconds(0.2f);

                SpawnMonster();
            }
            else if(monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 3.0f));

                SpawnMonster();
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator CreateMonsterGroup()
    {
        while (!isGameOver)
        {
            monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;
            if (monsterCount >= maxMonster)
            {
                yield return null;
                continue;
            }

            int groupSize = Random.Range(3, 7);

            Vector3 groupCenter = GetRandomPositionAroundPlayer();

            for (int i = 0; i < groupSize; i++)
            {
                Vector3 spawnPositionOffset = CalculateFormationOffset(i, groupSize);

                Vector3 spawnPosition = groupCenter + spawnPositionOffset;

                ObjectPool.Instance.GetGameObject(spawnPosition);
                monsterSpawnPos.Add(spawnPosition);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    Vector3 CalculateFormationOffset(int index, int groupSize)
    {
        float angle = (float)index / groupSize * Mathf.PI * 2f;
        float radius = 3f; 
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0f, z);
    }

    void SpawnMonster()
    {
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();

        ObjectPool.Instance.GetGameObject(spawnPosition);
        monsterSpawnPos.Add(spawnPosition);
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDirection2D = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 randomDirection3D = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y);

        Vector3 randomPosition = Player.position + randomDirection3D;

        return randomPosition;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
            
    }
}
