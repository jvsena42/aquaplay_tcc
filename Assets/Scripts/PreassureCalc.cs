using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassureCalc : MonoBehaviour
{

[SerializeField] List<Pipe> pipes;

    public void Calc(){

        for(int i =0;i==pipes.Count;i++){

            float flowRate = pipes[i].GetFlowRate();
            float length = pipes[i].GetLenght();
            float roughness = pipes[i].GetRoughnesst();
            float diameter = pipes[i].GetDiameter();
            float upstreamEnergy = 0f;
                       
            //Pegar a energia do tubo anterior
            if(i>0){
                 upstreamEnergy = pipes[i-1].GetUpStreamEnergy();
            }else{
                 upstreamEnergy = pipes[i].GetUpStreamEnergy();
            }
        
            float pressureLoss = 10.65f * Mathf.Pow(flowRate,1.85f)*length/(Mathf.Pow(roughness,1.85f) * Mathf.Pow(diameter,4.87f));
            pipes[i].SetUpStreamEnergy(upstreamEnergy-pressureLoss);
        }
    }

}
