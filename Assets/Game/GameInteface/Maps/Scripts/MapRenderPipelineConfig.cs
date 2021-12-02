using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Prototype.GUI
{
    [CreateAssetMenu(
        fileName = "MapRendererPipelineConfig",
        menuName = "GameInterface/Maps/New MapRendererPipelineConfig"
    )]
    public sealed class MapRenderPipelineConfig : ScriptableObject
    {
        public IMapRenderer[] LoadOrderedRenderers()
        {
            var count = this.renderClassNames.Length;
            var result = new IMapRenderer[count];
            for (var i = 0; i < count; i++)
            {
                var fullClassName = this.renderClassNames[i];
                var renderType = Type.GetType(fullClassName);
                if (renderType == null)
                {
                    throw new Exception($"Render Class {fullClassName} is not found");
                }

                result[i] = (IMapRenderer) Activator.CreateInstance(renderType);
            }

            return result;
        }
        
        [HideInInspector]
        [SerializeField]
        private string[] renderClassNames;

#if UNITY_EDITOR
        
        [LabelText("Map Renderers")]
        [SerializeField]
        private MonoScript[] renderScripts;

        private void OnValidate()
        {
            var count = this.renderScripts.Length;
            this.renderClassNames = new string[count];
            for (var i = 0; i < count; i++)
            {
                var script = this.renderScripts[i];
                var className = script.GetClass().FullName;
                this.renderClassNames[i] = className;
            }
        }
#endif
    }
}