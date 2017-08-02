using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ElectronicPanelButton : MonoBehaviour
{
    [SerializeField]
    Renderer _buttonRenderer;
    [SerializeField]
    Material _onMaterial;
    [SerializeField]
    Material _offMaterial;

    private bool _canActivateChecker;

    // Use this for initialization
    void Start()
    {
        
        _buttonRenderer.material = _offMaterial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canActivateChecker)
        {
            StaticActions.IsElectronicPanelActive = !StaticActions.IsElectronicPanelActive;
            _buttonRenderer.material = StaticActions.IsElectronicPanelActive ? _onMaterial : _offMaterial;
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag.Equals("Player"))
        {
            _canActivateChecker = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (!hit.tag.Equals("Player"))
        {
            _canActivateChecker = false;
        }
    }
}
