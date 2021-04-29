using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;
using Foundation;

public sealed class AutoSceneContext : SceneContext
{
    void EnumerateTransform(Transform transform, List<MonoInstaller> installers)
    {
        var components = transform.GetComponents<MonoInstaller>();
        foreach (var component in components) {
            if (component.AutoInitMode == MonoInstaller.AutoInit.Scene)
                installers.Add(component);
        }

        int n = transform.childCount;
        for (int i = 0; i < n; i++)
            EnumerateTransform(transform.GetChild(i), installers);
    }

    List<Type> GetDependencies(Type type)
    {
        var result = new List<Type>();

        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        var fields = type.GetFields(flags);
        foreach (var field in fields) {
            Attribute attribute = field.GetCustomAttribute<InjectAttribute>();
            if (attribute != null) {
                if (field.IsInitOnly) {
                    DebugOnly.Error($"Field \"{type.Name}.{field.Name}\" is init only.");
                    continue;
                }
                result.Add(field.FieldType);
            }

            attribute = field.GetCustomAttribute<InjectOptionalAttribute>();
            if (attribute != null) {
                if (field.IsInitOnly) {
                    DebugOnly.Error($"Field \"{type.Name}.{field.Name}\" is init only.");
                    continue;
                }
                result.Add(field.FieldType);
            }
        }

        var properties = type.GetProperties(flags);
        foreach (var prop in properties) {
            Attribute attribute = prop.GetCustomAttribute<InjectAttribute>();
            if (attribute != null) {
                if (!prop.CanWrite) {
                    DebugOnly.Error($"Property \"{type.Name}.{prop.Name}\" is not writable.");
                    continue;
                }
                result.Add(prop.PropertyType);
            }

            attribute = prop.GetCustomAttribute<InjectOptionalAttribute>();
            if (attribute != null) {
                if (!prop.CanWrite) {
                    DebugOnly.Error($"Property \"{type.Name}.{prop.Name}\" is not writable.");
                    continue;
                }
                result.Add(prop.PropertyType);
            }
        }

        return result;
    }

    protected override void RunInternal()
    {
        var installers = new List<MonoInstaller>(Installers);
        EnumerateTransform(transform, installers);

        var factoryDict = new Dictionary<Type, Type>();
        var interfaceDict = new Dictionary<Type, Type>();
        var typeToInstaller = new Dictionary<Type, MonoInstaller>();
        var installerDeps = new List<List<Type>>();
        foreach (var installer in installers) {
            var type = installer.GetType();

            typeToInstaller[type] = installer;
            installerDeps.Add(GetDependencies(type));

            foreach (var iface in type.GetInterfaces())
                interfaceDict[iface] = type;

            var attribute = type.GetCustomAttribute<FactoryInstallerAttribute>();
            if (attribute != null)
                factoryDict[attribute.FactoryType] = type;
        }

        var initialized = new HashSet<Type>();
        var sortedInstallers = new List<MonoInstaller>(Installers);

        int n = installers.Count;
        for (int i = 0; i < n; i++) {
            var installer = installers[i];
            var type = installer.GetType();
            var deps = installerDeps[i];

            /*
            var build = new System.Text.StringBuilder();
            build.Append(type.ToString());
            build.Append(":\n");
            foreach (var dep in deps)
                build.Append($"  - {dep}\n");
            DebugOnly.Message(build.ToString());
            */

            foreach (var it in deps) {
                var dep = it;
                if (interfaceDict.TryGetValue(dep, out var interfaceInstaller))
                    dep = interfaceInstaller;
                else if (factoryDict.TryGetValue(dep, out var factoryInstaller))
                    dep = factoryInstaller;

                if (initialized.Add(dep)) {
                    if (typeToInstaller.TryGetValue(dep, out var depInstaller)) {
                        DebugOnly.Message($"Adding {dep} as dependency of {type}.");
                        sortedInstallers.Add(depInstaller);
                    }
                }
            }

            if (initialized.Add(type)) {
                DebugOnly.Message($"Adding {type}.");
                sortedInstallers.Add(installer);
            }
        }

        Installers = sortedInstallers;

        base.RunInternal();
    }
}
