using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI : MonoBehaviour
{
    public Text charName;
    public Camera camera;
    public GameObject charRotation;
    private string parentTag;
    // Start is called before the first frame update
    void Start()
    {
        //Tell me which
        parentTag = transform.parent.tag;
        if (parentTag == "HenFriend")
        {
            char henIndex = transform.parent.name[transform.parent.name.Length - 1];
            int i = int.Parse(henIndex.ToString());
            //Debug.Log(GlobalVar.henNames[i]);
            
            charName.text = GlobalVar.henNames[i];
        }
        else if(parentTag == "DogFriend")
        {
            charName.text = GlobalVar.dogName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        charName.transform.rotation = Quaternion.LookRotation(charName.transform.position - camera.transform.position);
    }
}
