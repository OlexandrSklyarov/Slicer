using System;
using System.Threading.Tasks;
using Cinemachine;
using SA.ripts.Runtime.Core.UI;
using SA.Runtime.Core.Map;
using SA.Runtime.Core.Slicer;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace SA.Runtime.Core.Boot
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _followCamera;
        [SerializeField] private CinemachineVirtualCamera _startCamera;
        [SerializeField] private SlicerController _slicerController;
        [SerializeField] private MapController _mapController;

        private HudController _hud;

        private async void Start()
        {            
            await UniTask.WaitUntil(() => FindObjectOfType<HudController>() != null); 
            _hud = FindObjectOfType<HudController>();

            _mapController.Init();
            _slicerController.Init();

            Debug.Log("_startCamera");
            SetCameraTarget(_startCamera, _followCamera, _slicerController.transform, _slicerController.transform);   

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            Debug.Log("_followCamera");
            SetCameraTarget(_followCamera, _startCamera, _slicerController.transform, _slicerController.LookCameraPoint); 

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            Debug.Log("Active slicer...");

            _mapController.FinishEvent += OnCompletedAsync;    
            _slicerController.DamageEvent += OnLossAsync; 

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

        private async void OnLossAsync()
        {           
            StopGame();

            _hud.ShowLoss();

            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            SceneContext.Instance.SceneService.LoadMenu();
        }

        private async void OnCompletedAsync()
        {  
            _hud.ShowWin();

            StopGame();

            SceneContext.Instance.ScoreService.AddPoints(_slicerController.CollectedPoints);            

            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            SceneContext.Instance.SceneService.LoadMenu();
        }       

        private void StopGame()
        {            
            _mapController.FinishEvent -= OnCompletedAsync;    
            _slicerController.DamageEvent -= OnLossAsync; 

            _followCamera.Follow = _startCamera.Follow = null;
            _followCamera.LookAt = _startCamera.LookAt = null;
            _slicerController.ActiveControl(false);

            GameCameraPushBack();
        }

        private void GameCameraPushBack()
        {
            _followCamera.transform.DOMoveZ(_followCamera.transform.position.z -3f, 2f);
        }
    }
}
