using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Stretch")]
public class Stretch : ScriptableObject
{
    private float roughness = 0.0015f; //Hazen williams

    //Dinamic parameteres
    [SerializeField] float diameter = 0.1f; //m
    [SerializeField] float length = 5f;
    [SerializeField] float preassureLoss = 0.0f;
    [SerializeField] float flowRate = 0.0f; // m³/s
    [SerializeField] bool positiveDirection = true;
    
    public void SetPreassureLoss(float newPreassure){
        preassureLoss = newPreassure;
    }

    public void SetFlowRate(float newFlowRate){
        flowRate = newFlowRate;
    }

    public float GetFlowRate(){
        return flowRate;
    }

    public bool IsPositiveDirecton(){
        return positiveDirection;
    }

    public void SetPositiveDirecton(bool direction){
        positiveDirection = direction;
    }

    public float PreassureLoss(){
        float pressureLossCalc = 10.65f * Mathf.Pow(flowRate,1.85f)*length/(Mathf.Pow(roughness,1.85f) * Mathf.Pow(diameter,4.87f));
        return pressureLossCalc; 
    }
}
