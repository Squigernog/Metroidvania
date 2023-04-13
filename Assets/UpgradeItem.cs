using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag != "Player")
        {
            return;
        }

        if(this.transform.name == "Double Jump Upgrade")
        {
            collider.gameObject.transform.GetComponent<PlayerController>().SetDoubleJump(true);
            this.gameObject.SetActive(false);
        }

        //TODO 
        //add conditions for other upgrade items
        
    }
}
