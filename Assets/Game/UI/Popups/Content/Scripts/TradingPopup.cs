using System;
using GameElements;
using Prototype.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.GameEngine
{
    //Пример:
    public sealed class TradingPopup : PopupAnimated, IGameContextElement  
    {
        public IGameSystem GameSystem { get; set; }
        
        [SerializeField]
        private Button woodButton;

        [SerializeField]
        private Button stoneButton;

        private TradingViewController controller;
        
        private void Awake()
        {
            this.controller = new TradingViewController(this.GameSystem);
        }

        protected override void OnShow(object data)
        {
            this.SetupContoller(data);
            this.woodButton.onClick.AddListener(this.OnWoodButtonClicked);
            this.stoneButton.onClick.AddListener(this.OnStoneButtonClicked);
        }

        protected override void OnHide()
        {
            this.woodButton.onClick.RemoveListener(this.OnWoodButtonClicked);
            this.stoneButton.onClick.RemoveListener(this.OnStoneButtonClicked);
        }

        private void OnWoodButtonClicked()
        {
            this.controller.SaleWoodClicked();
        }

        private void OnStoneButtonClicked()
        {
            this.controller.SaleStoneClicked();
        }
        
        private void SetupContoller(object data)
        {
            if (data is Args args)
            {
                this.controller.Setup(args.Character, args.Trader);
            }
            else
            {
                throw new Exception("Expected TradingPopup.Args");
            }
        }

        public sealed class Args
        {
            public ITradeCharacter Character { get; }

            public ITrader Trader { get; }

            public Args(ITradeCharacter character, ITrader trader)
            {
                this.Character = character;
                this.Trader = trader;
            }
        }
    }
}