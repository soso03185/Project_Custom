using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<MonsterScript> monsters;// = new List<MonsterScript>();
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (monsters.Count != 0)
        {
            Debug.Log(monsters[0].transform);
        }
    }

    public void AddMonster(MonsterScript monster)
    {
        monsters.Add(monster);
    }

    public void DeleteMonster(MonsterScript monster)
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
}
