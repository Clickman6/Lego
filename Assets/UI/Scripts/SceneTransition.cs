using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    public TextMeshProUGUI LoadingPercentage;
    public Image LoadingProgressBar;

    private static SceneTransition _instance;
    private static bool _shouldPlayOpeningAnimation = false;

    private Animator _componentAnimator;
    private AsyncOperation _loadingSceneOperation;

    public static void SwitchToScene(string sceneName) {
        _instance._componentAnimator.SetTrigger("sceneClosing");

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        _instance._loadingSceneOperation.allowSceneActivation = false;

        _instance.LoadingProgressBar.fillAmount = 0;
    }

    private void Start() {
        _instance = this;

        _componentAnimator = GetComponent<Animator>();

        if (!_shouldPlayOpeningAnimation) return;
        
        _componentAnimator.SetTrigger("sceneOpening");
        _instance.LoadingProgressBar.fillAmount = 1;

        _shouldPlayOpeningAnimation = false;
    }

    private void Update() {
        if (_loadingSceneOperation == null) return;

        LoadingPercentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";

        LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, _loadingSceneOperation.progress,
                                                   Time.deltaTime * 5);
    }

    public void OnAnimationOver() {
        _shouldPlayOpeningAnimation = true;

        _loadingSceneOperation.allowSceneActivation = true;
    }
}
