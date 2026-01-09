using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class puzzle_data : MonoBehaviour
{

   public red_switch button1;
   public green_switch button2;    
   public  gold_switch button3;
  [SerializeField]  public UnityEvent puzzlesolved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Start()
    {
        button1 = GameObject.Find("Button-1").GetComponent<red_switch>();
        button2 = GameObject.Find("Button-2").GetComponent<green_switch>();
        button3 = GameObject.Find("Button-3").GetComponent<gold_switch>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
      if (button1.rednumber == 3 && button2.greennumber == 2 && button3.goldnumber == 3)
        {
            puzzlesolved.Invoke();
            Debug.Log("solved");
        }
        
    }
}
