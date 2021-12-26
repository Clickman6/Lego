using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    public TextMeshProUGUI LoadingPercentage;
    public Image LoadingProgressBar;

    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;

    public delegate AsyncOperation SceneLoader();

    public static void SwitchToScene(SceneLoader scene) {
        instance.gameObject.SetActive(true);

        instance.componentAnimator.SetTrigger("sceneClosing");

        instance.loadingSceneOperation = scene();

        instance.loadingSceneOperation.allowSceneActivation = false;

        instance.LoadingProgressBar.fillAmount = 0;
    }

    private void Start() {
        instance = this;

        componentAnimator = GetComponent<Animator>();

        if (!shouldPlayOpeningAnimation) {
            gameObject.SetActive(false);
            return;
        }
        
        componentAnimator.SetTrigger("sceneOpening");
        instance.LoadingProgressBar.fillAmount = 1;

        shouldPlayOpeningAnimation = false;
    }

    private void Update() {
        if (loadingSceneOperation == null) return;

        LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";

        LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
                                                   Time.deltaTime * 5);
    }

    public void OnSceneOpen() {
        if (!GameManager.Instance) return;
        
        GameManager.Stop();
    }

    public void OnSceneClose() {
        shouldPlayOpeningAnimation = true;

        loadingSceneOperation.allowSceneActivation = true;
    }
}
