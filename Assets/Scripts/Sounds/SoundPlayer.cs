using UnityEngine;
using Utilities;

namespace Sounds
{
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource audioSource;
        private Timer timer;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
        }
        
        public void Play()
        {
            audioSource.Play();
        }
        
        public void Play(float duration)
        {
            audioSource.Play();
            timer = new CountdownTimer(duration);
            timer.OnTimerStop += Stop;
            timer.Start();
        }
        
        public void PlayLoop()
        {
            audioSource.loop = true;
            audioSource.Play();
        }
        
        public void PlayLoop(float duration)
        {
            audioSource.loop = true;
            audioSource.Play();
            timer = new CountdownTimer(duration);
            timer.OnTimerStop += Stop;
            timer.Start();
        }
        
        public void Stop()
        {
            audioSource.Stop();
        }

        private void Update()
        {
            timer?.Tick(Time.deltaTime);
        }
    }
}
