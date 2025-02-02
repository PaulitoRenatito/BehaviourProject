using System;
using System.Collections.Generic;
using Entities;
using Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event EventHandler OnReset;
        public event EventHandler OnPause;
        
        [Header("Menu")]
        [SerializeField] private GameObject menuPanel;
        
        [Header("Buttons")]
        [SerializeField] private Button startStaticBehaviourTreeSimulationButton;
        [SerializeField] private Button startDynamicBehaviourTreeSimulationButton;
        [SerializeField] private Button exitButton;
        
        [Header("Entities")]
        [SerializeField] private EntityTask staticBehaviourTreeEntityPrefab;
        [SerializeField] private EntityTask dynamicBehaviourTreeEntityPrefab;
        
        [Header("Tasks")]
        [SerializeField] private List<Task> taskList; 
        
        [Header("Camera")]
        [SerializeField] private CinemachineCamera mainCamera;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            startStaticBehaviourTreeSimulationButton.onClick.AddListener(StartStaticBehaviourTreeSimulation);
            startDynamicBehaviourTreeSimulationButton.onClick.AddListener(StartDynamicBehaviourTreeSimulation);
            exitButton.onClick.AddListener(() => SceneManager.LoadScene("Main Menu"));
            
            menuPanel.SetActive(false);
            mainCamera.Follow = staticBehaviourTreeEntityPrefab.transform;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMenuPanel();
            }
        }

        private void ToggleMenuPanel()
        {
            if (menuPanel.activeSelf)
            {
                Time.timeScale = 1f;
                menuPanel.SetActive(false);
            }
            else
            {
                OnPause?.Invoke(this, EventArgs.Empty);
                Time.timeScale = 0f;
                menuPanel.SetActive(true);
            }
        }
        
        private void StartStaticBehaviourTreeSimulation()
        {
            OnReset?.Invoke(this, EventArgs.Empty);
            
            staticBehaviourTreeEntityPrefab.gameObject.transform.position = new Vector3(0, 0, 0);
            staticBehaviourTreeEntityPrefab.gameObject.SetActive(true);
            
            dynamicBehaviourTreeEntityPrefab.gameObject.transform.position = new Vector3(0, 0, 0);
            dynamicBehaviourTreeEntityPrefab.gameObject.SetActive(false);
            
            menuPanel.SetActive(false);
            
            Time.timeScale = 1f;
            
            mainCamera.Follow = staticBehaviourTreeEntityPrefab.transform;
        }
        
        private void StartDynamicBehaviourTreeSimulation()
        {
            OnReset?.Invoke(this, EventArgs.Empty);
            
            dynamicBehaviourTreeEntityPrefab.gameObject.transform.position = new Vector3(0, 0, 0);
            dynamicBehaviourTreeEntityPrefab.gameObject.SetActive(true);
            
            staticBehaviourTreeEntityPrefab.gameObject.transform.position = new Vector3(0, 0, 0);
            staticBehaviourTreeEntityPrefab.gameObject.SetActive(false);
            
            menuPanel.SetActive(false);
            
            Time.timeScale = 1f;
            
            mainCamera.Follow = dynamicBehaviourTreeEntityPrefab.transform;
        }
    }
}
