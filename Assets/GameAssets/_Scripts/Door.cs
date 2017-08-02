using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

namespace Assets.Demo.FinalScripts
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private int itemsToCheck = 2;
        [SerializeField]
        private bool _isFinalDoor = false;
        [SerializeField]
        private string _doorCamera;

        const bool DEBUG = false;
        Camera _finalCamera;
        FirstPersonController _player;
        Image _gameOverImage;
        Animator _animator;

        private List<string> _doorsOpened;


        void Awake()
        {
            _finalCamera = GameObject.Find(_doorCamera).GetComponent<Camera>();
            _player = FindObjectOfType<FirstPersonController>();
            _gameOverImage = FindObjectOfType<Image>();
            _animator = GetComponent<Animator>();
            var animator = GetComponent<Animator>();
            if (animator)
            {
                animator.enabled = false;
            }
        }

        void Start()
        {
            _doorsOpened = new List<string>();
            StaticActions.Checks = new List<CheckerEntity>();

            InvokeRepeating("Check", 1, 1);
        }


        void Check()
        {
            if (StaticActions.Checks.Count >= itemsToCheck)
            {
                foreach (var entry in StaticActions.Checks)
                {
                    if (!entry.Checked
                        || !entry.DoorNameToOpen.Equals(this.transform.name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return;
                    }
                }

                StartCoroutine(ChangeCamera());
            }
        }

        IEnumerator ChangeCamera()
        {
            if (_animator && !_animator.enabled && !_doorsOpened.Contains(this.transform.name))
            {
                _animator.enabled = true;
                _player.enabled = false;
                _finalCamera.enabled = true;
                _player.GetComponentInChildren<Camera>().enabled = false;

                yield return new WaitForSeconds(4f);

                if (_gameOverImage && _isFinalDoor)
                {
                    _gameOverImage.enabled = true;
                    _gameOverImage.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    _animator.enabled = false;
                    _player.enabled = true;
                    _finalCamera.enabled = false;
                    _player.GetComponentInChildren<Camera>().enabled = true;
                    if (_gameOverImage && _isFinalDoor)
                    {
                        _gameOverImage.enabled = false;
                        _gameOverImage.GetComponent<Animator>().enabled = false;
                    }

                    _doorsOpened.Add(this.transform.name);
                    StaticActions.Checks.Clear();
                }
            }
        }

    }

}
