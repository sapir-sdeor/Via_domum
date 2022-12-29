
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CoreMechanic
{
    public class Light : MonoBehaviour, ICoreMechanic
    {
     
        // Start is called before the first frame update
        public void SetLightOff(Light2D[] light2Ds)
        {
         

        }
        
        public void ApplyMechanic()
        {
            Acting.SetDestroyObstcale();
        }
        
    }
}
