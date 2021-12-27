using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance { get; private set; }

        [Header("Simple sounds")]
        private AudioSource _audioSource;
        [SerializeField] private CustomAudio _build;
        [SerializeField] private CustomAudio _click;
        [SerializeField] private CustomAudio _deny;
        [SerializeField] private CustomAudio _destroy;
        [SerializeField] private CustomAudio _success;

        [Header("Background music")]
        private float _timer;
        [SerializeField] private bool _playBackground;
        [SerializeField] private float _nextSoundDelay;
        [SerializeField] private CustomAudio[] _musics;

        private void Awake() {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            if (_playBackground) {
                StartCoroutine(PlayBackgroundMusic());
            }
        }

        public void PlayBuild()   => PlaySound(_build);
        public void PlayClick()   => PlaySound(_click);
        public void PlayDeny()    => PlaySound(_deny);
        public void PlayDestroy() => PlaySound(_destroy);
        public void PlaySuccess() => PlaySound(_success);

        private void PlaySound(CustomAudio audio) {
            if (audio == null) return;

            _audioSource.PlayOneShot(audio.Clip, audio.GetVolume());
        }

        //Background music
        private IEnumerator PlayBackgroundMusic() {
            if (_musics.Length <= 0) yield return null;

            int lastIndex = -1;

            while (_musics.Length > 0) {
                int index = Random.Range(0, _musics.Length);

                if (lastIndex == index) yield return new WaitForSeconds(0f);

                CustomAudio audio = _musics[index];
                float waitTime = audio.Clip.length + _nextSoundDelay;

                PlaySound(audio);

                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
