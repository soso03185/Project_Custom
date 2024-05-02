using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;
using static Define;

public class SpawnManager : MonoBehaviour
{
    public Transform target;
    public float spawnRadius = 10f;
    public int maxMonster;
    public int minMonster;
    private bool isGameOver = false;
    public int totalMonsterCount;
    private List<Vector3> monsterSpawnPos = new List<Vector3>();


    [System.Serializable]
    public struct SpawnOptions
    {
        public SpawnType spawnType;
        public string monsterName;
        public int maxMonsterCount;
        public bool isSpawn;
    }

    public List<SpawnOptions> spawnList = new List<SpawnOptions>();

    // Start is called before the first frame update
    void Start()
    {

        int monsterListSize = spawnList.Count;

        for (int i = 0; i < monsterListSize; i++)
        {
            if (spawnList[i].isSpawn)
            {
                switch (spawnList[i].spawnType)
                {
                    case SpawnType.Noraml:
                        StartCoroutine(this.CreateMonster(spawnList[i].maxMonsterCount, spawnList[i].monsterName));
                        break;
                    case SpawnType.Delay:
                        StartCoroutine(this.CreateMonsterDelay(spawnList[i].maxMonsterCount, spawnList[i].monsterName));
                        break;
                    case SpawnType.Group:
                        StartCoroutine(this.CreateMonsterGroup(spawnList[i].maxMonsterCount, spawnList[i].monsterName));
                        break;
                }
            }
        }
    }

    IEnumerator CreateMonster(int maxmonsterCount, string _monsterName)
    {
        while(!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if(monsterCount < maxmonsterCount)
            {
                yield return new WaitForSeconds(0.2f);
                totalMonsterCount++;
                SpawnMonster(_monsterName);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator CreateMonsterDelay(int maxMonsterCount, string _monsterName)
    {
        while (!isGameOver)
        {
            int monsterCount = Managers.Pool.GetPool(_monsterName).activeCount;

            if (monsterCount < maxMonsterCount)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 3.0f));
                totalMonsterCount++;
                SpawnMonster(_monsterName);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator CreateMonsterGroup(int maxmonsterCount, string _monsterName)
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

                //ObjectPool.Instance.GetGameObject(spawnPosition, _monsterName);
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
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();

        //ObjectPool.Instance.GetGameObject(spawnPosition, _monsterName);

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
