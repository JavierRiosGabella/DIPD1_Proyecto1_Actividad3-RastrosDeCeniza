using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_SelfDestroyer : MonoBehaviour {
    
    [SerializeField] GameObject particlesPrefab = null;

	public void UP_SelfDestroy()
    {
        Destroy(this.gameObject);
        if(particlesPrefab)
        {
            Instantiate(particlesPrefab, this.transform.position, Quaternion.identity);
        }
    }

}
