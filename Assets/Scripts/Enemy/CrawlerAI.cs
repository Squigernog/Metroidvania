using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAI : EnemyAI
{
    public float movement = 5;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(0, movement);
    }
}
