using System.Threading.Tasks;
using IA;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        static GameObject Player => GameObject.FindGameObjectWithTag("Player");
        static MovementController Movement => Player.GetComponent<MovementController>();

        int health = 3;
        Image img;
        EnemyController enemy;

        public static Transform GetTransform() { return Player.transform; }
        public static Vector3 GetPosition() { return Player.transform.position; }
        public static float GetStamina() { return Movement.StaminaPercentage(); }

        void Start()
        {
            GameObject obj = GameObject.Find("Health");
            img = obj.GetComponent<Image>();
            obj = GameObject.Find("Ghost");
            enemy = obj.GetComponent<EnemyController>();
            enemy.gameObject.SetActive(false);

        }

        async void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            health--;

            Color color = img.color;

            switch (health)
            {
                case 3:
                    color = img.color;
                    color.a = 0;
                    img.color = color;
                    break;
                case 2:
                    color = img.color;
                    color.a = 0.5f;
                    img.color = color;
                    break;
                case 1:
                    color = img.color;
                    color.a = 1;
                    img.color = color;
                    break;
                case 0:
                    SceneManager.LoadScene("Lose_Screen");
                    break;
            }

            enemy.enabled = false;
            await Awaitable.WaitForSecondsAsync(2);
            try
            {
                enemy.enabled = true;
            }
            catch (System.Exception)
            {

            }

        }
    }
}