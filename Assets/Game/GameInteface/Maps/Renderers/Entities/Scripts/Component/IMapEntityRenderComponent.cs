namespace Prototype.GameInterface
{
    public interface IMapEntityRenderComponent
    {
        void StartRender(MapEntityLayer layer);

        void UpdateRender(MapEntityLayer layer);

        void FinishRender(MapEntityLayer layer);
    }
}