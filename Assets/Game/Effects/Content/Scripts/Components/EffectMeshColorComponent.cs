using DynamicObjects;
using Otus.Meshes;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectMeshColorComponent : EffectComponent
    {
        [SerializeField]
        private Color color;

        private IEntityMesh targetMesh;
        
        public override void Activate(IDynamicObject target, IHandler handler)
        {
            var mesh = target.GetProperty<IEntityMesh>(PropertyKey.ENTITY_MESH);
            if (this.targetMesh == mesh)
            {
                return;
            }

            mesh.PushColor(this.color);
            this.targetMesh = mesh;
        }

        public override void Deactivate()
        {
            this.targetMesh.PopColor(this.color);
            this.targetMesh = null;
        }
    }
}