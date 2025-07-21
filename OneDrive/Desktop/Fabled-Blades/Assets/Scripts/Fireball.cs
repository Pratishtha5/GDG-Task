// Scripts/SkillSystem/Fireball.cs
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy on hit
        Destroy(gameObject);
    }
}
