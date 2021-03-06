using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pipe : MonoBehaviour
{
    //Static parametes
    private float roughness = 150f; //Hazen williams pvc usado +20anos
    

    //Dinamic parameteres
    [SerializeField] float length = 5; //m
    [SerializeField] float diameter = 0.05f; //m
    [SerializeField] float upstreamEnergy = 0.0f;
    [SerializeField] float downstreamEnergy = 0.0f;
    [SerializeField] float flowRate = 0.0f; //m³/s
    [SerializeField] Pipe previousPipe;
    [SerializeField] List<Pipe> nextPipes;

    [SerializeField] TextMeshProUGUI textPreassure;
    [SerializeField] TextMeshProUGUI textDiameter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       UpdateTextDiameter();
       UpdateTextPreassure();
       UpdateFlowRate();
       UpdatePreassureColor();
      // verifyPreviousDiameter();
    }

    private void UpdateTextPreassure()
    {
        if(textPreassure != null){
            float mcaPreassure = downstreamEnergy/9.806f;
            mcaPreassure = (float) Math.Round(mcaPreassure, 2);

            textPreassure.SetText(mcaPreassure.ToString() + " mca");
        }
    }

    private void UpdateTextDiameter()
    {
        if(textDiameter != null){
            float diameterMilimeters = diameter*1000f;

            textDiameter.SetText("Ø" + diameterMilimeters.ToString() + " mm");
        }
    }

    private void UpdatePreassureColor(){

        if(textPreassure!= null){
            //Pressão min 10 mca = 98,06 kpa
            float minPreassure = 98.06f;

            if(downstreamEnergy<minPreassure){
                //Vermelho
                textPreassure.color = new Color32(218, 26, 26, 255);
            }else{
                //Azul
                textPreassure.color = new Color32(38, 183, 183, 255);
            }
        }
        
    }

    private void verifyPreviousDiameter(){
        if(previousPipe != null && textDiameter != null){

            //O diâmetro não pode aumentar ao longo do trecho
            if(previousPipe.diameter < diameter){
                //Vermelho
                textDiameter.color = new Color32(218, 26, 26, 255);
            }else{
                //Branco
                textDiameter.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public Pipe GetPreviousPipe(){return previousPipe;}
    public float GetDiameter(){return diameter;}
    public void SetDiameter(float newDiameter){diameter = newDiameter;}

    public float GetUpStreamEnergy(){return upstreamEnergy;}
    public void SetUpStreamEnergy(float energy){upstreamEnergy = energy;}
    public float GetDownStreamEnergy(){return downstreamEnergy;}
    public void SetDownStreamEnergy(float energy){downstreamEnergy = energy;}
    public float GetFlowRate(){return flowRate;}
    public float GetLenght(){return length;}
    public float GetRoughnesst(){return roughness;}

    private void GetEnergy(){
        if(previousPipe!= null){
            upstreamEnergy = previousPipe.downstreamEnergy;    
        }
    }

    private void UpdateFlowRate(){
        if(nextPipes.Count>0){
            flowRate = 0;
            foreach(Pipe pipe in nextPipes){
                flowRate += pipe.flowRate;
            }
        }
    }

}
