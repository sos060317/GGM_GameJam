using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitube = 5f;
    [SerializeField] private float offset = 0f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitube;
    }
}