using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;
using static Define;

public class SpawnSystem : MonoBehaviour
{
    public Transform target;
    public float spawnRadius = 10f;
    public int maxMonster;
    public int minMonster;
    private bool isGameOver = false;
    public int totalMonsterCount;
    private List<Vector3> monsterSpawnPos = new List<Vector3>();

    public List<SpawnOptions> spawnList = new List<SpawnOptions>();

    [System.Serializable]
    public struct SpawnOptions
    {
        public SpawnType spawnType;
        public MonsterName monsterName;
        public int maxMonsterCount;
        public bool isSpawn;
    }

    private void Start()
    {
        StartSpawning();
    }
    public void StartSpawning()
    {
        int monsterListSize = spawnList.Count;

        for (int i = 0; i < monsterListSize; i++)
        {
            if (spawnList[i].isSpawn)
            {
                switch (spawnList[i].spawnType)
                {
                    case SpawnType.Noraml:
                        CoroutineHandler.instance.StartMyCoroutine(this.NormalSpawn(spawnList[i].maxMonsterCount, spawnList[i].monsterName.ToString()));
                        break;
                    case SpawnType.Delay:
                        CoroutineHandler.instance.StartMyCoroutine(this.DelaySpawn(spawnList[i].maxMonsterCount, spawnList[i].monsterName.ToString()));
                        break;
                    case SpawnType.Group:
                        CoroutineHandler.instance.StartMyCoroutine(this.GroupSpawn(spawnList[i].maxMonsterCount, spawnList[i].monsterName.ToString()));
                        break;
                }
            }
        }
    }

    IEnumerator NormalSpawn(int maxmonsterCount, string _monsterName)
    {
        while(!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if(monsterCount < maxmonsterCount)
            {
                yield return new WaitForSeconds(0.2f);
                SpawnMonster(_monsterName);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator DelaySpawn(int maxMonsterCount, string _monsterName)
    {
        while (!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if (monsterCount < maxMonsterCount)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 3.0f));
                SpawnMonster(_monsterName);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator GroupSpawn(int maxmonsterCount, string _monsterName)
    {
        while (!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;
            if (monsterCount >= maxmonsterCount)
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

                totalMonsterCount++;
                Managers.Pool.GetPool(_monsterName).GetGameObject(_monsterName, spawnPosition);
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

    void SpawnMonster(string _monsterName)
    {
        totalMonsterCount++;
        
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();

        Managers.Pool.GetPool(_monsterName).GetGameObject(_monsterName, spawnPosition);
        monsterSpawnPos.Add(spawnPosition);
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDirection2D = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 randomDirection3D = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y);

        Vector3 randomPosition = target.position + randomDirection3D;

        return randomPosition;
    }



}
