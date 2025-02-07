using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button gotoBehaviourSceneButton;
        [SerializeField] private Button quitButton;
        
        private void Start()
        {
            gotoBehaviourSceneButton.onClick.AddListener(() => SceneManager.LoadScene("Scene Tasks"));
            quitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}
