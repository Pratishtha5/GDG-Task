using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingPanel; // Assign in Inspector
    public float loadingDuration = 2f; // Duration of fake loading

    public WarriorController warrior; // Reference to freeze input

    public void TriggerLoading()
    {
        StartCoroutine(LoadingRoutine());
    }

    IEnumerator LoadingRoutine()
    {
        loadingPanel.SetActive(true);
        if (warrior != null)
            warrior.isFrozen = true; // Freeze movement

        yield return new WaitForSeconds(loadingDuration);

        loadingPanel.SetActive(false);
        if (warrior != null)
            warrior.isFrozen = false; // Resume movement
    }
}
