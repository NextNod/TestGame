using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject Boom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(Instantiate(Boom, transform.position, Quaternion.identity), 1.5f);
    }
}
