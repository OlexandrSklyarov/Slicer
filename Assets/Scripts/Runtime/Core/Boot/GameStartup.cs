using System;
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
        [SerializeField] private SlicerController _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private MapController _mapController;

        private SlicerController _slicerController;
        private HudController _hud;

        private async void Start()
        {
            await FindHUD();

            _mapController.Init();

            //init Slicer
            if (_slicerController == null) 
                _slicerController = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
                
            _slicerController.enabled = false;
            _slicerController.enabled = true;
            _slicerController.Init();

            await WaitSwitchCameras();

            //Active slicer...
            _mapController.FinishEvent += OnCompletedAsync;
            _slicerController.DamageEvent += OnLossAsync;
            _slicerController.ActiveControl(true);
        }

        private async UniTask FindHUD()
        {
            await UniTask.WaitUntil(() => FindObjectOfType<HudController>() != null);
            _hud = FindObjectOfType<HudController>();
        }

        private async UniTask WaitSwitchCameras()
        {
            //_startCamera
            SetCameraTarget(_startCamera, _followCamera, _slicerController.transform, _slicerController.transform);

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            //_followCamera
            SetCameraTarget(_followCamera, _startCamera, _slicerController.transform, _slicerController.LookCameraPoint);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
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

            SceneContext.Instance.SceneService.LoadGame();
        }

        private async void OnCompletedAsync()
        {  
            _hud.ShowWin();

            StopGame();

            SceneContext.Instance.ScoreService.AddPoints(_slicerController.CollectedPoints);            

            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            SceneContext.Instance.SceneService.LoadGame();
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
