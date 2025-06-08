using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivate : MonoBehaviour
{

    public string[] lines;
    public bool canActivate;
    

    // Update is called once per frame
    void Update()
    {
        if(canActivate && Input.GetMouseButtonUp(1) && !DialogManager.instance.dialogPanel.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }


}
