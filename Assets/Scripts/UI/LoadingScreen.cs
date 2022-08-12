using System.Collections;
using UnityEngine;
using DG.Tweening;
using Managers;
using Infrastructure;
using Infrastructure.AssetsProvider;
using Infrastructure.Factory;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup[] _panelsToShow;

        private const float MinValue = 0f, MaxValue = 1f, TimeToFade = 0.8f, TimeBetweenPanels = 3f;

        private void Start()
        {
            StartCoroutine(nameof(ShowPanels));
        }

        private IEnumerator ShowPanels()
        {
            for (int i = 0; i < _panelsToShow.Length; i++)
            {
                if (i > 0) // First panel has max fade
                {
                    _panelsToShow[i].DOFade(MaxValue, TimeToFade);
                }

                if (i == _panelsToShow.Length - 1) // If loading screen 
                {
                    yield return new WaitForSeconds(TimeToFade); // Wait until we see loading

                    SceneLoader sceneLoader = AllServices.Container.Single<SceneLoader>();
                    if (sceneLoader != null)
                    {
                        Debug.Log("Start loading scene");
                        sceneLoader.Load("Lobby");

                    }
                    
                    yield return null;
                }

                yield return new WaitForSeconds(TimeBetweenPanels);


                _panelsToShow[i].DOFade(MinValue, TimeToFade);
                yield return new WaitForSeconds(TimeToFade);
            }

        }

        //private IEnumerator LoadLobby()
        //{
        //    AsyncOperation ao = GameManager.Instance.StartLobby();

        //    yield return new WaitUntil(() => ao.isDone);

            
        //    Destroy(gameObject);
        //}
    }
}