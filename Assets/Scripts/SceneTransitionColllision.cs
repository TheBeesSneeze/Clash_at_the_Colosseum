using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
public class SceneTransitionColllision : MonoBehaviour
{
    [Scene] [Tooltip("Scene the tutorial is transitioning to. Please do NOT change this.")]
    public int TransitionScene;


    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(TransitionScene);
        PublicEvents.StartSound.Invoke();
    }
}
