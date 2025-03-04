using Drops;
using Surface;
using Zenject;

namespace GameScene
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SurfaceController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DropController>().FromComponentInHierarchy().AsSingle();
        }
    }
}