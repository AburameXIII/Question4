using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MovementState { MovingP1, MovingP2, MovingP3, End}

public class Movement : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float Speed;
    MovementState CurrentState;
    [Header("Points")]
    public Transform P1;
    public Transform P2;
    public Transform P3;
    [Header("VictoryEffects")]
    public AudioSource VictorySound;
    public GameObject VictoryParticleEffect;
    [Header("ExplosionEffects")]
    public AudioSource ExplosionSound;
    public GameObject ExplosionParticleEffect;

    private void Start()
    {
        CurrentState = MovementState.MovingP1;
    }

    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        if (CurrentState == MovementState.End) return;

        Vector3 EndGoal = Vector3.zero;

        switch (CurrentState)
        {
            case (MovementState.MovingP1): EndGoal = P1.position; break;
            case (MovementState.MovingP2): EndGoal = P2.position; break;
            case (MovementState.MovingP3): EndGoal = P3.position; break;
        }

        Vector3 Direction = (EndGoal - this.transform.position).normalized;

        Rigidbody.MovePosition(this.transform.position + Direction * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.collider.tag == "P1" && CurrentState == MovementState.MovingP1)
        {
            CurrentState = MovementState.MovingP2;
        }
        else if (collision.collider.tag == "P2" && CurrentState == MovementState.MovingP2)
        {
            CurrentState = MovementState.MovingP3;
        }
        else if (collision.collider.tag == "P3" && CurrentState == MovementState.MovingP3)
        {
            CurrentState = MovementState.End;
            Instantiate(VictoryParticleEffect, this.transform);
            VictorySound.Play();
        }
        else if (collision.collider.tag == "Asteroid")
        {
            CurrentState = MovementState.End;
            Instantiate(ExplosionParticleEffect, this.transform.position, Quaternion.identity);
            ExplosionSound.Play();
            Destroy(this.gameObject);
        }
    }


}
