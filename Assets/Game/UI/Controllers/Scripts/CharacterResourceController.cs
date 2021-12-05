// namespace Prototype.UI
// {
//     public sealed class CharacterResourceController
//     {
//         private readonly CharacterResourcesView view;
//
//         private ITradeCharacter currentCharacter;
//         
//         public CharacterResourceController(CharacterResourcesView view)
//         {
//             this.view = view;
//         }
//
//         public void Setup(ITradeCharacter character)
//         {
//             this.currentCharacter = character;
//             this.currentCharacter.OnMoneyChanged += this.OnMoneyChanged;
//             this.currentCharacter.OnStoneChanged += this.OnStoneChanged;
//             this.currentCharacter.OnWoodChanged += this.OnWoodChanged;
//
//             this.UpdateWood();
//             this.UpdateStone();
//             this.Update
//         }
//
//         private void OnWoodChanged(int obj)
//         {
//             
//         }
//
//         private void OnStoneChanged(int obj)
//         {
//             
//         }
//
//         private void OnMoneyChanged(int obj)
//         {
//             
//         }
//
//         public void Reset()
//         {
//             if (this.currentCharacter != null)
//             {
//                 this.currentCharacter.OnMoneyChanged -= this.OnMoneyChanged;
//                 this.currentCharacter.OnStoneChanged -= this.OnStoneChanged;
//                 this.currentCharacter.OnWoodChanged -= this.OnWoodChanged;
//             }
//         }
//     }
// }