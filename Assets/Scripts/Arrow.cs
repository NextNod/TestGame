using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Person"))
        {
            PersonEng.HP = 0;
            Destroy(gameObject);
        }
        else if (collision.gameObject.name.StartsWith("Enemy"))
        {
            EnemyEng.HP = 0;
            Destroy(gameObject);
        }
        else
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }
}
