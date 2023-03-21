using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class cubeSpawner : MonoBehaviour
{
    [SerializeField] private BoxController _boxController;
    [SerializeField] private SplineComputer _splineComputer;

    private IEnumerator Start()
    {
        while(true)
        {
            var dummyCube = Instantiate(_boxController, transform);
            dummyCube.Init(_splineComputer);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
