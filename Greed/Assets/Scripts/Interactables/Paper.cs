using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : Interactable {

    public Material mat;
    public Transform prefab;
    

    public override void Interact()
    {
        isHoldItem = true;
        Transform obj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        obj.SetParent(GameObject.FindGameObjectWithTag("Right Hand").transform);
        obj.localPosition = new Vector3(1.24f, 0, 1.75f);
        obj.localRotation = Quaternion.Euler(-9.01f, 32f, 21.158f);
        this.gameObject.SetActive(false);
        
    }


}
