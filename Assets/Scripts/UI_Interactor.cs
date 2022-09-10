using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interactor : MonoBehaviour
{
    LineRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<LineRenderer>();
    }

    public LayerMask layerMask;
    public Button btn;

    public bool detectHit(LineRenderer rend)
    {
        bool isHit = false;

        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            btn = hit.collider.gameObject.GetComponent<Button>();
            isHit = true;
        } else
        {
            isHit = false;
        }

        return isHit;
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (detectHit(rend) && Input.GetAxis("Submit") > 0)
        {
            btn.onClick.Invoke();
        }
    }
}
