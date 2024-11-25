using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    public void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        Movement();
    }
}
