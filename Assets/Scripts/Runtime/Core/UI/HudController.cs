using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SA.ripts.Runtime.Core.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private Button _settings;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        private void OnEnable()  
        {
            _winPanel.SetActive(false);
            _losePanel.SetActive(false);


            OnScoreChange(SceneContext.Instance.ScoreService.CurrentPoints);
            SceneContext.Instance.ScoreService.ChangeScoreEvent += OnScoreChange;
        }

        private void OnDisable()  
        {
            SceneContext.Instance.ScoreService.ChangeScoreEvent -= OnScoreChange;
        }

        private void OnScoreChange(int points)
        {
            _pointsText.text = $"{points}";
        }

        public void ShowLoss()
        {
            _losePanel.SetActive(true);
        }

        public void ShowWin()
        {
            _winPanel.SetActive(true);
        }
    }
}