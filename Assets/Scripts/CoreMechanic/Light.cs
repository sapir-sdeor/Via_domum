
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CoreMechanic
{
    public class Light : MonoBehaviour, ICoreMechanic
    {
        private Light2D[] light2Ds;
        // Start is called before the first frame update
        public void SetLight(Light2D[] light2Ds)
        {
            this.light2Ds = light2Ds;
            foreach (Light2D _light2D in this.light2Ds)
            {
                _light2D.color = Color.black;
            }
           
        }
        
        
        public void ApplyMechanic()
        {
            //to
            
        }
    }
}
