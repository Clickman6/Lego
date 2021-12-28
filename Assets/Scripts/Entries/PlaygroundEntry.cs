using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using Managers;
using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Playground = SaveLoad.Playground;

namespace Entries {
    public class PlaygroundEntry : MonoBehaviour, ISceneLoadHandler<bool> {
        public static PlaygroundEntry Instance { get; private set; }

        private bool _load;

        [Header("UI")]
        [SerializeField] private GameObject _continue;
        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _loading;
        [SerializeField] private Image _loadingProgressBar;
        [SerializeField] private TextMeshProUGUI _loadingPercentage;

        [Header("Save")]
        [SerializeField] private GameObject _buildings;
        [SerializeField] private Colors _colors;
        [SerializeField] private List<GameObject> _blocks = new List<GameObject>();
        [SerializeField] private GameObject _savePanel;
        [SerializeField] private Image _saveLoading;

        public void OnSceneLoaded(bool load) {
            _load = load;
        }

        private void Awake() {
            Instance = this;
        }

        public void Start() {
            // GameManager.Stop();

            _continue.SetActive(true);

            if (_load) {
                Load();
            } else {
                ShowButton();
            }
        }

        public void Continue() {
            _continue.SetActive(false);
            GameManager.Play();

            // gameObject.SetActive(false);

            StartCoroutine(AutoSave());
        }

        private void ShowButton() {
            _loading.SetActive(false);
            _button.SetActive(true);
        }

        private void Load() {
            SaveLoad.SaveLoad.LoadFile();

            Player.Player.Instance.transform.position = Playground.Current.Player.Position();
            Player.Player.Instance.transform.rotation = Playground.Current.Player.Rotation();

            var blocks = Playground.Current.Items;
            int progress;

            for (int i = 0; i < blocks.Count; i++) {
                progress = Mathf.RoundToInt(i * 100 / blocks.Count);

                _loadingPercentage.text = progress + "%";
                _loadingProgressBar.fillAmount =
                    Mathf.Lerp(_loadingProgressBar.fillAmount, progress, Time.deltaTime * 5);

                CreateBlock(blocks[i]);
            }

            ShowButton();
        }

        public void Save() {
            Playground.Save(_buildings.GetComponentsInChildren<Block>(), Player.Player.Instance);
            SaveLoad.SaveLoad.SaveFile();

            StartCoroutine(HideSavePanel());
        }

        private IEnumerator AutoSave() {
            while (true) {
                Save();

                yield return new WaitForSeconds(60f * 5f);
            }
        }

        public IEnumerator HideSavePanel() {
            _savePanel.SetActive(true);

            while (_saveLoading.fillAmount < 1) {
                _saveLoading.fillAmount = Mathf.Lerp(_saveLoading.fillAmount,
                                                     _saveLoading.fillAmount + 15f * Time.deltaTime,
                                                     Time.deltaTime * 10);

                yield return null;
            }

            _savePanel.SetActive(false);
        }

        private void CreateBlock(SaveLoad.Block block) {
            var material = _colors.GetMaterials().Find(i => i.GetInstanceID() == block.Material);
            var prefab = _blocks[block.Id];

            if (!prefab) return;

            var tmp = Instantiate(prefab, block.Position(), block.Rotation(), _buildings.transform);

            if (material) {
                tmp.GetComponent<Block>().SetMaterial(material);
            }

            tmp.layer = LayerMask.NameToLayer("Building");
        }
    }
}
