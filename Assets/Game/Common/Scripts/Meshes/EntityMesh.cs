using System.Collections.Generic;
using UnityEngine;

namespace Otus.Meshes
{
    public interface IEntityMesh
    {
        void PushColor(Color color);

        void PopColor(Color color);
    }
    
    public sealed class EntityMesh : MonoBehaviour, IEntityMesh
    {
        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private string shaderColorName;
        
        private List<Color> colorStack;

        private Color currentColor;

        private Color baseColor;
        
        private void Awake()
        {
            this.colorStack = new List<Color>();

            this.baseColor = this.meshRenderer.material.color;
            this.currentColor = this.baseColor;
        }

        public void PushColor(Color color)
        {
            this.colorStack.Add(color);
            this.currentColor = color;
            
            this.UpdateMeshesColor();
        }

        public void PopColor(Color color)
        {
            this.colorStack.Remove(color);
            if (this.currentColor != color)
            {
                return;
            }

            var stackSize = this.colorStack.Count;
            if (stackSize > 0)
            {
                var nextIndex = stackSize - 1;
                this.currentColor = this.colorStack[nextIndex];
            }
            else
            {
                this.currentColor = this.baseColor;
            }
            
            this.UpdateMeshesColor();
        }

        private void UpdateMeshesColor()
        {
            this.meshRenderer.material.SetColor(this.shaderColorName, this.currentColor);
        }
    }
}