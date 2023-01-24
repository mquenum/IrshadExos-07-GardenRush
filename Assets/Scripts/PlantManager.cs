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
        Grow(_growingAmount);
    }

    private void Grow(float grow)
    {
        Transform obj = transform;
        float yGrow = (obj.localScale.y + grow) - obj.localScale.y;
        Vector3 scaleChange = new Vector3(1.0f, yGrow, 1.0f);
        float nextHeight = scaleChange.y + obj.localScale.y;

        if (nextHeight <= _maxHeight)
        {
            // grow plant
            obj.localScale += scaleChange;
            // reset _chrono
            _chrono = 0;
        }
        else
        {
            // disable chrono to prevent grown plants destruction
            _disabledChrono = true;
        }
    }
}
