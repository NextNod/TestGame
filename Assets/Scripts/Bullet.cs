using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Person;
    public int Damage = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Person) PersonEng.HP -= Damage;
        else if (collision.gameObject.name.StartsWith("Enemy")) EnemyEng.HP -= Damage;
        Destroy(gameObject);
    }
}
