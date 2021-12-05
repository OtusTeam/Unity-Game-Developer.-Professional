// using System;
// using Game.UI.Popups.Content.Scripts;
// using GameElements;
// using Prototype.UI;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Prototype.UI
// {
//     //Пример:
//     public sealed class TradingPopup : PopupAnimated, IGameContextElement  
//     {
//         public IGameSystem GameSystem { get; set; }
//         
//         [Space]
//         [SerializeField]
//         private Button woodButton;
//
//         [SerializeField]
//         private Button stoneButton;
//
//         [SerializeField]
//         private CharacterResourcesView resourcesView;
//
//         private CharacterTradeController tradeController;
//         
//         private CharacterResourceController
//         
//         private void Awake()
//         {
//             this.tradeController = new CharacterTradeController(this.GameSystem);
//         }
//
//         protected override void OnShow(object data)
//         {
//             if (!(data is Args args))
//             {
//                 throw new Exception("Expected TradingPopup.Args");
//             }
//
//
//             var character = args.Character;
//             var trader = args.Trader;
//             
//             this.Setup(character, trader);
//             this.tradeController.Setup(character, trader);
//             this.woodButton.onClick.AddListener(this.OnWoodButtonClicked);
//             this.stoneButton.onClick.AddListener(this.OnStoneButtonClicked);
//         }
//
//         private void Setup(ITradeCharacter character, ITrader trader)
//         {
//             this.moneyText.text = character.Money.ToString();`
//             this.woodText.text
//             this.stoneText.text = 
//         }
//         
//
//         protected override void OnHide()
//         {
//             this.woodButton.onClick.RemoveListener(this.OnWoodButtonClicked);
//             this.stoneButton.onClick.RemoveListener(this.OnStoneButtonClicked);
//         }
//
//         private void OnWoodButtonClicked()
//         {
//             this.tradeController.SaleWoodClicked();
//         }
//
//         private void OnStoneButtonClicked()
//         {
//             this.tradeController.SaleStoneClicked();
//         }
//         
//         public sealed class Args
//         {
//             public ITradeCharacter Character { get; }
//
//             public ITrader Trader { get; }
//
//             public Args(ITradeCharacter character, ITrader trader)
//             {
//                 this.Character = character;
//                 this.Trader = trader;
//             }
//         }
//     }
// }