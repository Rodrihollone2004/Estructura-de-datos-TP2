using UnityEngine;

public class CollisionItems : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    Missions missions;

    private void Awake()
    {
        missions = FindObjectOfType<Missions>();     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            spawnObject.SetActive(true);
            missions._Missions.Dequeue();
            missions.UIUpdate();
        }
    }
}
