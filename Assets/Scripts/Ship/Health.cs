using UnityEngine;

namespace Ship
{
    public class Health : MonoBehaviour
    {
        private int _health = 10;
        
        private const int MIN_HEALTH = 0;

        void Awake()
        {
            
        }
        
        public void TakeDamage(int damage)
        { 
            Debug.Log("Took some damage");
            _health = Mathf.Max(MIN_HEALTH, _health - damage);
        }
    }
}
