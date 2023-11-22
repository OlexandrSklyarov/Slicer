using UnityEngine;

namespace SA.Runtime.Core.Boot
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start() 
        {
            SceneContext.Instance.Init();

            SceneContext.Instance.SceneService.LoadMenu();    
        }
    }
}
