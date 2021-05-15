using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassureCalc : MonoBehaviour
{

[SerializeField] List<Pipe> pipes;

    public void Calc(){

        for(int i =0;i<pipes.Count;i++)
        {

            float flowRate = pipes[i].GetFlowRate();
            float length = pipes[i].GetLenght();
            float roughness = pipes[i].GetRoughnesst();
            float diameter = pipes[i].GetDiameter();
            float upstreamEnergy = 0f;
            float downStreamEnergy = 0f;

            //Pegar a energia do tubo anterior
            if (i > 0)
            {
                upstreamEnergy = pipes[i-1].GetDownStreamEnergy();
                pipes[i].SetUpStreamEnergy(upstreamEnergy);
            }
            else
            {
                upstreamEnergy = pipes[i].GetUpStreamEnergy();
            }
            float pressureLoss = CalcPreassureLoss(flowRate, length, roughness, diameter);
            downStreamEnergy = upstreamEnergy - pressureLoss;
            
            if(downStreamEnergy>0){
                pipes[i].SetDownStreamEnergy(downStreamEnergy);
            }else{
                pipes[i].SetDownStreamEnergy(0f);
            }
            
        }
    }

    private static float CalcPreassureLoss(float flowRate, float length, float roughness, float diameter)
    {
        //return (10.65f * Mathf.Pow(flowRate, 1.85f) * length) / (Mathf.Pow(roughness, 1.85f) * Mathf.Pow(diameter, 4.87f));
        return (10.65f * 0.57289981433308f * length) / (Mathf.Pow(roughness, 1.85f) * Mathf.Pow(diameter, 4.87f));
    }
}
