using UnityEngine;

namespace Assets.Demo.FinalScripts
{
    public class Pickup : MonoBehaviour
    {
        public Transform PickUpTransform;
        public float ForceDrop = 3;

        Camera _camera;
        Transform _hasPickup;

        void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }


        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //No tiene ninguna caja en la mano por lo que comprobamos
                //si podemos seleccionar alguna
                if (_hasPickup == null)
                {
                    var ray = _camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 2.5f))
                    {
                        if (!hit.transform.CompareTag("Pickupable")) return;
                        var rb = hit.transform.GetComponent<Rigidbody>();

                        if (!rb) return;

                        _hasPickup = hit.transform;

                        _hasPickup.position = PickUpTransform.position;
                        _hasPickup.parent = gameObject.transform;
                        rb.isKinematic = true;
                    }
                }

                //Tenemos una caja en la mano por lo que la lanzamos
                //hacia adelante
                else
                {
                    var rb = _hasPickup.GetComponent<Rigidbody>();
                    if(rb)
                    {
                        _hasPickup.transform.parent = null;
                        rb.isKinematic = false;
                        rb.AddForce(transform.forward * ForceDrop, ForceMode.Impulse);

                        _hasPickup = null;
                    }
                    
                }
            }
        }
    }

}
