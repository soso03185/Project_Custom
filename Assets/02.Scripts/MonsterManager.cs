using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager
{
    public List<GameObject> monsters = new List<GameObject>();
  

    public void AddMonster(GameObject monster)
    {
        monsters.Add(monster);
    }

    public void DeleteMonster(GameObject monster)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] == monster)
            {
                monsters.RemoveAt(i);
                return;
            }
        }
    }

    public GameObject GetNearestMonster(Player player)
    {
        if(monsters.Count == 0)
            return null;

        var distances =
            monsters.Select(monster => Vector3.Distance(player.transform.position, monster.transform.position));

        float minDistance = distances.Min();

        int index = distances.ToList().IndexOf(minDistance);

        return monsters[index];
    }

    public GameObject GetFarthestMonster(Player player)
    {
        if (monsters.Count == 0)
            return null;

        var distances =
            monsters.Select(monster => Vector3.Distance(player.transform.position, monster.transform.position));

        float maxDistance = distances.Max();

        int index = distances.ToList().IndexOf(maxDistance);

        return monsters[index];
    }
}
