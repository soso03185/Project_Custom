using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager
{
    public List<DemoMonster> monsters = new List<DemoMonster>();
    public Player player;
  

    public void AddMonster(DemoMonster monster)
    {
        monsters.Add(monster);
    }

    public void DeleteMonster(DemoMonster monster)
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

    public GameObject GetNearestMonster()
    {
        if(monsters.Count == 0)
            return null;

        var distances =
            monsters.Select(monster => Vector3.Distance(player.transform.position, monster.transform.position));

        float minDistance = distances.Min();

        int index = distances.ToList().IndexOf(minDistance);

        return monsters[index].gameObject;
    }

    public GameObject GetFarthestMonster()
    {
        if (monsters.Count == 0)
            return null;

        var distances =
            monsters.Select(monster => Vector3.Distance(player.transform.position, monster.transform.position));

        float maxDistance = distances.Max();

        int index = distances.ToList().IndexOf(maxDistance);

        return monsters[index].gameObject;
    }
}
