using UnityEngine;
using UnityEngine.UI;

namespace Prototype.UI
{
    public sealed class CharacterResourcesView : MonoBehaviour
    {
        [SerializeField]
        private Text woodText;

        [SerializeField]
        private Text stoneText;

        [SerializeField]
        private Text moneyText;

        public void SetMoney(int money)
        {
            this.moneyText.text = money.ToString();
        }

        public void SetStone(int stoneAmount)
        {
            this.stoneText.text = stoneAmount.ToString();
        }

        public void SetWood(int wood)
        {
            this.woodText.text = wood.ToString();
        }
    }
}