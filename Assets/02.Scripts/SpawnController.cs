using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;
using static Define;

public class SpawnController : MonoBehaviour
{
    public static SpawnController spawnInstance;

    public Transform target;

    private bool isGameOver = false;
    public int spawnMonsterCount;
    private List<Vector3> monsterSpawnPos = new List<Vector3>();
    public float spawnRadius = 10f;


    public int groupSpawnMinRange;

    public int groupSpawnMaxRange;

    public float normalSpawnTime;
    public float delaySpawnTimeMin;
    public float delaySpawnTimeMax;
    public float groupSpawnTime;



    private void Awake()
    {
        spawnInstance = this;
    }

    public void StartSpawning(int stageMaxMonsterCount, int monsterCount, string monsterName, SpawnType spawnType)
    {
        switch (spawnType)
        {
            case SpawnType.Normal:
                StartCoroutine(this.NormalSpawn(monsterCount, monsterName, stageMaxMonsterCount));
                break;
            case SpawnType.Delay:
                StartCoroutine(this.DelaySpawn(monsterCount, monsterName, stageMaxMonsterCount));
                break;
            case SpawnType.Group:
                StartCoroutine(this.GroupSpawn(monsterCount, monsterName, stageMaxMonsterCount));
                break;
        }
    }

    public void StopSpawning(int stageMaxMonsterCount)
    {
        if (spawnMonsterCount == stageMaxMonsterCount)
        {
            spawnMonsterCount = 0;
            Debug.Log("모든 몬스터 소환 완료");
            StopAllCoroutines();
        }
    }

    IEnumerator NormalSpawn(int maxMonsterCount, string _monsterName, int stageMaxMonster)
    {
        while(!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if(monsterCount < maxMonsterCount && spawnMonsterCount < stageMaxMonster)
            {
                Vector3 spawnPosition = GetRandomPositionAroundPlayer();

                yield return new WaitForSeconds(normalSpawnTime);
                SpawnMonster(_monsterName, spawnPosition, stageMaxMonster);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator DelaySpawn(int maxMonsterCount, string _monsterName, int stageMaxMonster)
    {
        while (!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if (monsterCount < maxMonsterCount && spawnMonsterCount < stageMaxMonster)
            {
                Vector3 spawnPosition = GetRandomPositionAroundPlayer();

                yield return new WaitForSeconds(Random.Range(delaySpawnTimeMin, delaySpawnTimeMax));
                SpawnMonster(_monsterName, spawnPosition, stageMaxMonster);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator GroupSpawn(int maxMonsterCount, string _monsterName, int stageMaxMonster)
    {
        while (!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if (monsterCount < maxMonsterCount && spawnMonsterCount < stageMaxMonster)
            {
                int groupSize = Random.Range(groupSpawnMinRange, groupSpawnMaxRange);

                if(stageMaxMonster - spawnMonsterCount < groupSize)
                {
                    groupSize = stageMaxMonster - spawnMonsterCount;
                }

                Vector3 groupCenter = GetRandomPositionAroundPlayer();

                for (int i = 0; i < groupSize; i++)
                {
                    Vector3 spawnPositionOffset = CalculateFormationOffset(i, groupSize);

                    Vector3 spawnPosition = groupCenter + spawnPositionOffset;

                    SpawnMonster(_monsterName, spawnPosition, stageMaxMonster);
                }

                yield return new WaitForSeconds(groupSpawnTime);
            }
            else
            {
                yield return null;
                continue;
            }

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

    void SpawnMonster(string _monsterName, Vector3 spawnPos, int stageMaxMonsterCount)
    {
        spawnMonsterCount++;

        Managers.Pool.GetPool(_monsterName).GetGameObject(_monsterName, spawnPos);
        monsterSpawnPos.Add(spawnPos);

        if (spawnMonsterCount == stageMaxMonsterCount)
        {
            StopSpawning(stageMaxMonsterCount);
        }
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDirection2D = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 randomDirection3D = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y);

        Vector3 randomPosition = target.position + randomDirection3D;

        return randomPosition;
    }

}
