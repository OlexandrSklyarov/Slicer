using System;
using System.Threading.Tasks;
using Cinemachine;
using SA.Runtime.Core.Slicer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SA.Runtime.Core.Level
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private SlicerController _slicerController;
        [SerializeField] private CinemachineVirtualCamera _followCamera;
        [SerializeField] private CinemachineVirtualCamera _startCamera;
        

        private async void Start()
        {
            Debug.Log("_followCamera");
            SetCameraTarget(_followCamera, _startCamera, _slicerController.transform, _slicerController.LookCameraPoint); 

            await Task.Delay(TimeSpan.FromSeconds(1f));

            Debug.Log("_startCamera");
            SetCameraTarget(_startCamera, _followCamera, _slicerController.transform, _slicerController.transform);   

            await Task.Delay(TimeSpan.FromSeconds(2f));

            Debug.Log("_followCamera");
            SetCameraTarget(_followCamera, _startCamera, _slicerController.transform, _slicerController.LookCameraPoint); 

            await Task.Delay(TimeSpan.FromSeconds(1f));

            Debug.Log("Active slicer...");

            _slicerController.CompleteEvent += OnCompleted;    
            _slicerController.DamageEvent += OnLoss; 

            _slicerController.ActiveControl(true);  
        }

        private void SetCameraTarget(CinemachineVirtualCamera camera, CinemachineVirtualCamera prevCamera, 
            Transform target, Transform lookPoint)
        {
            prevCamera.Priority = 0;
            camera.Priority = 10;
            camera.Follow = target;
            camera.LookAt = lookPoint;
        }

        private async void OnLoss()
        {
            Debug.Log("Loss");
            _slicerController.DamageEvent -= OnLoss; 
            StopGame();

            await Task.Delay(TimeSpan.FromSeconds(2f));

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnCompleted()
        {
            Debug.Log("Completed!!!");
            _slicerController.CompleteEvent -= OnCompleted;    
            StopGame();
        }       

        private void StopGame()
        {            
            _followCamera.Follow = _startCamera.Follow = null;
            _followCamera.LookAt = _startCamera.LookAt = null;
            _slicerController.ActiveControl(false);

        }
    }
}
