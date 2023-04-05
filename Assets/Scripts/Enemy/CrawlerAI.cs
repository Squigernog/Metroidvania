using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.noveltech.dev/simple-patrolling-monster-unity/
public class CrawlerAI : EnemyAI
{
    public float movement = 5;
    private bool isGoingRight = true;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        isGoingRight=true;
    }

    private void FixedUpdate()
    {
        Vector3 directionTranslation = (isGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * movement;

        transform.Translate(directionTranslation);
    }
}
