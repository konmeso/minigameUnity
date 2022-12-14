using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // VARIABLES
    [SerializeField] private float mouseSensitivity;


    // REFERENCES
    private Transform parent;
    


    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent; //not sure how it works
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);        
    }

}
