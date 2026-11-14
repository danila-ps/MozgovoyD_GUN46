using Zenject;

namespace Netologia.DI
{
	public class MainInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<SceneController>().FromComponentOn(gameObject).AsSingle();
		}
	}
}
