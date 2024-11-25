using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Stack<Vector2> actualPosition = new Stack<Vector2>();
    private Stack<Sprite> spriteStack = new Stack<Sprite>();

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;

    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!mainCamera) 
            mainCamera = Camera.main;
    }

    private void Start()
    {
        AddPosition(transform.position, spriteRenderer.sprite);
    }

    void Update()
    {
        ManageInputs();
    }

    void ManageInputs()
    {
        Vector2 movement = Vector2.zero;
        Sprite newSprite = spriteRenderer.sprite;

        if (Input.GetKeyDown(KeyCode.W))
        {
            movement = Vector2.up;
            newSprite = spriteUp;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movement = Vector2.down;
            newSprite = spriteDown;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movement = Vector2.left;
            newSprite = spriteLeft;
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movement = Vector2.right;
            newSprite = spriteLeft;
            spriteRenderer.flipX = true;
        }

        if (movement != Vector2.zero)
        {
            Vector2 targetPosition = (Vector2)transform.position + movement;

            if (IsWithinScreen(targetPosition))
            {
                AddPosition(transform.position, spriteRenderer.sprite);
                transform.Translate(movement);
                spriteRenderer.sprite = newSprite; 
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ResetPosition();
        }
    }

    private void AddPosition(Vector2 position, Sprite sprite)
    {
        actualPosition.Push(position);
        spriteStack.Push(sprite);
    }

    private void ResetPosition()
    {
        if (actualPosition.Count > 0 && spriteStack.Count > 0)
        {
            transform.position = actualPosition.Pop();
            spriteRenderer.sprite = spriteStack.Pop();
            spriteRenderer.flipX = false;
        }
    }

    private bool IsWithinScreen(Vector2 targetPosition)
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(targetPosition);

        return viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
               viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }
}
