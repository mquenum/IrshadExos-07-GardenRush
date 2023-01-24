using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] float _growingAmount;
    [SerializeField] float _maxHeight;
    private float _chrono;
    private float _maxChrono = 15.0f;
    private bool hasGrown = false;
    private float growthSpeed = 5.0f;

    private void OnEnable()
    {
        _chrono = 0f;
    }

    private void Update()
    {
        _chrono += Time.deltaTime;

        if (_chrono >= _maxChrono && !hasGrown)
        {
            // time out, destroy plant
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasGrown && other.CompareTag("Player"))
            StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (transform.localScale.y < _maxHeight)
        {
            transform.localScale += new Vector3(0, growthSpeed * Time.deltaTime, 0);
            hasGrown = true;
            yield return null;
        }
    }
}
