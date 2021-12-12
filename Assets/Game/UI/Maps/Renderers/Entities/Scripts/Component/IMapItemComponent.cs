namespace Prototype.GameInterface
{
    public interface IMapItemComponent
    {
        void StartRender(IMapEntityLayer layer);

        void UpdateRender(IMapEntityLayer layer);

        void FinishRender(IMapEntityLayer layer);
    }
}