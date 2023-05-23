using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        EnemyAI.ResetStaticData();
        Enemy.ResetStaticData();
        SkeletonArrow.ResetStaticData();
    }
}
