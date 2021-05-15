using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeButton : MonoBehaviour
{
    public GameObject diamScreem;
    [SerializeField] int pipeId;
    [SerializeField] List<Pipe> listPipes;

    public void showPanel(){
        diamScreem.gameObject.SetActive(true);
    }

    public void hidePanel(){
        diamScreem.gameObject.SetActive(false);
    }

    public void UpdateDiameter(float diameter){
        listPipes[pipeId].SetDiameter(diameter);
    }

    public void SetPipeId(int id){pipeId = id;}    
}


