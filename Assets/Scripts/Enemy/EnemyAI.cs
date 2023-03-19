using System.Collections;
using System.Collections.Generic;
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
}
