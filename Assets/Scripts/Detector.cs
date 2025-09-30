using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    public event Action EnemyEntered;
    public event Action EnemyExited;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            EnemyEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            EnemyExited?.Invoke();
    }
}
