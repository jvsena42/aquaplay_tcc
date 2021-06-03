using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PreassureCalc : MonoBehaviour
{

[SerializeField] List<Pipe> pipes;
[SerializeField] List<Pipe> pipesVerifyPreassure;
public GameObject successScreem;
public GameObject failurePreassureScreem;
public GameObject failureDiameterScreem;

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
                Pipe previousPipe = pipes[i].GetPreviousPipe();
                if(previousPipe != null){
                    upstreamEnergy = previousPipe.GetDownStreamEnergy();
                    pipes[i].SetUpStreamEnergy(upstreamEnergy);
                }else{
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
            VerifyMinPreassure();
        
    }

    private static float CalcPreassureLoss(float flowRate, float length, float roughness, float diameter)
    {
        return (10.65f * Mathf.Pow(flowRate, 1.85f) * length) / (Mathf.Pow(roughness, 1.85f) * Mathf.Pow(diameter, 4.87f));
    }

    private void VerifyMinPreassure(){
        float minPreassure = 98.06f;

        List<float> filteredList = GetListPreassures().Where(x => x < minPreassure).ToList();

        if(filteredList.Count>0){
            showScreen(failurePreassureScreem);
        }else{
            showScreen(successScreem);
        }

    }

    private List<float> GetListPreassures(){
        List<float> preassures = new List<float>();
        
        foreach(Pipe pipe in pipesVerifyPreassure){
            preassures.Add(pipe.GetDownStreamEnergy());
        }

        return preassures;
    }

     private bool verifyPreviousDiameter(){
         foreach(Pipe pipe in pipes){
            if(pipe.GetPreviousPipe() != null){

                float diameter = pipe.GetDiameter();
                float previousDiameter = pipe.GetPreviousPipe().GetDiameter();

                if(previousDiameter < diameter){
                    return false;
                }
            }
        }
        return true;
    }

     public void showScreen(GameObject screen){
        screen.gameObject.SetActive(true);
    }

    public void hideScreen(GameObject screen){
        screen.gameObject.SetActive(false);
    }
}
