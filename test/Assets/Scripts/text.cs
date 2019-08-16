using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    // Start is called before the first frame update
    void awake()
    {
      if(GameObject.FindGameObjectWithTag("Enemy") == null){
        gameObject.SetActive(true);
      }else{
        gameObject.SetActive(false);
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
