using System.Collections.Generic;
using Zenject;

namespace Foundation
{
    public sealed class CameraManager : AbstractService<ICameraManager>,
        ICameraManager,
        IOnPlayerAdded,
        IOnPlayerRemoved
    {
        [Inject]
        private IPlayerManager playerManager = default;

        private readonly List<ICamera> allCameras = new List<ICamera>();

        private readonly List<CameraHolder> playersWithCameras = new List<CameraHolder>();

        public void ToggleFirstThirdPersonCamera(int playerIndex)
        {
            var player = this.GetPlayerWithCameras(playerIndex);
            if (player != null)
            {
                player.FirstPersonMode = !player.FirstPersonMode;
            }
        }

        public void AddFirstPersonCamera(IFirstPersonCamera camera)
        {
            if (camera.PlayerIndex >= 0)
            {
                var player = this.GetPlayerWithCameras(camera.PlayerIndex);
                if (player != null)
                {
                    player.AddFirstPersonCamera(camera);
                }
            }

            this.allCameras.Add(camera);
            this.UpdateCamera(camera);
        }

        public void RemoveFirstPersonCamera(IFirstPersonCamera camera)
        {
            if (camera.PlayerIndex >= 0)
            {
                var player = this.GetPlayerWithCameras(camera.PlayerIndex);
                if (player != null)
                {
                    player.RemoveFirstPersonCamera(camera);
                }
            }

            this.allCameras.Remove(camera);
            this.UpdateCameras();
        }

        public void AddThirdPersonCamera(IThirdPersonCamera camera)
        {
            if (camera.PlayerIndex >= 0)
            {
                var player = this.GetPlayerWithCameras(camera.PlayerIndex);
                if (player != null)
                {
                    player.AddThirdPersonCamera(camera);
                }
            }

            this.allCameras.Add(camera);
            this.UpdateCamera(camera);
        }

        public void RemoveThirdPersonCamera(IThirdPersonCamera camera)
        {
            if (camera.PlayerIndex >= 0)
            {
                var player = this.GetPlayerWithCameras(camera.PlayerIndex);
                if (player != null)
                {
                    player.RemoveThirdPersonCamera(camera);
                }
            }

            this.allCameras.Remove(camera);
            this.UpdateCameras();
        }

        #region Callbacks

        protected override void OnEnable()
        {
            base.OnEnable();
            this.Observe(this.playerManager.OnPlayerAdded);
            this.Observe(this.playerManager.OnPlayerRemoved);
            this.UpdateCameras(true);
        }

        private void Update()
        {
            this.UpdateCameras();
        }

        void IOnPlayerAdded.Do(int playerIndex)
        {
            if (playerIndex < 0)
            {
                DebugOnly.Error("Invalid player index.");
                return;
            }

            while (playerIndex >= playersWithCameras.Count)
            {
                this.playersWithCameras.Add(null);
            }

            var player = this.playersWithCameras[playerIndex];
            if (player == null)
            {
                player = this.InstantiatePlayer(playerIndex);
                this.playersWithCameras[playerIndex] = player;
            }

            this.UpdateCameras(true);
        }
        
        void IOnPlayerRemoved.Do(int playerIndex)
        {
            if (playerIndex >= 0 && playerIndex < playersWithCameras.Count)
            {
                this.playersWithCameras[playerIndex] = null;
            }

            this.UpdateCameras(true);
        }

        #endregion

        private CameraHolder InstantiatePlayer(int playerIndex)
        {
            var firstPersonCameras = new List<ICamera>();
            var thirdPersonCameras = new List<ICamera>();
            
            for (int i = 0, count = this.allCameras.Count; i < count; i++)
            {
                var camera = this.allCameras[i];
                if (camera is IFirstPersonCamera firstPerson && firstPerson.PlayerIndex == playerIndex)
                {
                    firstPersonCameras.Add(firstPerson);
                }

                if (camera is IThirdPersonCamera thirdPerson && thirdPerson.PlayerIndex == playerIndex)
                {
                    thirdPersonCameras.Add(thirdPerson);
                }
            }

            return new CameraHolder(firstPersonCameras, thirdPersonCameras);
        }
        
        private CameraHolder GetPlayerWithCameras(int index)
        {
            if (index < 0 || index >= this.playersWithCameras.Count)
            {
                DebugOnly.Error("Invalid player index.");
                return null;
            }

            return this.playersWithCameras[index];
        }

        private void UpdateCamera(ICamera camera)
        {
            var playerIndex = camera.PlayerIndex;
            if (playerIndex < 0)
            {
                camera.GameObject.SetActive(false);
            }
            else
            {
                var cameraHolder = this.GetPlayerWithCameras(playerIndex);
                var isActive = cameraHolder != null && camera == cameraHolder.CurrentCamera;
                camera.GameObject.SetActive(isActive);
            }
        }

        private void UpdateCameras(bool force = false)
        {
            var changed = force;
            foreach (var player in playersWithCameras)
            {
                //Игрок выставляет текущую камеру поле CurrentCamera
                if (player.UpdateCurrentCamera())
                {
                    changed = true;
                }
            }

            if (changed)
            {
                //Вкл / выкл каждой камеры
                foreach (var camera in allCameras)
                {
                    this.UpdateCamera(camera);
                }
            }
        }

        private sealed class CameraHolder
        {
            public bool FirstPersonMode { get; set; }

            public ICamera CurrentCamera { get; private set; }
            
            private readonly List<ICamera> firstPersonCameras;
            
            private readonly List<ICamera> thirdPersonCameras;

            public CameraHolder(List<ICamera> firstPersonCameras, List<ICamera> thirdPersonCameras)
            {
                this.firstPersonCameras = firstPersonCameras;
                this.thirdPersonCameras = thirdPersonCameras;
                this.CurrentCamera = this.GetRequiredCamera();
            }

            public void AddFirstPersonCamera(ICamera camera)
            {
                this.firstPersonCameras.Add(camera);
            }

            public void AddThirdPersonCamera(ICamera camera)
            {
                this.thirdPersonCameras.Add(camera);
            }

            public void RemoveFirstPersonCamera(ICamera camera)
            {
                this.firstPersonCameras.Remove(camera);
            }
            
            public void RemoveThirdPersonCamera(ICamera camera)
            {
                this.thirdPersonCameras.Remove(camera);
            }

            public bool UpdateCurrentCamera()
            {
                var requiredCamera = this.GetRequiredCamera();
                if (requiredCamera != this.CurrentCamera)
                {
                    this.CurrentCamera = requiredCamera;
                    return true;
                }

                return false;
            }

            private ICamera GetRequiredCamera()
            {
                if (this.FirstPersonMode)
                {
                    if (this.firstPersonCameras.Count > 0)
                    {
                        return this.firstPersonCameras[0];
                    }

                    DebugOnly.Error("No first person cameras in the scene.");
                }
                else
                {
                    if (this.thirdPersonCameras.Count > 0)
                    {
                        return this.thirdPersonCameras[0];
                    }

                    DebugOnly.Error("No third person cameras in the scene.");
                }

                return null;
            }
        }
    }
}