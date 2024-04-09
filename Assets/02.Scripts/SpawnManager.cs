using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public Transform Player;
    public GameObject monsterPrefab;
    public float spawnRadius = 10f;
    public int maxMonster = 10;

    public bool isGameOver = false;

    private List<Vector3> monsterSpawnPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if (monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(0.2f);

                Vector3 spawnPosition = GetRandomPositionAroundPlayer();

                bool overlap = CheckOverlap(spawnPosition);

                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                monsterSpawnPos.Add(spawnPosition);
            }
            else
            {
                yield return null;
            }
        }
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDirection2D = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 randomDirection3D = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y);

        Vector3 randomPosition = Player.position + randomDirection3D;

        return randomPosition;
    }

    bool CheckOverlap(Vector3 pos)
    {
        foreach (Vector3 spawnPos in monsterSpawnPos)
        {
            if (Vector3.Distance(pos, spawnPos) < spawnRadius * 2)
            {
                return true;
            }
        }
        return false;
    }
}
