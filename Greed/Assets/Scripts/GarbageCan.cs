using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCan : MonoBehaviour {

    public ParticleSystem ps;


	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaperBall"))
        {
            ps.Emit(150);
        }
    }
}
