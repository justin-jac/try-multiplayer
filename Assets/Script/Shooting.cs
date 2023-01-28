using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    [HideInInspector] public bool canFire;
    private float timer;
    public float fireRate;

    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canFire = true;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);

            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer > fireRate)
                {
                    canFire = true;
                    timer = 0;
                }
            }

            if (Input.GetMouseButtonDown(0) && canFire)
            {
                canFire = false;
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }
        }
    }
}
