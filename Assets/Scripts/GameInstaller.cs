using Zenject;

namespace Netologia.Gameplay
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var controls = new Controls();
            Container.Bind<Controls>().FromInstance(controls).AsSingle();
            Container.Bind<Controls.GameActions>().FromInstance(controls.Game).AsSingle();
        }
    }
}
