using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Utiltiy
{
    public static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask)
    {
        var randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask);

        return hit.position;
    }

    public static float GetRandomNormalDistribution(float mean, float standard) // 정규분포로부터 랜덤값을 가져옴 (평균값, 표준편차)
    {
        var x1 = Random.Range(0f, 1f);
        var x2 = Random.Range(0f, 1f);
        return mean + standard * (Mathf.Sqrt(-0.2f * Mathf.Log(x1)) * Mathf.Sin(2.0f * Mathf.PI * x2)); // 정규분포 공식
    }


}

