using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrapMovement : MonoBehaviour
{
    private Rigidbody2D sawRigidbody;
    public Transform particles;
    private ParticleSystem sawParticles;
    public BoxCollider2D startPoint, endPoint;

    public float speed = 5f;
    public float rotationSpeed = 1f;

    private Vector2 direction;

    private void Awake()
    {
        sawRigidbody = GetComponent<Rigidbody2D>();
        sawParticles = particles.GetComponent<ParticleSystem>();
        direction = (startPoint.transform.position - endPoint.transform.position).normalized;
    }

    private void Update()
    {
        sawRigidbody.velocity = new Vector2(direction.x * speed, direction.y * speed);
        particles.transform.position = transform.position;

        transform.Rotate(new Vector3(0, 0, 1), rotationSpeed);
    }

    public void ChangeDirection()
    {
        direction = new Vector2(-direction.x, -direction.y);

        particles.transform.Rotate(new Vector3(0, 180, 0));
    }

}
