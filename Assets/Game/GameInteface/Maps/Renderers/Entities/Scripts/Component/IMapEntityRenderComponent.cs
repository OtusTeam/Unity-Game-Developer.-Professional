namespace Prototype.GameInterface
{
    public interface IMapEntityRenderComponent
    {
        void StartRender(IMapEntityLayer layer);

        void UpdateRender(IMapEntityLayer layer);

        void FinishRender(IMapEntityLayer layer);
    }
}