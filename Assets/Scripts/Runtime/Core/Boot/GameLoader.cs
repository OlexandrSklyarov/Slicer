using UnityEngine;

namespace SA.Runtime.Core.Boot
{
    public class GameLoader : MonoBehaviour
    {
        public void Load()
        {            
            SceneContext.Instance.SceneService.LoadGame();
        }
    }
}
