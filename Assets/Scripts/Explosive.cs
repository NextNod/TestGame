using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public bool isActive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive) 
        {
            GameObject Aim = collision.gameObject;
            if (Aim.name.StartsWith("Person"))
            {
                float distance = Vector2.Distance(transform.position, Aim.transform.position);
                int damage = 100 - (int)distance * 2;
                MoveEng.HP -= damage;
            }
            else if (Aim.name.StartsWith("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, Aim.transform.position);
                int damage = (int)(100f - distance * 2f);
                EnemyEng.HP -= damage;
            }
            isActive = false;
        }
    }
}
