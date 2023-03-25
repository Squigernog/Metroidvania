using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Roaming,
        Investigate,
        ChaseTarget,
        Attack,
        None,
    }

    public enum InvestigateState
    {
        FacePosition,
        MoveToPosition,
    }

    public Vector3 startingPos;
    public Vector3 roamingPos;
    public Transform playerPos;

    [SerializeField] public GameObject investigateStatus;
    [SerializeField] public GameObject playerSpottedStatus;
    [SerializeField] public GameObject tiredStatus;

    public Enemy EN;

    public State state;
    public InvestigateState iState;
    public bool alert = false;

    public float nextWaypointDistance = 3f;

    public int currentWaypoint = 0;
    public Rigidbody2D rb;

    public float roamSpeed = .01f;

    public void Alert()
    {
        alert = true;
    }

    public Vector3 GetRoamingPosition()
    {
        return startingPos + GetRandomDir() * Random.Range(2f, 5f);
    }

    //generates a random direction in the form of a vector3 variable
    public Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public void RotateTowardsTarget(Vector3 targetPosition) //Rotate: https://www.youtube.com/watch?v=mKLp-2iseDc&ab_channel=KristerCederlund
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * EN.rotationSpeed);
    }

    public IEnumerator PlayerSpottedStatusCoroutine()
    {
        playerSpottedStatus.SetActive(true);
        yield return new WaitForSeconds(.5f);
        playerSpottedStatus.SetActive(false);

    }
}
