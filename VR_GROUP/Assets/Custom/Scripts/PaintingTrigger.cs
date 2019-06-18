namespace GoogleVR.HelloVR
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using TMPro;

    [RequireComponent(typeof(Collider))]
    public class PaintingTrigger : MonoBehaviour
    {
        // VARIABLES FOR PAINTING 
        private TextMeshPro paintingName;
        private AudioSource audioClass;
        private AudioSource[] allAudioSources;
        
        void Start()
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            paintingName = this.GetComponentInChildren<TextMeshPro>();
            audioClass = this.GetComponentInChildren<AudioSource>();
            IsFocused(false);
        } 

        public void IsFocused(bool focused)
        {
            //Debug.Log("idFocused: " + focused);
            paintingName.enabled = focused;
            if (!focused)
            {
                audioClass.Stop();
            }
            return;
        }

        void StopAllAudio() {
            foreach( AudioSource audioS in allAudioSources) {
                audioS.Stop();
            }
        }
        public void PlayAudio()
        {
            StopAllAudio();
            audioClass.Play(0);
            Debug.Log("started");
        }
    }
}
