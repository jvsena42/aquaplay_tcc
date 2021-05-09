using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeButton : MonoBehaviour
{
    public GameObject diamScreem;

    public void showPanel(){
        diamScreem.gameObject.SetActive(true);
    }

    public void hidePanel(){
        diamScreem.gameObject.SetActive(false);
    }
}


