using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    public Slider FlyBar;
    public Text area;
    public Text talkTo;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVar.flyingPower = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        talkTo.text = GlobalVar.talkToFriendly;
        area.text = GlobalVar.area;
        FlyBar.value = GlobalVar.flyingPower;
        if (GlobalVar.flyingPowerCooldown == true)
        {
            if (GlobalVar.flyingPower < 1.0f)
            {
                GlobalVar.flyingPower += 0.0001f;
            }
            
        }
        if (GlobalVar.flyingPower > 0.99f)
        {
            GlobalVar.flyingPowerCooldown = false;
        }
       
    }
}
