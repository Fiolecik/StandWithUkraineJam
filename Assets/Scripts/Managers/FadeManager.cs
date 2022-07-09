using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class FadeManager : SingletonMonoBehaviour<FadeManager>
    {
        [HideInInspector] public UnityEvent OnFadeEnd = new UnityEvent();
        public bool Fading
        {
            get => fading;
        }
        
        [SerializeField] private Color clearColor = Color.clear;
        [SerializeField] private Color fadeColor = Color.black;
        
        private Image image;
        private bool fading = false;
        
        private void Awake()
        {
            image = GetComponent<Image>();
            DontDestroyOnLoad(gameObject);
        }

        public void FadeIn()
        {
            StartCoroutine(AnimateFade(fadeColor));
        }

        public void FadeOut()
        {
            StartCoroutine(AnimateFade(clearColor));
        }

        private IEnumerator AnimateFade(Color to)
        {
            if(fading)
                yield break;
            fading = true;
            float delta = 0;
            Color currentColor = image.color;
            while (delta<1)
            {
                yield return null;
                delta += Time.deltaTime;
                image.color = Color.Lerp(currentColor, to, delta);
            }

            OnFadeEnd.Invoke();
            fading = false;
        }
    }
}