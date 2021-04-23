using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cycle")]
public class Cycle : ScriptableObject
{
    [SerializeField] List<Stretch> stretchs;
    private float preassureLossSum = 0f;
    private float hazenWilliansConstant = 1.85f;

    public void startCalc(){

        CalPreassureDrop();

        while(preassureLossSum >0.0001){
            BalanceFlow();
        }
    }

    private void CalPreassureDrop(){
        preassureLossSum =0f;
        foreach(Stretch stretch in stretchs){
            preassureLossSum += stretch.PreassureLoss();
        }
    }

    private void BalanceFlow(){
        
        float energyFlow = CalcEnergyFlowRatio();

        float deldaFlow = -1f*preassureLossSum/(hazenWilliansConstant*energyFlow);

        
        foreach(Stretch stretch in stretchs){

            if(stretch.IsPositiveDirecton()){
                float newFlow = stretch.GetFlowRate()+deldaFlow;
                
                if(newFlow>0) {
                    stretch.SetPositiveDirecton(true);
                }else{
                    stretch.SetPositiveDirecton(false);
                }

                stretch.SetFlowRate(Mathf.Abs(newFlow));
            }else{
                
                float newFlow = stretch.GetFlowRate()-deldaFlow;
                
                if(newFlow>0) {
                    stretch.SetPositiveDirecton(true);
                }else{
                    stretch.SetPositiveDirecton(false);
                }

                stretch.SetFlowRate(Mathf.Abs(newFlow));
            }
        }

        CalPreassureDrop();
    }

    // preassuredrop/deltaQ
    private float CalcEnergyFlowRatio(){

        float energyFlow = 0f;
        foreach(Stretch stretch in stretchs){
            energyFlow += stretch.PreassureLoss()/stretch.GetFlowRate();
        }
        return energyFlow;
    }    
}
