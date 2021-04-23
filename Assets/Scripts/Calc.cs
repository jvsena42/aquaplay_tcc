using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calc : MonoBehaviour
{
    [SerializeField] Cycle cycle;
    void Start()
    {
        cycle.startCalc();
    }

}
