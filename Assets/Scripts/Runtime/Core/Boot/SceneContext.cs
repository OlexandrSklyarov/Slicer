using SA.Runtime.Core.Services;
using UnityEngine;

namespace SA
{
    public class SceneContext : MonoBehaviour
    {
        public static SceneContext Instance => _instance;
        private static SceneContext _instance;

        public ScoreService ScoreService {get; private set;}
        public SceneService SceneService {get; private set;}

        private void Awake() 
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this);
        }

        public void Init()
        {
            ScoreService = new ScoreService(0);
            SceneService = new SceneService();
        }
    }
}
