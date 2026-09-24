using UnityEngine;
using UnityEngine.UI;
using player = Player.PlayerManager;

namespace HUD
{
    public class EnergyController: MonoBehaviour
    {
        [SerializeField] Image leftFill;
        [SerializeField] Image rightFill;

        void Update()
        {
            float stamina = player.GetStamina();
            leftFill.fillAmount = stamina;
            rightFill.fillAmount = stamina;
        }
    }
}