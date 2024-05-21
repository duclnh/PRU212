using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject storePannel;

    void Start(){
      ToogleStore();
    }
    public void ToogleStore(){
       if(storePannel != null){
            if(!storePannel.activeSelf){
                storePannel.SetActive(true);
            }else{
                storePannel.SetActive(false);
            }
       }
    }

    public bool ToggleStoreStatus() => storePannel.activeSelf;
}
