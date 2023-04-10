using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.noveltech.dev/simple-patrolling-monster-unity/
public class CrawlerAI : EnemyAI
{
    public float movement = 5;
    public float mRaycastingDistance = 1f;
    private bool bIsGoingRight = true;
    private SpriteRenderer _mSpriteRenderer;

    private void OnEnable()
    {
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = bIsGoingRight;

        rb = GetComponent<Rigidbody2D>();
        bIsGoingRight=true;
    }

    private void FixedUpdate()
    {
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * movement;

        transform.Translate(directionTranslation);

        CheckForWalls();
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (bIsGoingRight) ? Vector3.right : Vector3.left;

        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f);

        // if we hit something, check its tag and act accordingly
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Wall")
            {
                bIsGoingRight = !bIsGoingRight;
                _mSpriteRenderer.flipX = bIsGoingRight;

            }
        }
    }
}
