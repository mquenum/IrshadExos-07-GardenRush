using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] float _growingAmount;
    [SerializeField] float _maxHeight;
    private float _chrono;
    private float _maxChrono = 15.0f;
    private bool _disabledChrono = false;
    private float growthSpeed = 5.0f;

    private void Update()
    {
        _chrono += Time.deltaTime;

        if (_chrono >= _maxChrono && !_disabledChrono)
        {
            // time out, destroy plant
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (transform.localScale.y < _maxHeight)
        {
            transform.localScale += new Vector3(0, growthSpeed * Time.deltaTime, 0);
            _disabledChrono = true;
            yield return null;
        }
    }
}
