using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
     public void showScreen(GameObject screen){
        screen.gameObject.SetActive(true);
    }

    public void hideScreen(GameObject screen){
        screen.gameObject.SetActive(false);
    }
}
