using UnityEngine;
using Zenject;

namespace _Project.Scripts.Common.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("InstallBindings BootstrapInstaller..");
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy BootstrapInstaller..");
        }
    }
}
