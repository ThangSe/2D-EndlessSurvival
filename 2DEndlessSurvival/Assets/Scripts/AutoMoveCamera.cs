using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private void Update()
    {
        Vector3 moveDir = Vector3.right;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
