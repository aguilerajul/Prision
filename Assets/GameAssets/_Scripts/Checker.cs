using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Demo.FinalScripts;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private string _nameToCheck;
    [SerializeField]
    private string _doorNameToOpen;
    [SerializeField]
    private bool _needsElectronicPanel;

    private CheckerEntity _checkerEntity;

    void Start()
    {
        _checkerEntity = new CheckerEntity
        {
            Checked = true,
            CheckerName = _nameToCheck,
            DoorNameToOpen = _doorNameToOpen
        };
    }

    void OnTriggerEnter(Collider collider)
    {
        if(_needsElectronicPanel && !StaticActions.IsElectronicPanelActive)
            return;

        if (collider.transform.name.Equals(_nameToCheck, StringComparison.CurrentCultureIgnoreCase))
        {
            GetComponent<ParticleSystem>().Play();
            var check = StaticActions.Checks.FirstOrDefault(x => x.DoorNameToOpen == _checkerEntity.DoorNameToOpen &&
                                                                 x.CheckerName == _checkerEntity.CheckerName);
            if (check == null)
            {
                StaticActions.Checks.Add(_checkerEntity);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.transform.name.Equals(_nameToCheck, StringComparison.CurrentCultureIgnoreCase))
        {
            GetComponent<ParticleSystem>().Stop();
            var check = StaticActions.Checks.FirstOrDefault(x => x.DoorNameToOpen == _checkerEntity.DoorNameToOpen &&
                                                      x.CheckerName == _checkerEntity.CheckerName);
            if (check != null)
            {
                StaticActions.Checks.Remove(check);
            }
        }
    }

}
