using UnityEngine;

namespace Scriptes.CameraLogic
{
    public class AspectCamera : MonoBehaviour
    {
        private Camera _camera;
        
        [SerializeField] private float baseSize ;  
        [SerializeField] private float baseAspect; 

        private void Start()
        {
            _camera = GetComponent<Camera>();
            EnableCamera();
        }
        

        private void EnableCamera()
        {
            float aspect = (float)Screen.width / (float)Screen.height;
            
            _camera.orthographicSize = baseSize * (baseAspect / aspect);
        }
    }
}
