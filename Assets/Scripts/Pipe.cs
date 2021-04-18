using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    //Static parametes
    private float roughness = 0.0015f; //Hazen williams
    private float length = 5; //m

    //Dinamic parameteres
    [SerializeField] float diameter = 0.1f; //m
    [SerializeField] float upstreamEnergy = 0.0f;
    [SerializeField] float downstreamEnergy = 0.0f;
    [SerializeField] float flowRate = 0.0f; //m³/s

    [SerializeField] Pipe previousPipe;
    [SerializeField] List<Pipe> nextPipes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetEnergy();
        GetFlowRate();
        CalcPressureDrop();
    }

    private void CalcPressureDrop(){
        //make the pipes calc in the correct order
        if(upstreamEnergy!=0.0f){
            float pressureLoss = 10.65f * Mathf.Pow(flowRate,1.85f)*length/(Mathf.Pow(roughness,1.85f) * Mathf.Pow(diameter,4.87f));
            downstreamEnergy = upstreamEnergy-pressureLoss;
        }
    }

    private void GetEnergy(){
        if(previousPipe!= null){
            upstreamEnergy = previousPipe.downstreamEnergy;    
        }
    }

    private void GetFlowRate(){
        if(nextPipes.Count>0){
            flowRate = 0;
            foreach(Pipe pipe in nextPipes){
                flowRate += pipe.flowRate;
            }
        }
    }

}
